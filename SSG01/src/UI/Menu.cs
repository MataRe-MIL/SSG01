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

            Console.Write("\n||現在 "); unit.symbolRendering(); Console.WriteLine(unit.unitName + " が行動可能です。行動を選択してください。||");
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

		public void AttackMenu(Core.Operation operation, Data.Units.Unit unit, int x, int y, int distance)
		{
			List<Data.Units.Unit> targetUnits = operation.searchUnits(x, y, distance);
			for(int i = 0; i < targetUnits.Count; ++i)
			{
				if (targetUnits[i].team == 0)
					targetUnits.Remove(targetUnits[i]);
			}

			Console.WriteLine("\n||現在"); unit.symbolRendering(); Console.Write("が行動可能です。攻撃する対象を選択してください。||");

		}
	}
}