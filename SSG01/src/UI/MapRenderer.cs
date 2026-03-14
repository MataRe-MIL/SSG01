namespace SSG01.UI
{
	using System;

    public class MapRenderer
	{
		private int[][] mapTiles;

		public MapRenderer(int[][] MapTiles)
		{
			mapTiles = MapTiles;
		}

		public void DrawMap()
		{
			for(int i = 0; i < mapTiles.Length; ++i)
			{
				for(int j = 0; j < mapTiles[i].Length; ++j)
				{
					switch (mapTiles[i][j])
					{
						case 0:
							{
								Console.ResetColor();		//色を初期状態に
								Console.Write(" ");
								break;
							}
						case 1:
							{
								Console.BackgroundColor = ConsoleColor.Yellow;		//文字背景色を黄色に
								Console.ForegroundColor = ConsoleColor.Black;		//文字色を黒色に
								Console.ResetColor();
								break;
							}
						default:Console.Write("？"); break;
					}
				}
				Console.WriteLine();
			}
		}
	}
}