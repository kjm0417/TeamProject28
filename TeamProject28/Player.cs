using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TeamProject28.GameManager;
using static TeamProject28.SuperPower;

namespace TeamProject28
{
    [Serializable]
    public class Player 
    {
       
        public int level { get; set; }
        public string name { get; set; }
        public Job job { get; set; }
        public double baseIQ { get; set; }
        public double IQ { get; set; }
        public int baseFocus { get; set; }
        public int focus { get; set; }
        public int maxTime { get; set; }
        public int currentTime { get; set; }
        public int maxPassion { get; set; }
        public int currentPassion { get; set; }
        public int gold { get; set; }
        public int exp { get; set; }
        public List<Skill> skills { get; set; }


        public Player()
        {
            this.level = 1;
            this.name = "스파르타";
            this.job = Job.수강생;
            this.baseIQ = 10;
            this.IQ = 10;
            this.baseFocus = 5;
            this.focus = 5;
            this.maxTime = 100;
            this.currentTime = maxTime;
            this.maxPassion = 50;
            this.currentPassion = maxPassion;
            this.gold = 1500;
            this.exp = 0;
            skills = new List<Skill> {new SuperPower(), new DoubleAttack(), new AttackBuff(), new Heal()};
        }

        public enum Job { 수강생, 튜터, 담임매니저 }


        //플레이어가 스킬을 사용하기 위한 구현
        //스킬에 대한 배열 가져오기 skill가져와서 사용해야한다, 스킬 코르기 위한 int
        public void UseSkill(int skillindex, List<Monster> target) 
        {
            if (skillindex >= 0 && skillindex < skills.Count)
            {
                Skill selectedSkill = skills[skillindex];
                if (currentPassion >= selectedSkill.manaCost)
                {
                    currentPassion -= selectedSkill.manaCost;
                   
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"{selectedSkill.skillName} 스킬을 사용했습니다!");
                    selectedSkill.Use(this, target);
                }
                else
                {
                    Console.WriteLine("마나가 부족합니다.");
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("열정이 부족합니다.");
                }
            }
        }

        //플레이어가 전투중에 스킬을 사용


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
            Console.WriteLine("열정 : {0}", currentPassion);
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
