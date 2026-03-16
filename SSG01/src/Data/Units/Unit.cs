namespace SSG01.Data.Units
{
    using SSG01.Core;
    using System;

	public class Unit
	{
		private int positionX = 0;
		private int positionY = 0;

		private Core.Operation operation;		//現在のマップインスタンスをロード


		public String team = "UNKNOWN";
		public bool playable = false;

		public Unit(Core.Operation operation, int startPositionX, int startPositionY, string team, bool playable)    //引数:オペレーション・システムインスタンス, 初期位置, 所属チーム, プレイアブル
		{
			positionX = startPositionX;
			positionY = startPositionY;

			this.operation = operation;

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
						if (operation.CheckMapTile((positionX - 1), positionY, 1) == true) --positionX;
						break;
					}
				case 'b':
					{
						if (operation.CheckMapTile((positionX + 1), positionY, 1) == true) ++positionX;
						break;
					}
				case 'r':
					{
						if(operation.CheckMapTile(positionX, (positionY + 1), 1) == true) ++positionY;
						break;
					}
				case 'l':
					{
						if (operation.CheckMapTile(positionX, (positionY - 1), 1) == true) --positionY;
						break;
					}
			}
		}
	}
}