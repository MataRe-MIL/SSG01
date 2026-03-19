namespace SSG01.UI
{
	using System;

    public class MapRenderer
	{
        private int[][] mapTiles;

		public void DrawMap(int[][] mapTiles, List<Data.Units.Unit>[] units = null)
		{
			this.mapTiles = mapTiles;

            for (int i = 0; i < mapTiles.Length; ++i)
			{
				for(int j = 0; j < mapTiles[i].Length; ++j)
				{
					switch (mapTiles[i][j])
					{
						case 0:
							{
								Console.ResetColor();       //色を初期状態に

								if(units != null)
									UnitRenderer(units,i,j);
								else
									Console.Write(" ");
								break;
							}
						case 1:
							{
								Console.BackgroundColor = ConsoleColor.White;		//文字背景色を黄色に
								Console.ForegroundColor = ConsoleColor.Black;       //文字色を黒色に
								if (units != null)
									UnitRenderer(units, i, j);
								else
									Console.Write(" ");
								Console.ResetColor();
								break;
							}
						default:Console.Write("？"); break;
					}
				}
				Console.WriteLine();
			}
		}

		public void UnitRenderer(List<Data.Units.Unit>[] units, int checkX, int checkY)
		{
			for (int i = 0; i < units.Length; ++i)
			{
                for (int j = 0; j < units[i].Count; j++)
                {
					if (units[i][j].x == checkX && units[i][j].y == checkY)
					{
						units[i][j].SymbolRendering(j);
						goto EndLoop;
					}
                }
            }
			Console.Write(" ");
			EndLoop:;
		}
	}
}