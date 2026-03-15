namespace SSG01.Data.Stages
{
	using System;

	public class Stage1 : Stage
	{
		public readonly string stageName = "Area S09 ～End Field～";

		public Stage1(int id, string name) : base(id, name)
		{
			this.mapTiles = new int[][]
				{
					new int[]{0,0,0,0,0,0,0,0,0},
					new int[]{0,1,1,1,1,1,1,1,0},
					new int[]{0,1,1,1,1,1,1,1,0},
					new int[]{0,1,0,0,0,0,0,1,0},
					new int[]{0,1,1,1,1,1,1,1,0},
					new int[]{0,1,1,1,1,1,1,1,0},
					new int[]{0,1,1,1,1,1,1,1,0}
				};

			List<Data.Units.Unit> units = new List<Units.Unit>()
			{

			};
		}
	}
}