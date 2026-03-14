namespace SSG01.Data.Units
{
    using SSG01.Core;
    using System;

	public class BaseUnit
	{
		private int positionX = 0;
		private int positionY = 0;

		private Game game;		//現在のマップインスタンスをロード


		public String team = "UNKNOWN";
		public bool playable = false;

		public BaseUnit(Game game, int startPositionX, int startPositionY, string team, bool playable)    //引数:現在のマップインスタンス, 初期位置, 所属チーム, プレイアブル
		{
			positionX = startPositionX;
			positionY = startPositionY;

			this.game = game;

			switch (team)
			{
				case "": break;
				default: this.team = team; break;
			}

			this.playable = playable;
		}


		//UnitAction

		public void Move(char direction)
		{
			switch(direction)
			{
				case 'f':
					{
						if (game.CheckMapTile((positionX - 1), positionY, 1) == true) --positionX;
						break;
					}
				case 'b':
					{
						if (game.CheckMapTile((positionX + 1), positionY, 1) == true) ++positionX;
						break;
					}
				case 'r':
					{
						if(game.CheckMapTile(positionX, (positionY + 1), 1) == true) ++positionY;
						break;
					}
				case 'l':
					{
						if (game.CheckMapTile(positionX, (positionY - 1), 1) == true) --positionY;
						break;
					}
			}
		}
	}
}