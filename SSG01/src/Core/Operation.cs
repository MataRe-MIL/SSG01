namespace SSG01.Core
{
    using System;
    using System.Collections.Generic;
    using System.Security.Cryptography;

    public class Operation
    {
        Core.Utilities util = new Core.Utilities();     //ユーティリティの起動
        UI.MapRenderer mapRenderer = new UI.MapRenderer();      //マップレンダラー起動
        UI.Menu menu;     //メニュー起動


        Data.Stages.Stage stage;        //現在のステージデータ
        List<Data.Units.Unit> actionOrder = new List<Data.Units.Unit>();       //ユニット行動順リスト
        int turnCount = 0;       //行動周回数

        public Operation(UI.Menu menu)
        {
            this.menu = menu;
        }


        //<summary>
        //オペレーションモードのメイン関数。オペレーション中の処理はこの関数を中心として処理される。
        //nowStage...現在のステージデータ
        //</summary>
        public void startOperation(Data.Stages.Stage nowStage)
        {
            stage = nowStage;

            Console.WriteLine("作戦開始\n\n");

            while (true)
            {
                ++turnCount;        //行動周回数の加算
                Console.WriteLine(turnCount + "周目\n");

                util.ConsoleClear();        //コンソールのクリア
                mapRenderer.DrawMap(stage.mapTiles, stage.setUnits);        //マップの描画
                drawActionTurn();       //行動順の決定
                unitsAction();        //ユニットが行動順に沿って行動
            }
        }


        //==========UnitAction==========
        private void unitsAction()
        {
            for(int i = 0; i < actionOrder.Count; ++i)
            {
                actionOrder[i].unitAction(menu);       //ユニットの行動
                util.ConsoleClear();        //コンソールのクリア
                mapRenderer.DrawMap(stage.mapTiles, stage.setUnits);        //マップの描画
            }
        }


        //<summary>
        //マップ上のユニットの行動順を決定する関数。
        //ユニットの行動順は俊敏値を基にした乱数によって決定される。
        //</summary>
        public void drawActionTurn()
        {
            actionOrder.Clear();        //行動順リストの初期化

            for (int i = 0; i < stage.setUnits.Length; ++i)
            {
                for(int j = 0; j < stage.setUnits[i].Count; ++j)
                {
                    stage.setUnits[i][j].actionRandom();
                    actionOrder.Add(stage.setUnits[i][j]);
                }
            }
            actionOrder.Sort((a, b) => b.actionRandomValue.CompareTo(a.actionRandomValue));      //行動順の確定
        }

        //<summary>
        //あるひとつのマップタイルが特定の値であるかを確認する関数。
        //マップタイルが特定の値であれば1、そうでなければ1を返す。
        //x...マップタイルのx座標、y...マップタイルのy座標
        //返り値...{通行の可否(0,1), ユニットが存在するかどうか(0,1)}
        //</summary>
        public int[] checkMapTile(int x, int y)
        {
            int[] result = new int[2];      //{通行の可否(1,0), ユニットが存在するかどうか(0,1)}

            if (x < 0 || y < 0)
            {
                result[0] = 0;
                result[1] = 0;
                return result;
            }
            else
            {
                result[0] = stage.mapTiles[x][y];
                result[1] = 0;
                for (int i = 0; i < stage.setUnits.Length; ++i)
                {
                    for (int j = 0; j < stage.setUnits[i].Count; ++j)
                    {
                        if (stage.setUnits[i][j].x == x && stage.setUnits[i][j].y == y)
                        {
                            result[1] = 1;
                            goto EndTileCheckLoop;
                        }
                    }
                }
                EndTileCheckLoop:;
            }
            return result;
        }

        //<summary>
        //ある座標を中心として指定した距離（マンハッタン距離）内のユニットをリストアップする関数。
        //なお探索中心座標のユニットは探索対象から除外される。
        //x...探索中心のx座標、y...探索中心のy座標、distance...探索する距離
        //</summary>
        public List<Data.Units.Unit> searchUnits(int x, int y, int distance)
        {
            List<Data.Units.Unit> resultList = new List<Data.Units.Unit>();     //探索結果保存リスト
            
            for(int i = 0; i < stage.setUnits.Length; ++i)
            {
                for(int j = 0; j < stage.setUnits[i].Count; ++j)
                {
                    if (stage.setUnits[i][j].x != x && stage.setUnits[i][j].y != y && (Math.Abs(stage.setUnits[i][j].x - x) + Math.Abs(stage.setUnits[i][j].y - y)) <= distance)
                    {
                        resultList.Add(stage.setUnits[i][j]);
                    }
                }
            }

            return resultList;
        }

        //<summary>
        //指定されたターゲット座標から各タイルまでの距離を計算する関数。
        //マップのタイル情報とターゲット座標を入力として受け取り、距離マップを返す。
        //</summary>
        public int[][] createDistanceMap(int[][] mapTiles, int[] targetPosition)
        {
            int targetX = targetPosition[0];
            int targetY = targetPosition[1];
            int height = mapTiles.Length;
            int width = mapTiles[0].Length;

            //ターゲット座標がマップの範囲外である場合、例外をスローする
            if (targetX < 0 || targetY < 0 || targetX >= height || targetY >= width)
            {
                throw new ArgumentException("ターゲット座標がマップの範囲外です。");
            }


            int[][] distanceMap = new int[height][];

            for(int x =0; x < height; ++x)
            {
                distanceMap[x] = new int[width];

                for(int y = 0; y < width; ++y)
                {
                    distanceMap[x][y] = -1;
                }
            }

            distanceMap[targetX][targetY] = 0;     //ターゲット座標の距離を0に設定

            //ターゲット座標のタイルが通行不可である場合は-1で埋められた距離マップを返す
            if (mapTiles[targetX][targetY] == 0)
            {
                return distanceMap;
            }

            Queue<(int x, int y)> queue = new Queue<(int x, int y)>();
            queue.Enqueue((targetX, targetY));

            //上・下・左・右
            int[] dx = { -1, 1, 0, 0 };
            int[] dy = { 0, 0, -1, 1 };

            while(queue.Count > 0)
            {
                (int currentX, int currentY) = queue.Dequeue();
                int currentDistance = distanceMap[currentX][currentY];

                for(int i = 0; i < 4; ++i)
                {
                    int nextX = currentX + dx[i];
                    int nextY = currentY + dy[i];

                    //マップの範囲外または通行不可能なタイルまたは既に探索済みである場合はスキップ
                    if (nextX < 0 || nextY < 0 || nextX >= height || nextY >= width || mapTiles[nextX][nextY] == 0 || distanceMap[nextX][nextY] != -1)
                    {
                        continue;
                    }

                    distanceMap[nextX][nextY] = currentDistance + 1;
                    queue.Enqueue((nextX, nextY));
                }
            }

            return distanceMap;
        }
    }
}