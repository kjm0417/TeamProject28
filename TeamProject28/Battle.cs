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
                monsters.Add(monsterList[3]); //보스 몬스터 
            }
            else 
            {
                for (int i = 0; i < random.Next(1, 4); i++)
                {
                    monsters.Add(monsterList[random.Next(0, 3)]); //일반 몬스터 
                } 
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("공부!!\n");
            Console.ForegroundColor = ConsoleColor.White;
            BattleSelect();
        }


        public void BattleSelect()
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
            Console.WriteLine("TIME {0}/100\n", maxTime);

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
                    PlayerAttack();
                    break;
            }
        }

        public void PlayerAttack()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");

            for(int i =0; i<monsters.Count; i++)
            {
                Monster monster = monsters[i];
                Console.WriteLine($"\n{i+1}. Lv. {monster.level} {monster.name}  TIme {monster.time}");
            }

            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv. {level}  {job}");
            Console.WriteLine($"Time {maxTime}/{currentTime}");
            Console.WriteLine("\n0. 취소\n");

            Console.Write("대상을 선택해주세요.\n>>");

            int target = GameStart.instance.Input();

            if (target < 0 || target > monsters.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                target = GameStart.instance.Input();
            }
            else if (target ==0)
            {
                GameStart.instance.ActionSelect();
            }

            //선택한 몬스터 번호 받기
            Monster selectMonster = monsters[target - 1];

            if (selectMonster.time <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다.");
                Console.ForegroundColor = ConsoleColor.White;
                PlayerAttack();
            }
            else
            {
                // 공격력 계산 (10% 오차 적용)
                double variance = IQ * 0.1; //오차 계산
                double minAttack = Math.Ceiling(IQ - variance); // Math.Ceiling 하기 위해 사용 double일 때 사용가능
                double maxAttack = Math.Ceiling(IQ + variance);
                int finalAttackPower = random.Next((int)minAttack, (int)maxAttack + 1);

                // 몬스터 체력 감소
                int monsterCurrentTime = selectMonster.time;
                monsterCurrentTime = selectMonster.time - finalAttackPower;
;
                Console.Clear();
                Console.WriteLine("Battle!!!\n");
                Console.WriteLine($"{name}의 공격!");
                Console.WriteLine($"Lv. {selectMonster.level} {selectMonster.name} 을(를) 맞췄습니다.  [데미지 : {finalAttackPower}]");
                Console.WriteLine($"Lv. {selectMonster.level} {selectMonster.name}");
                Console.WriteLine($"Hp. {selectMonster.time} ->{(selectMonster.time<0?"Dead": monsterCurrentTime)}");

            }
        }

        // 몬스터와 플레이어의 상태를 다시 출력해주는 메서드
        public void ShowBattleStatus()
        {
            Console.WriteLine("**Battle 결과!!**\n");

            // 몬스터 상태 출력
            for (int i = 0; i < monsters.Count; i++)
            {
                var monster = monsters[i];
                if (monster.time > 0)
                {
                    Console.WriteLine("**{0}** Lv.{1} {2} HP {3}", i + 1, monster.level, monster.name, monster.time);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.WriteLine("**{0}** Lv.{1} {2} Dead", i + 1, monster.level, monster.name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

            Console.WriteLine("0. 다음");
        }
    }
}


