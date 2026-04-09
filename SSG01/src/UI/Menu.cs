namespace SSG01.UI
{
	using System;

    public class Menu
	{
		private string input;
		Core.Utilities util;		//utilitiesインスタンスをロード


		public Menu(Core.Utilities util)
		{
			this.util = util;
        }


        public Data.Stages.Stage StartMenu(Data.Stages.StageManager stageManager)
		{
			int selectedStage = 0;


			util.ConsoleClear();

			Console.WriteLine("============================");
			Console.WriteLine("||戦役を選択してください。||");
			Console.WriteLine("============================\n");

			for (int i = 0; i < stageManager.stages.Count; ++i)
			{
				Console.WriteLine(stageManager.stages[i].id + ". " + stageManager.stages[i].name);
			}

			while (true)
			{
				Console.Write("\n>> ");
				input = Console.ReadLine();

				if (int.TryParse(input, out selectedStage))
				{
					for (int i = 0; i < stageManager.stages.Count; ++i)
					{
						if (stageManager.stages[i].id == selectedStage)
						{
							util.ConsoleClear();
							return stageManager.stages[i];
						}
					}
				}
			}
        }

		public int UnitActionMenu(Data.Units.Unit unit)
		{
			int selectedAction = 0;

            Console.Write("\n||現在 "); unit.SymbolRendering(); Console.WriteLine(unit.unitName + " が行動可能です。行動を選択してください。||");
			Console.WriteLine("0.待機");
			Console.WriteLine("1.移動");
			Console.WriteLine("2.攻撃");

			while (true)
			{
				Console.Write("\n>> ");
				input = Console.ReadLine();
				if(int.TryParse(input, out selectedAction))
					return selectedAction;
			}
        }

		public int UnitMoveMenu()
		{
			int selectedDirection;

			Console.WriteLine("\n||移動する方向を選択してください。||");
			Console.WriteLine("1.上");
			Console.WriteLine("2.右");
			Console.WriteLine("3.左");
			Console.WriteLine("4.下");

			while (true)
			{
				Console.Write("\n>> ");
				input = Console.ReadLine();
				if (int.TryParse(input, out selectedDirection))
					return selectedDirection;
			}
        }

		public int UnitAttackMenu(Core.Operation operation, Data.Stages.Stage stage, Data.Units.Unit unit)
		{
			List<Data.Units.Unit> attackableUnits = operation.CheckMapTiles(unit.x, unit.y, unit.attackRange);
			int counter = 1;
			int selectAttackUnit;

			Console.WriteLine("\n||攻撃対象を選択してください。||");
			for(int i = 0; i < attackableUnits.Count; ++i)
			{
				if(attackableUnits[i].team == 0)
				{
					Console.Write(counter + "."); attackableUnits[i].SymbolRendering(); Console.WriteLine(attackableUnits[i].unitName);
					++counter;
				}
			}


			counter = 0;
			while(true)
			{
				Console.Write(">> ");
				input = Console.ReadLine();
				if(int.TryParse(input, out selectAttackUnit))
				{
					for(int i = 0; i < attackableUnits.Count; ++i)
					{
						if(attackableUnits[i].team == 0)
						{
							++counter;
							if(counter == selectAttackUnit)
							{

							}
						}
					}
				}
			}

			return selectAttackUnit;
		}
	}
}