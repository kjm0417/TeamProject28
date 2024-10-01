using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace TeamProject28
{
    public class Intro
    {
        public static void IntroScene()
        {
            string text = "Loading...";
            int windowWidth = Console.WindowWidth;
            int windowHeight = Console.WindowHeight;

            int leftPadding = (windowWidth - text.Length) / 2;
            int topPadding = windowHeight / 2;

            Console.SetCursorPosition(leftPadding, topPadding - 1);
            Console.WriteLine(text);

            text = "■";
            leftPadding = (windowWidth - text.Length) / 2;
            Console.SetCursorPosition(leftPadding - 10, topPadding + 2);

            for (int i = 0; i < 10; i++)
            {
                Console.Write(text);
                Thread.Sleep(500);
            }
            Console.Clear();

            // 책 상단
            Console.WriteLine("+" + new string('-', 20) + "+");
            // 책 페이지
            for (int i = 0; i < 15; i++)
            {
                if (i == 15 / 2)
                    Console.WriteLine("|" + new string(' ', 20 / 2 - 6) + "무한의 과제" + new string(' ', 20 / 2 - 5) + "|");
                else
                    Console.WriteLine("|" + new string(' ', 20) + "|");
            }
            // 책 하단
            Console.WriteLine("+" + new string('-', 20) + "+");
            Console.WriteLine("\n" + new string(' ', 20 / 2 - 8) + "Press To any key\n\n");
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            Console.Clear();
            
            return;
        }

        public static string CollectName()
        {
            Console.WriteLine("스파르타 던전에 오신 여러분 환영합니다.");
            Console.Write("원하시는 이름을 설정해주세요.\n>> ");
            string name = Console.ReadLine();
            while (name == null)
            {
                Console.WriteLine("다시 입력해주세요");
                name = Console.ReadLine();
            }
            Console.Clear();

            return name;
        }
    }
}
