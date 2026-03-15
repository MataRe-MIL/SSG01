namespace SSG01.Data.Stages
{
	using System;

    public class StageManager
	{
        public List<Data.Stages.Stage> stages = new List<Data.Stages.Stage>();		//ステージのリスト

        public StageManager()
        {
            stages.Add(new Stage1(1, "Area S09 ～End Field～"));
        }
    }
}