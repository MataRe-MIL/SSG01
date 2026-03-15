namespace SSG01.Core
{
	using System;

	public class Operation
	{
		UI.MapRenderer mapRenderer;		//マップレンダラー
		Data.Stages.Stage stage;		//現在のステージデータ

		public Operation(Data.Stages.Stage nowStage)
		{
			stage = nowStage;
			mapRenderer = new UI.MapRenderer(stage.mapTiles);		//マップレンダラー起動
        }
    }
}