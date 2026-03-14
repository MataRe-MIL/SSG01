namespace SSG01.UI
{
	using System;
    using System.Runtime.CompilerServices;

    public class Menu
	{
		private string input;


		public int StartMenu(Data.Stages.StageManager stageManager)
		{
			int selectedStage = 0;


			while (true)
			{
				Console.Clear();
				Console.ResetColor();

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
						return selectedStage;
					}
				}
			}
        }

		private void Error()
		{

		}
	}
}