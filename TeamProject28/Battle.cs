﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TeamProject28.GameManager;

namespace TeamProject28
{
    internal class Battle
    {
        public Player player;
        public static int stage = 1;
        Random random = new Random();

        //public static Monster easyMonster = new Monster(MonsterType.Easy);
        //public static Monster normalMonster = new Monster(MonsterType.Normal);
        //public static Monster hardMonster = new Monster(MonsterType.Hard);
        //public static Monster bossMonster = new Monster(MonsterType.Boss);
        //public Monster[] monsterList = { easyMonster, normalMonster, hardMonster, bossMonster };

        public List<Monster> monsters = new List<Monster>();

        public void Start()
        {
            player = GameStart.instance.player;
            monsters.Clear();
            if (stage % 5 == 0)
            {
                monsters.Add(new Monster(MonsterType.Boss)); //보스 몬스터 
            }
            else
            {
                int monsterCount = random.Next(1, 4);
                for (int i = 0; i < monsterCount; i++)
                {
                    monsters.Add(new Monster((MonsterType)random.Next(0, 3))); // 0부터 2까지의 몬스터 타입 중 랜덤 선택
                }
            }

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("공부!!\n");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Monster monster in monsters)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Lv.{monster.level} {monster.name} HP {monster.maxTime}");
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($"\n[내정보]\nLv.{player.level} {player.name} (전사)\nHP {player.currentTime}/{player.maxTime}\n");
            Console.WriteLine("1. 공격\n");

            // 전투 행동 선택
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int action = GameStart.instance.Input();

            // 행동에 따라 전투 시작
            if (action == 1)
            {
                BattleLoop(); // 턴제 전투 루프 시작
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }


        public void BattleLoop()
        {
            // 전투가 끝날 때까지 계속 반복
            while (true)
            {
                Console.Clear();
                // 플레이어 턴
                Console.WriteLine("플레이어 턴");
                PlayerAttack(); // 플레이어가 적을 공격
                Thread.Sleep(3000);


                // 적이 모두 죽었는지 확인
                bool allMonstersDead = monsters.All(monster => monster.currentTime <= 0);
                if (allMonstersDead)
                {
                    Console.Clear();
                    EndBattle(victory: true); // 승리
                    break;
                }

                // 적의 턴
                Console.WriteLine("적의 턴");
                MonsterAttack(); // 적이 플레이어를 공격

                Thread.Sleep(3000);
                // 플레이어가 죽었는지 확인
                if (player.currentTime <= 0)
                {
                    Console.Clear();
                    EndBattle(victory: false); // 패배
                    break;
                }

                ShowBattleStatus(); // 현재 상태 출력
            }
        }


        public void PlayerAttack()
        {
            Console.WriteLine("Battle!!");

            for (int i = 0; i < monsters.Count; i++)
            {
                Monster monster = monsters[i];
                Console.WriteLine($"\n{i + 1}. Lv. {monster.level} {monster.name}  TIme {monster.currentTime}");
            }

            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv. {player.level} {player.name} ({player.job})");
            Console.WriteLine($"Time {player.currentTime}/{player.maxTime}");
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
            else if (target == 0)
            {
                GameStart.instance.ActionSelect();
            }

            //선택한 몬스터 번호 받기
            Monster selectMonster = monsters[target - 1];

            if (selectMonster.currentTime <= 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다.");
                Console.ForegroundColor = ConsoleColor.White;
                PlayerAttack();
            }
            else
            {
                // 공격력 계산 (10% 오차 적용)
                double variance = player.IQ * 0.1; //오차 계산
                double minAttack = Math.Ceiling(player.IQ - variance); // Math.Ceiling 하기 위해 사용 double일 때 사용가능
                double maxAttack = Math.Ceiling(player.IQ + variance);
                int playerAttack = random.Next((int)minAttack, (int)maxAttack + 1);

                // 몬스터 체력 감소

                Console.Clear();
                Console.WriteLine("Battle!!!\n");
                Console.WriteLine($"{player.name}의 공격!");
                Console.WriteLine($"Lv. {selectMonster.level} {selectMonster.name} 을(를) 맞췄습니다.  [데미지 : {playerAttack}]");
                Console.WriteLine($"Lv. {selectMonster.level} {selectMonster.name}");
                Console.WriteLine($"Hp. {selectMonster.currentTime} ->{(selectMonster.currentTime- playerAttack < 0 ? "Dead" : selectMonster.currentTime- playerAttack)}");
                selectMonster.currentTime -= playerAttack;


                Console.Clear();
                Console.WriteLine("Battle!!!\n");
                Console.WriteLine($"{player.name}의 공격!");
                Console.WriteLine($"Lv. {selectMonster.level} {selectMonster.name}을(를) 맞췄습니다. [데미지 : {playerAttack}]");
                Console.WriteLine($"Lv. {selectMonster.level} {selectMonster.name}");
                Console.WriteLine($"HP {selectMonster.currentTime}/{selectMonster.maxTime}");
            }
        }

        public void MonsterAttack()
        {

            Console.Clear();
            Console.WriteLine("Battle!!");

            foreach (Monster monster in monsters)
            {
                double variance = monster.IQ * 0.1; //오차 계산
                double minAttack = Math.Ceiling(monster.IQ - variance); // Math.Ceiling 하기 위해 사용 double일 때 사용가능
                double maxAttack = Math.Ceiling(monster.IQ + variance);
                int mosterAttack = random.Next((int)minAttack, (int)maxAttack + 1);

                if (monster.currentTime > 0) // 살아있는 적만 공격
                {
                    player.currentTime -= mosterAttack;

                    Console.WriteLine($"Lv. {monster.level} {monster.name} 이(가) 공격했습니다! [데미지: {mosterAttack}]");
                    Console.WriteLine($"플레이어의 남은 TIME: {player.currentTime}/{player.maxTime}");

                    if (player.currentTime <= 0)
                    {
                        Console.WriteLine($"{player.name}은(는) 죽었습니다...");
                    }
                }
            }


        }




        public void EndBattle(bool victory)
        {
            Console.WriteLine("Battle!! - Result\n");

            if (victory)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Victory\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"학원에서 과제 {monsters.Count}마리를 잡았습니다.\n");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Lose\n");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine($"Lv.{player.level} {player.name}");
            Console.WriteLine($"HP {player.maxTime} -> {player.currentTime}\n");
            Console.WriteLine("0. 다음\n");

            int input = GameStart.instance.Input();
            if (input == 0)
            {
                if (!victory)
                {
                    //GameOver();
                }
                else
                {
                    GameStart.instance.ActionSelect(); // 승리 후 다음 선택
                }
            }
            Console.Clear();
        }


        public void ShowBattleStatus()
        {
            Console.WriteLine("현재 전투 상황:\n");

            foreach (Monster monster in monsters)
            {
                if (monster.currentTime > 0)
                {
                    Console.WriteLine($"{monster.name} (Time {monster.currentTime})");
                }
                else
                {
                    Console.WriteLine($"{monster.name}는 죽었습니다.");
                }
            }

            Console.WriteLine($"\n{player.name}의 HP: {player.currentTime}/{player.maxTime}");
        }

        // 게임 오버 처리
        public void GameOver()
        {
            Console.Clear();
            Console.WriteLine("플레이어가 죽었습니다.!");
            GameStart.instance.ActionSelect();
            // 게임 오버 후 처리 로직 추가
        }

    }
}


