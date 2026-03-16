namespace SSG01.Data.Stages
{
	using System;

	public class Stage
	{
		public int id = 0;
		public string name = "";

		public int[][] mapTiles;
		public Core.Operation operation;

		public Stage(int id, string name)
		{
			this.id = id;
			this.name = name;
		}
	}
}