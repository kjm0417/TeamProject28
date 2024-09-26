using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject28
{
    internal class ShowPlayerStats
    {
        public struct Player
        {
            public int level;
            public string name;
            public Job job;
            public int IQ;
            public int focus;
            public int time;
            public int gold;

            public Player()
            {
                this.level = 1;
                this.name = "Sparta";
                this.job = Job.수강생;
                this.IQ = 10;
                this.focus = 5;
                this.time = 100;
                this.gold = 1500;
            }
        }

        public enum Job { 수강생, 튜터, 담임매니저 }

        static Player player = new Player();
        public int Input()
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int i))
            {
                return i;
            }
            else
            {
                return -1;
            }
        }

        // 상태 보기
        public void PlayerStats()
        {
            // 이전 창 지우기
            Console.Clear();

            //상태창 띄우기
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("상태 보기");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("캐릭터의 정보가 표시됩니다.");
            Console.WriteLine();

            Console.WriteLine("Lv. {0}", player.level);
            Console.WriteLine("{0} ( {1} )", player.name, player.job);
            Console.WriteLine("IQ : {0}", player.IQ);
            Console.WriteLine("집중력 : {0}", player.focus);
            Console.WriteLine("시간 : {0}", player.time);
            Console.WriteLine("Gold : {0}", player.gold);
            Console.WriteLine();

            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            // 입력 받기
            int input = Input();
            while (input < 0 || input > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                input = Input();
            }

            switch (input)
            {
                case 0:
                    Console.WriteLine("완성");
                    return;
            }
        }
    }
}
