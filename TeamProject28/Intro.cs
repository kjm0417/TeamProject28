using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject28
{
    internal class Intro
    {
        public static string IntroScene()
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
