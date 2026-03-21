namespace SSG01.Core
{
	using System;
	using System.Collections.Generic;

	public class Game
	{
		/*
		 * ＜gamePhase一覧＞
		 * 0:StartMenu
		 * 1:Operation
		 */
		public int gamePhase = 0;		//ゲームフェーズ
		public Data.Stages.Stage nowStage;			//現在実行中のステージ

		public Game()
		{
			Core.Utilities util = new Core.Utilities();     //ユーティリティの起動
            UI.Menu menuRenderer = new UI.Menu(util);       //メニューレンダラーの起動
			Core.Operation operation = new Operation();		//オペレーション・システムの起動
            Data.Stages.StageManager stageManager = new Data.Stages.StageManager(operation);        //ステージマネージャーの起動


            nowStage = menuRenderer.StartMenu(stageManager);
			gamePhase = 1;


			operation.StartOperation(nowStage);
        }
    }
}