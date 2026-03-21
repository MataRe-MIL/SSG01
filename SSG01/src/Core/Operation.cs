namespace SSG01.Core
{
    using System;
    using System.Collections.Generic;

    public class Operation
    {
        UI.MapRenderer mapRenderer = new UI.MapRenderer();      //マップレンダラー起動
        Random rand = new Random();       //乱数生成インスタンスをロード


        Data.Stages.Stage stage;        //現在のステージデータ
        List<Data.Units.Unit> actionOrder = new List<Data.Units.Unit>();       //ユニット行動順リスト
        Data.Units.Unit nowActionUnit;      //現在行動中のユニット
        int turnCount = 0;       //行動周回数


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