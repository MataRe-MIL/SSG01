namespace SSG01.Core
{
    using System;
    using System.Collections.Generic;

    public class Operation
    {
        Core.Utilities util = new Core.Utilities();     //ユーティリティの起動
        UI.MapRenderer mapRenderer = new UI.MapRenderer();      //マップレンダラー起動
        UI.Menu menu;     //メニュー起動
        Random rand = new Random();       //乱数生成インスタンスをロード


        Data.Stages.Stage stage;        //現在のステージデータ
        List<Data.Units.Unit> actionOrder = new List<Data.Units.Unit>();       //ユニット行動順リスト
        Data.Units.Unit nowActionUnit;      //現在行動中のユニット
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
                                    actionOrder[i].Move((Data.Enums.Direction)menu.UnitMoveMenu(actionOrder[i]));
                                    endActionSelect = true;
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
        //マップタイルが特定の値であればtrue、そうでなければfalseを返す。
        //x...マップタイルのx座標、y...マップタイルのy座標、target...特定の値
        //</summary>
        public bool CheckMapTile(int x, int y, int target)
        {
            bool result = false;

            if (x < 0 || y < 0 || x >= stage.mapTiles.Length || y >= stage.mapTiles[x].Length || stage.mapTiles[x][y] != target) result = false;
            else if (stage.mapTiles[x][y] == target) result = true;
            else result = false;

            return result;
        }
    }
}