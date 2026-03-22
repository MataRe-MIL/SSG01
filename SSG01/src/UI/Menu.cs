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


			while (true)
			{
				util.ConsoleClear();

				Console.WriteLine("============================");
				Console.WriteLine("||戦役を選択してください。||");
				Console.WriteLine("============================\n");

				for (int i = 0; i < stageManager.stages.Count; ++i)
				{
					Console.WriteLine(stageManager.stages[i].id + ". " + stageManager.stages[i].name);
				}
				Console.Write("\n>> ");

				input = Console.ReadLine();
				selectedStage = int.Parse(input);

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

		public int UnitActionMenu(Data.Units.Unit unit)
		{
			int selectedAction = 0;

            Console.Write("\n||現在 "); unit.SymbolRendering(); Console.WriteLine(unit.unitName + " が行動可能です。行動を選択してください。||");
			Console.WriteLine("1.移動");
			Console.Write("\n>> ");

			input = Console.ReadLine();
			selectedAction = int.Parse(input);

			return selectedAction;
        }

		public int UnitMoveMenu(Data.Units.Unit unit)
		{
			int selectedDirection;

			Console.WriteLine("\n||移動する方向を選択してください。||");
			Console.WriteLine("1.上");
			Console.WriteLine("2.右");
			Console.WriteLine("3.左");
			Console.WriteLine("4.下");
			Console.Write("\n>> ");

			input = Console.ReadLine();
			selectedDirection = int.Parse(input);

			return selectedDirection;
        }
	}
}