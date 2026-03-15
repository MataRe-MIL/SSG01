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
			UI.Menu menuRenderer = new UI.Menu();		//メニューレンダラーの起動
            Data.Stages.StageManager stageManager = new Data.Stages.StageManager();		//ステージマネージャーの起動

            nowStage = menuRenderer.StartMenu(stageManager);
			gamePhase = 1;


			Operation operation = new Operation(nowStage);		//オペレーション・システムの起動
        }


        public bool CheckMapTile(int[][] nowMapTiles, int x, int y, int target)
        {
            bool result = false;

            if (x < 0 || y < 0 || x >= nowMapTiles.Length || y >= nowMapTiles[x].Length || nowMapTiles[x][y] != target) result = false;
            else if (nowMapTiles[x][y] == target) result = true;
            else result = false;

            return result;
        }


    }
}