using System.Security.Cryptography.X509Certificates;

namespace SSG01
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start Loading Game");

            Core.Game game = new Core.Game();       //メインゲームインスタンスを起動

            /*
            int[][] mapTiles =
            {
                new int[]{0,0,0,0,0,0,0,0,0},
                new int[]{0,1,1,1,1,1,1,1,0},
                new int[]{0,1,1,1,1,1,1,1,0},
                new int[]{0,1,0,0,0,0,0,1,0},
                new int[]{0,1,1,1,1,1,1,1,0},
                new int[]{0,1,1,1,1,1,1,1,0},
                new int[]{0,1,1,1,1,1,1,1,0}
            };
            UI.MapRenderer map = new UI.MapRenderer(mapTiles);
            map.DrawMap();
            */
        }
    }
}
