namespace SSG01.UI
{
	using System;

    public class Menu
	{
		private string input;
		Core.Utilities util;


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
				Console.Write("\n>>");

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

		private void Error()
		{

		}
	}
}