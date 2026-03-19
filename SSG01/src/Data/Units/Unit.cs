namespace SSG01.Data.Units
{
    using SSG01.Core;
    using System;
    using System.Diagnostics.SymbolStore;

    public class Unit
	{
        private Core.Operation operation;       //現在のマップインスタンスをロード


        public int x = 0;
		public int y = 0;

		public char symbol = 'U';
		public bool playable;       //プレイヤーが操作可能なユニットかどうか
        public string unitName;     //ユニットの名前
		public int team;		//0：プレイヤー陣営、1：敵陣営

        public Unit(Core.Operation operation, int startPositionX, int startPositionY, int team, bool playable = false,  string unitName = "")
		{
			x = startPositionX;
			y = startPositionY;

			this.operation = operation;
			this.team = team;
			this.playable = playable;
			this.unitName = unitName;
        }

		public void SymbolRendering(int unitNum)
		{
			if(team == 0)
			{
				Console.BackgroundColor = ConsoleColor.Green;
				Console.ForegroundColor = ConsoleColor.Black;

				Console.Write((UnitSymbols)unitNum);

				Console.ResetColor();
            }
			else if(team == 1)
			{
				Console.BackgroundColor = ConsoleColor.Red;
				Console.ForegroundColor = ConsoleColor.White;

				Console.Write((UnitSymbols)unitNum);

				Console.ResetColor();
            }
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