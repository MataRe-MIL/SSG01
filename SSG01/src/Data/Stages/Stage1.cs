namespace SSG01.Data.Stages
{
	using System;

	public class Stage1 : Stage
	{
		public Stage1(Core.Operation operation, int id, string name) : base(id, name)
		{
			this.mapTiles = new int[][]
				{
					new int[]{0,1,0,0,0,0,0,0,0},
					new int[]{0,1,1,1,1,1,1,1,0},
					new int[]{0,1,1,1,1,1,1,1,0},
					new int[]{0,1,0,0,1,0,0,1,1},
					new int[]{0,1,1,1,1,1,1,1,1},
					new int[]{0,1,1,1,0,1,1,1,0},
					new int[]{0,1,1,1,0,1,1,1,0}
				};

			this.setUnits = new List<Units.Unit>[]
			{
				new List<Units.Unit>
				{
					new Units.Unit(operation,1,1,0,true,"捜査小隊"),		//operationインスタンス,開始位置x,開始位置y,敵味方(0 or 1),プレイアブルかどうか,ユニット名
                    new Units.Unit(operation,3,1,1,true,"捜査小隊"),

                }
            };
		}
	}
}