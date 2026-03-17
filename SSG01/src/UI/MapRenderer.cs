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

								if (units != null)
								{
									for (int i1 = 0; i1 < units.Length; ++i1)
									{
										for (int j1 = 0; j < units[i1].Count; ++j1)
										{
											if (units[i1][j1].x == i && units[i1][j1].y == j)
											{
												Console.Write("U");
												goto EndLoop;
											}
										}
									}
								}
								else
								{
									Console.Write(" ");
								}
							EndLoop:
								break;
							}
						case 1:
							{
								Console.BackgroundColor = ConsoleColor.Yellow;		//文字背景色を黄色に
								Console.ForegroundColor = ConsoleColor.Black;       //文字色を黒色に
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
	}
}