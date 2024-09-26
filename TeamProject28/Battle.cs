using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TeamProject28.GameManager;

namespace TeamProject28
{
    internal class Battle : Player
    {
        public static int stage = 1;
        Random random = new Random();

        public static Monster easyMonster = new Monster(MonsterType.Easy);
        public static Monster normalMonster = new Monster(MonsterType.Normal);
        public static Monster hardMonster = new Monster(MonsterType.Hard);
        public static Monster bossMonster = new Monster(MonsterType.Boss);
        public Monster[] monsterList = { easyMonster, normalMonster, hardMonster, bossMonster };

        public List<Monster> monsters = new List<Monster>();

        public void Start()
        {
            monsters.Clear();
            if (stage % 5 == 0)
            {
                monsters.Add(monsterList[3]);
            }
            else 
            {
                for (int i = 0; i < random.Next(1, 4); i++)
                {
                    monsters.Add(monsterList[random.Next(0, 3)]);
                } 
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("공부!!\n");
            Console.ForegroundColor = ConsoleColor.White;
            ActionSelect();
        }


        public void ActionSelect()
        {
            // 몬스터 정보
            foreach (Monster monster in monsters)
            {
                Console.WriteLine("Lv.{0} {1}\t TIME {2}", monster.level, monster.name, monster.time);
            }
            Console.WriteLine();

            // 내정보
            Console.WriteLine("[내정보]");
            Console.WriteLine("Lv.{0}\t{1} ({2})", level, name, job);
            Console.WriteLine("TIME {0}/100\n", time);

            Console.WriteLine("1. 과제하기");
            Console.Write("\n원하시는 행동을 입력해주세요 입력해주세요\n >>");
            int input = GameStart.instance.Input();

            while (input < 1 || input > 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                input = GameStart.instance.Input();
            }

            switch (input)
            {
                case 1:
                    // 이 부분 바꿔주시면 돼요!
                    // 공격();
                    Console.WriteLine("공격");
                    break;
            }
        }
    }
}
