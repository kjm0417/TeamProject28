using System.Numerics;
using TeamProject28.GameManager;
using static TeamProject28.Player;

namespace TeamProject28
{
    internal class Program
    {
        public static List<Quest> list_Quest = new List<Quest>();

        static void Main(string[] args)
        {
            GameStart gameStart = new GameStart();
            gameStart.player.name = Intro.IntroScene();
            gameStart.Start();


            list_Quest.Add(new Quest1());
        }

        
    }
}
