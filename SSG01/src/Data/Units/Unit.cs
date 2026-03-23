namespace SSG01.Data.Units
{
    using SSG01.Core;
    using System;

    public class Unit
	{
        private Core.Operation operation;       //現在のマップインスタンスをロード
		private static System.Random rand = new System.Random();        //乱数生成インスタンスをロード

        //=====ユニットの基本情報=====
        public bool playable;       //プレイヤーが操作可能なユニットかどうか
        public string unitName;     //ユニットの名前
		public int team;        //0：プレイヤー陣営、1：敵陣営

		public int mobility = 20;        //ユニットの俊敏性

		//=====ユニットの変数=====
        public int x = 0;
        public int y = 0;

        public int actionRandomValue;		//行動順決定乱数の出力値
		public int symbolNum;		//画面上のユニット識別記号番号


        public Unit(Core.Operation operation, int startPositionX, int startPositionY, int team, bool playable = false,  string unitName = "")
		{
			x = startPositionX;
			y = startPositionY;

			this.operation = operation;
			this.team = team;
			this.playable = playable;
			this.unitName = unitName;
        }


		//マップ上シンボルの描画
		public void SymbolRendering(int unitNum = 52)
		{
			if (unitNum < 52)
				symbolNum = unitNum;
			else if (unitNum == 52)
				unitNum = symbolNum;

            if (team == 0)
			{
				Console.BackgroundColor = ConsoleColor.Green;
				Console.ForegroundColor = ConsoleColor.Black;

                Console.Write((Data.Enums.UnitSymbols)unitNum);

				Console.ResetColor();
            }
			else if(team == 1)
			{
				Console.BackgroundColor = ConsoleColor.Red;
				Console.ForegroundColor = ConsoleColor.White;

                Console.Write((Data.Enums.UnitSymbols)unitNum);

				Console.ResetColor();
            }
		}


		//unplayableユニットの自律行動アルゴリズム
		public void AI()
		{

		}

		//ユニットの行動順決定乱数生成
		public void ActionRandom()
		{
            actionRandomValue = rand.Next(0, mobility);
        }

        //<summary>
        //ユニットの移動処理関数。
        //移動方向に応じて、マップ上の移動先タイルが移動可能かどうかを判定し、移動可能な場合はユニットの座標を更新する。
        //direction...移動方向、nowTurn...現在のターン数
        //</summary>
        public void Move(Data.Enums.Direction direction)
		{
			switch(direction)
			{
				case Enums.Direction.forward:
					{
						if (operation.CheckMapTile((x - 1), y, 1) == true) --x;
						break;
					}
				case Enums.Direction.backward:
					{
						if (operation.CheckMapTile((x + 1), y, 1) == true) ++x;
						break;
					}
				case Enums.Direction.right:
					{
						if(operation.CheckMapTile(x, (y + 1), 1) == true) ++y;
						break;
					}
				case Enums.Direction.left:
					{
						if (operation.CheckMapTile(x, (y - 1), 1) == true) --y;
						break;
					}
			}
		}
	}
}