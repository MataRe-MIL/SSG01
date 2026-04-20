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
        public void StartOperation(Data.Stages.Stage nowStage)
        {
            stage = nowStage;

            Console.WriteLine("作戦開始\n\n");

            while (true)
            {
                ++turnCount;        //行動周回数の加算
                Console.WriteLine(turnCount + "周目\n");

                mapRenderer.DrawMap(stage.mapTiles, stage.setUnits);        //マップの描画
                DrawActionTurn();       //行動順の決定
                UnitAction();       //ユニットの行動
            }
        }

        public void UnitAction()
        {
            bool endActionSelect = false;        //行動選択終了フラグ
            bool moveSelect = false;        //移動選択フラグ

            for (int i = 0; i < actionOrder.Count; ++i)
            {
                if (actionOrder[i].playable == false) continue;     //unplayableなユニットは行動させない（一時的な措置）
                else
                {
                    endActionSelect = false;       //行動選択終了フラグの初期化
                    while (endActionSelect == false)        //行動選択が正常に終了するまでループ
                    {
                        switch ((Data.Enums.ActionType)menu.UnitActionMenu(actionOrder[i]))
                        {
                            case Data.Enums.ActionType.None:
                                {
                                    Console.WriteLine("待機します");
                                    endActionSelect = true;
                                    break;
                                }
                            case Data.Enums.ActionType.Move:
                                {
                                    while(true)
                                    {
                                        if(actionOrder[i].Move((Data.Enums.Direction)menu.UnitMoveMenu()) == true)
                                            break;
                                        else
                                            Console.WriteLine("その方向には移動できません。");
                                    }
                                    endActionSelect = true;
                                    break;
                                }
                            case Data.Enums.ActionType.Attack:
                                {
                                    while(true)
                                    {

                                    }

                                    break;
                                }
                            default: break;
                        }
                    }
                }

                util.ConsoleClear();       //コンソールのクリア
                mapRenderer.DrawMap(stage.mapTiles, stage.setUnits);
            }
            util.ConsoleClear();
        }

        //<summary>
        //マップ上のユニットの行動順を決定する関数。
        //ユニットの行動順は俊敏値を基にした乱数によって決定される。
        //</summary>
        public void DrawActionTurn()
        {
            actionOrder.Clear();        //行動順リストの初期化

            for (int i = 0; i < stage.setUnits.Length; ++i)
            {
                for(int j = 0; j < stage.setUnits[i].Count; ++j)
                {
                    stage.setUnits[i][j].ActionRandom();
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
        public int[] CheckMapTile(int x, int y)
        {
            int[] result = new int[2];      //{通行の可否(0,1), ユニットが存在するかどうか(0,1)}

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
            EndTileCheckLoop:

            return result;
        }

        //<summary>
        //ある座標を中心として指定した距離（マンハッタン距離）内のユニットをリストアップする関数。
        //なお探索中心座標のユニットは探索対象から除外される。
        //x...探索中心のx座標、y...探索中心のy座標、distance...探索する距離
        //</summary>
        public List<Data.Units.Unit> SearchUnits(int x, int y, int distance)
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
    }
}