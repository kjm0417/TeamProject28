using System.Numerics;
using TeamProject28.GameManager;
using static TeamProject28.Player;

namespace TeamProject28
{
    public class Program
    {
        static void Main(string[] args)
        {
            GameStart gameStart = new GameStart();
            Intro.IntroScene();
            gameStart.Load();
            gameStart.Start();
        }
    }
}
