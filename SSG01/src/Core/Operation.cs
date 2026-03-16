namespace SSG01.Core
{
	using System;

	public class Operation
	{
		UI.MapRenderer mapRenderer = new UI.MapRenderer();      //マップレンダラー起動
		Data.Stages.Stage stage;        //現在のステージデータ


		public void StartOperation(Data.Stages.Stage nowStage)
		{
			stage = nowStage;

			mapRenderer.DrawMap(stage.mapTiles);        //マップの描画

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