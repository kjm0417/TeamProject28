using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TeamProject28.GameManager;

namespace TeamProject28
{
    internal class Player 
    {
       
        public int level;
        public string name;
        public Job job;
        public double IQ;
        public int focus;
        public int maxTime;
        public int currentTime;
        public int gold;
        public int exp;
        public int mp;

        public Player()
        {
            this.level = 1;
            this.name = "스파르타";
            this.job = Job.수강생;
            this.IQ = 10;
            this.focus = 5;
            this.maxTime = 100;
            this.currentTime = maxTime;
            this.gold = 1500;
            this.exp = 0;
            this.mp = 50;
        }

        public enum Job { 수강생, 튜터, 담임매니저 }


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

            Console.WriteLine("Lv. {0}", level);
            Console.WriteLine("{0} ( {1} )", name, job);
            Console.WriteLine("exp : {0}", exp);
            Console.WriteLine("IQ : {0}", IQ);
            Console.WriteLine("집중력 : {0}", focus);
            Console.WriteLine("시간 : {0}", currentTime);
            Console.WriteLine("마나 : {0}", mp);
            Console.WriteLine("Gold : {0}", gold);
            Console.WriteLine();

            Console.WriteLine("0. 나가기");
            Console.WriteLine();

            //입력 받기
            int input = GameStart.instance.Input();
            while (input < 0 || input > 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                input = GameStart.instance.Input();
            }

            switch (input)
            {
                case 0:
                    Console.Clear();
                    GameStart.instance.Start();
                    
                    return;
            }

            Console.Clear();
        }

    }
}
