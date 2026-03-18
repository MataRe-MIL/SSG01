namespace SSG01.Data.Units
{
    using SSG01.Core;
    using System;

	public class Unit
	{
        private Core.Operation operation;       //現在のマップインスタンスをロード


        public int x = 0;
		public int y = 0;

		public char symbol = 'U';		//ユニットのシンボル
        public String team = "UNKNOWN";		//ユニットの所属チーム
		public bool playable = false;

		public Unit(Core.Operation operation, int startPositionX, int startPositionY, string team, bool playable)    //引数:オペレーション・システムインスタンス, 初期位置, 所属チーム, プレイアブル
		{
			x = startPositionX;
			y = startPositionY;

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
						if (operation.CheckMapTile((x - 1), y, 1) == true) --x;
						break;
					}
				case 'b':
					{
						if (operation.CheckMapTile((x + 1), y, 1) == true) ++x;
						break;
					}
				case 'r':
					{
						if(operation.CheckMapTile(x, (y + 1), 1) == true) ++y;
						break;
					}
				case 'l':
					{
						if (operation.CheckMapTile(x, (y - 1), 1) == true) --y;
						break;
					}
			}
		}
	}
}