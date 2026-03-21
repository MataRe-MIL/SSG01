namespace SSG01.Core
{
    using System;
    using System.Collections.Generic;

    public class Operation
    {
        UI.MapRenderer mapRenderer = new UI.MapRenderer();      //マップレンダラー起動
        Random rand = new Random();       //乱数生成インスタンスをロード


        Data.Stages.Stage stage;        //現在のステージデータ
        Data.Units.Unit nowActionUnit;      //現在行動中のユニット
        int turnCount = 0;       //ターン数


        public void StartOperation(Data.Stages.Stage nowStage)
        {
            stage = nowStage;


            Console.WriteLine("作戦開始\n\n");


            ++turnCount;
            mapRenderer.DrawMap(stage.mapTiles, stage.setUnits);        //マップの描画

            DrawActionTurn();       //行動順の決定
        }

        public void DrawActionTurn()
        {
            List<Data.Units.Unit> drawActionTurn = new List<Data.Units.Unit>();        //行動順決定乱数が最大値のユニットを格納するリスト
            int max = 0;        //マップ上の全ユニットの行動順決定乱数の最大値
            int temp = 0;       //行動順決定乱数の一時格納変数


            for (int i = 0; i < stage.setUnits.Length; ++i)
            {
                for (int j = 0; j < stage.setUnits[i].Count; ++j)
                {
                    temp = stage.setUnits[i][j].ActionRandom();

                    if(temp > max)
                    {
                        drawActionTurn.Clear();
                        max = temp;
                        drawActionTurn.Add(stage.setUnits[i][j]);
                    }
                    else if(temp == max)
                    {
                        drawActionTurn.Add(stage.setUnits[i][j]);
                    }
                }
            }

            if(drawActionTurn.Count == 1)
            {
                nowActionUnit = drawActionTurn[0];
            }
            else if(drawActionTurn.Count > 1)
            {
                nowActionUnit = drawActionTurn[rand.Next(0, drawActionTurn.Count)];
            }
        }

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