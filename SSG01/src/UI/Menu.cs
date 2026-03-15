namespace SSG01.UI
{
	using System;

    public class Menu
	{
		private string input;


		public Data.Stages.Stage StartMenu(Data.Stages.StageManager stageManager)
		{
			int selectedStage = 0;


			while (true)
			{
				Core.Utilities.ConsoleClear();

				Console.WriteLine("============================");
				Console.WriteLine("||戦役を選択してください。||");
				Console.WriteLine("============================\n");

				for (int i = 0; i < stageManager.stages.Count; ++i)
				{
					Console.WriteLine(stageManager.stages[i].id + ". " + stageManager.stages[i].name);
				}
				Console.WriteLine("\n番号(半角)");

				input = Console.ReadLine();
				selectedStage = int.Parse(input);

				for (int i = 0; i < stageManager.stages.Count; ++i)
				{
					if (stageManager.stages[i].id == selectedStage)
					{
						Core.Utilities.ConsoleClear();
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