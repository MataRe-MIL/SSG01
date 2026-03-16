namespace SSG01.Data.Stages
{
	using System;

	public class Stage1 : Stage
	{
		public readonly string stageName = "Area S09 ～End Field～";

		public Stage1(Core.Operation operation, int id, string name) : base(id, name)
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

			List<Units.Unit>[] setUnits = new List<Units.Unit>[]
			{
				new List<Units.Unit>
				{
					new Units.Unit(operation,1,1,"PMC",true)
				}
			};
		}
	}
}