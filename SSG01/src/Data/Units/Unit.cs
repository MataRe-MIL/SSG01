namespace SSG01.Data.Units
{
    using SSG01.Core;
    using System;

    public class Unit
	{
        private Core.Operation operation;       //現在のマップインスタンスをロード
        private Core.Utilities util = new Core.Utilities();     //ユーティリティの起動
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
		public void symbolRendering(int unitNum = 52)
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
		public void actionRandom()
		{
            actionRandomValue = rand.Next(0, mobility);
        }

        public void unitAction(UI.Menu menu)
        {
            bool endActionSelect = false;        //行動選択終了フラグ
            bool moveSelect = false;        //移動選択フラグ

            if (playable == false)
            {
                Console.Write("アンプレイアブルの操作");
            }
            else
            {
                endActionSelect = false;       //行動選択終了フラグの初期化
                while (endActionSelect == false)        //行動選択が正常に終了するまでループ
                {
                    switch((Data.Enums.ActionType)menu.UnitActionMenu(this))
                    {
                        case Data.Enums.ActionType.None:
                            {
                                endActionSelect = noneAction();
                                break;
                            }
                        case Data.Enums.ActionType.Move:
                            {
                                while(true)
                                {
                                    if (move(menu) == true)
                                    {
                                        endActionSelect = true;
                                        break;
                                    }
                                    else
                                        Console.WriteLine("その方向には移動できません。");
                                }
                                break;
                            }
                        case Data.Enums.ActionType.Attack:
                        default: break;
                    }
                }
            }
        }

        private bool noneAction()
        {
            Console.WriteLine("待機します．");
            return true;
        }

        //<summary>
        //ユニットの移動処理関数。
        //移動方向に応じて、マップ上の移動先タイルが移動可能かどうかを判定し、移動可能な場合はユニットの座標を更新する。
        //direction...移動方向、nowTurn...現在のターン数
        //</summary>
        private bool move(UI.Menu menu)
        {
            int[] checkPosi = new int[2] {x, y};       //移動先タイルの座標を保管する．(x, y)で表される．

            while (true)
            {
                switch ((Data.Enums.Direction)menu.UnitMoveMenu())
                {
                    case Enums.Direction.forward:
                        {
                            checkPosi[0] = x - 1;
                            break;
                        }
                    case Enums.Direction.backward:
                        {
                            checkPosi[0] = x + 1;
                            break;
                        }
                    case Enums.Direction.right:
                        {
                            checkPosi[1] = y + 1;
                            break;
                        }
                    case Enums.Direction.left:
                        {
                            checkPosi[1] = y - 1;
                            break;
                        }
                    default:
                        {
                            return false;
                        }
                }

                if (operation.checkMapTile(checkPosi[0], checkPosi[1])[0] == 1 && operation.checkMapTile(checkPosi[0], checkPosi[1])[1] == 0)
                {
                    x = checkPosi[0];
                    y = checkPosi[1];
                    return true;
                }
                else
                {
                    Console.WriteLine("その方向には移動できません。");
                    return false;
                }
            }
		}
	}
}