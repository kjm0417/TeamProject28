using System;
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
        public Give give;
        public static int stage = 1;
        Random random = new Random();

        public List<Monster> monsters = new List<Monster>();

        public void Start()
        {
            player = GameStart.instance.player;
            give = new Give();
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

            BattleLoop();

 
        }


        public void UseSkillMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n사용할 스킬을 선택하세요:\n");

                for (int i = 0; i < GameStart.instance.skills.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {GameStart.instance.skills[i].skillName} (Cost: {GameStart.instance.skills[i].manaCost} 열정) - {GameStart.instance.skills[i].skillDescription}");
                }
                Console.WriteLine("0. 취소");

                int skillIndex = GameStart.instance.Input() - 1;

                if (skillIndex == -1)
                {
                    PlayerTurn(); // 취소하면 PlayerTurn으로 돌아감
                    break;
                }
                else if (skillIndex >= 0 && skillIndex < GameStart.instance.skills.Count)
                {
                    var skill = GameStart.instance.skills[skillIndex];

                    if (skill.isSelfBuff)
                    {
                        // 자기 자신에게 버프 적용
                        bool skillUsed = player.UseSkill(skillIndex, new List<Monster>());
                        if (!skillUsed)
                        {
                            WaitForNextStep();
                            PlayerTurn(); // 스킬 사용 실패 시 PlayerTurn으로 돌아감
                            break;
                        }
                        player.UseSkill(skillIndex, new List<Monster>());
                        Console.WriteLine($"{player.name}에게 {skill.skillName}이(가) 적용되었습니다.");
                        WaitForNextStep();
                    }
                    else if (skill.numberPerson == 1)
                    {
                        // 단일 대상 스킬
                        SelectSingleTarget(skillIndex);
                        WaitForNextStep();
                    }
                    else if (skill.numberPerson == 2)
                    {
                        // 다수 대상 스킬
                        SelectMultipleTargets(skillIndex, skill.numberPerson);
                        WaitForNextStep();
                    }

                    // 스킬 사용 후 상태창 출력
                    Console.Clear();
                    ShowBattleStatus();
                    WaitForNextStep();

                    // 적이 죽었는지 확인
                    if (monsters.All(monster => monster.currentTime <= 0))
                    {
                        EndBattle(victory: true);
                        break;
                    }

                    // 스킬 사용 후 적의 공격
                    //MonsterAttack();


                    break; // 적 공격 후 턴 종료
                }

                Console.WriteLine("잘못된 선택입니다. 다시 시도해주세요.");
            }
        }

        public void SelectSingleTarget(int skillIndex)
        {


            while (true)
            {
                Console.WriteLine("\n공격할 대상을 선택하세요:\n");

                for (int i = 0; i < monsters.Count; i++)
                {
                    if (monsters[i].currentTime > 0)
                    {
                        Console.WriteLine($"{i + 1}. Lv. {monsters[i].level} {monsters[i].name} (HP: {monsters[i].currentTime})");
                    }
                }

                int targetIndex = GameStart.instance.Input() - 1;

                if (targetIndex >= 0 && targetIndex < monsters.Count && monsters[targetIndex].currentTime > 0)
                {
                    var targetMonster = monsters[targetIndex];
                    bool skillUsed = player.UseSkill(skillIndex, new List<Monster> { targetMonster });

                    if (!skillUsed)
                    {
                        WaitForNextStep();
                        PlayerTurn(); // 스킬 사용 실패 시 PlayerTurn으로 돌아감
                        break;
                    }

                    if (targetMonster.currentTime <= 0)
                    {
                        Console.WriteLine($"Lv. {targetMonster.level} {targetMonster.name}은(는) 죽었습니다.");
                        targetMonster.currentTime = 0;
                    }
                    else
                    {
                        Console.WriteLine($"Hp. {targetMonster.currentTime}/{targetMonster.maxTime}");
                    }

                    break; // 타겟 선택이 완료되면 루프를 종료
                }
                else
                {
                    Console.WriteLine("잘못된 선택이거나 이미 죽은 적입니다. 다시 선택하세요.");
                }
            }
        }

        public void SelectMultipleTargets(int skillIndex, int targetCount)
        {
            List<Monster> selectedMonsters = new List<Monster>();

            int aliveMonstersCount = monsters.Count(m => m.currentTime > 0);
            if (aliveMonstersCount < targetCount) //몬스터 수가 부족하면 스킬 사용 불가 처리
            {
                Console.WriteLine($"이 스킬은 {targetCount}명 이상의 적에게만 사용할 수 있습니다. 다른 스킬을 선택하세요.");
                WaitForNextStep();
                PlayerTurn(); // 스킬 사용 실패 시 PlayerTurn으로 돌아감
                return;
            }

            for (int t = 0; t < targetCount; t++)
            {
                while (true)
                {
                    Console.WriteLine("\n공격할 대상을 선택하세요:\n");
                    for (int i = 0; i < monsters.Count; i++)
                    {
                        if (!selectedMonsters.Contains(monsters[i]) && monsters[i].currentTime > 0) //선택이 안된 몬스터 체력이 0보다 높은
                        {
                            Console.WriteLine($"{i + 1}. Lv. {monsters[i].level} {monsters[i].name} (HP: {monsters[i].currentTime})");
                        }
                    }

                    int targetIndex = GameStart.instance.Input() - 1;

                    if (targetIndex >= 0 && targetIndex < monsters.Count && monsters[targetIndex].currentTime > 0 && !selectedMonsters.Contains(monsters[targetIndex]))
                    {
                        selectedMonsters.Add(monsters[targetIndex]);
                        break; // 유효한 선택일 경우 루프 종료
                    }
                    else
                    {
                        Console.WriteLine("잘못된 선택이거나 이미 선택된 적입니다. 다시 선택하세요.");
                    }
                }
            }

            bool skillUsed = player.UseSkill(skillIndex, selectedMonsters);

            if (!skillUsed)
            {
                WaitForNextStep();
                PlayerTurn(); // 스킬 사용 실패 시 PlayerTurn으로 돌아감
                return;
            }

            foreach (var targetMonster in selectedMonsters)
            {
                if (targetMonster.currentTime <= 0)
                {
                    Console.WriteLine($"Lv. {targetMonster.level} {targetMonster.name}은(는) 죽었습니다.");
                    targetMonster.currentTime = 0;
                }
                else
                {
                    Console.WriteLine($"Hp. {targetMonster.currentTime}/{targetMonster.maxTime}");
                }
            }
        }


        //battle 관련 토글
        public void BattleLoop()
        {
            while (true)
            {
                Console.Clear();
                // 플레이어 턴
                PlayerTurn();

                // 적이 모두 죽었는지 확인
                if (monsters.All(monster => monster.currentTime <= 0))
                {
                    EndBattle(victory: true); // 승리
                    break;
                }

                //// 적의 턴
                MonsterAttack();

                // 플레이어가 죽었는지 확인
                if (player.currentTime <= 0)
                {
                    EndBattle(victory: false); // 패배
                    break;
                }

                ShowBattleStatus(); // 전투 상태 출력
            }
        }


        public void PlayerTurn()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("공부!!\n");
            Console.ForegroundColor = ConsoleColor.White;
            foreach (Monster monster in monsters)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine($"Lv.{monster.level} {monster.name} TIme {(monster.currentTime<=0?"Dead": monster.currentTime)}");
            }
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine($"\n[내정보]\nLv.{player.level} {player.name} {player.job}\nTime {player.currentTime}/{player.maxTime}\n열정 {player.currentPassion}/{player.maxPassion} ");
            Console.WriteLine("1. 공격");
            Console.WriteLine("2. 스킬\n");

            // 전투 행동 선택
            Console.WriteLine("원하시는 행동을 입력해주세요.");
            Console.Write(">> ");
            int action = GameStart.instance.Input();

            // 행동에 따라 전투 시작
            while (action < 1 || action > 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다.");
                Console.ForegroundColor = ConsoleColor.White;
                action = GameStart.instance.Input();
            }

            switch (action)
            {
                case 1:
                    PlayerAttack();
                    break;
                case 2:
                    UseSkillMenu();
                    break;
            }

            // 적이 모두 죽었는지 확인
            if (monsters.All(monster => monster.currentTime <= 0))
            {
                EndBattle(victory: true);
            }

            // 플레이어가 죽었는지 확인
            if (player.currentTime <= 0)
            {
                EndBattle(victory: false);
            }
        }
        

        public void PlayerAttack()
        {
            Console.Clear();
            Console.WriteLine("Battle!!");

            for (int i = 0; i < monsters.Count; i++)
            {
                Monster monster = monsters[i];
                Console.WriteLine($"\n{i + 1}. Lv. {monster.level} {monster.name}  TIme {(monster.currentTime<=0?"Dead": monster.currentTime)}");
            }

            Console.WriteLine("\n[내정보]");
            Console.WriteLine($"Lv. {player.level} {player.name} ({player.job})");
            Console.WriteLine($"Time {player.currentTime}/{player.maxTime}");
            Console.WriteLine($"열정 {player.currentPassion}/{player.maxPassion}");
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
            //target은 1부터 인데 배열은 0부터여서 
            Monster selectMonster = monsters[target - 1];

            if (selectMonster.currentTime <= 0) //죽은 몬스터 선택했을 때 다시 입력
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

                //치명타 공격
                int critical = random.Next(0, 100);
                int criticalAttack = playerAttack * 160 / 100;

                //공격 미스
                int miss = random.Next(0, 100);

                if(miss<10)
                {
                    Console.Clear();
                    Console.WriteLine("Battle!!!\n");
                    Console.WriteLine($"{player.name}의 공격!");
                    Console.WriteLine($"Lv {selectMonster.level} {selectMonster.name} 을(를) 공격했지만 아무일도 일어나지 않았습니다.");
                    WaitForNextStep();
                }
                else
                {
                    if (criticalAttack < 16)
                    {
                        // 몬스터 체력 감소
                        selectMonster.currentTime -= criticalAttack;

                        // 출력
                        Console.Clear();
                        Console.WriteLine("Battle!!!\n");
                        Console.WriteLine($"{player.name}의 공격!");
                        Console.WriteLine($"Lv. {selectMonster.level} {selectMonster.name}을(를) 맞췄습니다. [데미지 : {criticalAttack}] - 치명타 공격!!");
                        WaitForNextStep();

                    }
                    else
                    {
                        // 몬스터 체력 감소
                        selectMonster.currentTime -= playerAttack;

                        // 출력
                        Console.Clear();
                        Console.WriteLine("Battle!!!\n");
                        Console.WriteLine($"{player.name}의 공격!");
                        Console.WriteLine($"Lv. {selectMonster.level} {selectMonster.name}을(를) 맞췄습니다. [데미지 : {playerAttack}]");
                        WaitForNextStep();
                    }
                }


                //사망 처리
                if (selectMonster.currentTime <= 0) // 몬스터가 죽었을 때 처리
                {
                    Console.WriteLine($"Lv. {selectMonster.level} {selectMonster.name}은(는) 죽었습니다.");
                    selectMonster.currentTime = 0; // 체력을 0으로 설정
                }
                else
                {
                    Console.WriteLine($"Hp. {selectMonster.currentTime}/{selectMonster.maxTime}");
                }

                // 정보를 확인하고 다음 단계로 이동
                Console.Clear();
                ShowBattleStatus();
                WaitForNextStep();
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

            WaitForNextStep();
        }

        public void EndBattle(bool victory)
        {
            Console.Clear();
            Console.WriteLine("Battle!! - Result\n");
            int beforeExp = player.exp;
            int beforeLevel = player.level;

            if (victory)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Victory\n");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"학원에서 과제 {monsters.Count}개를 풀었습니다.\n");

                GetExp();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You Lose\n");
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.WriteLine("[캐릭터 정보]");
            Console.Write($"Lv.{beforeLevel} {player.name}");
            if ( player.level > beforeLevel )
            {
                Console.WriteLine($" -> Lv.{ player.level} { player.name}");
            }
            else
            {
                Console.WriteLine();
            }
            Console.WriteLine($"HP {player.maxTime} -> {player.currentTime}");
            Console.WriteLine($"exp {beforeExp} -> {player.exp}\n");

            Console.WriteLine("[획득 아이템]");
            give.GiveGold(monsters);
            give.GiveItem();
            give.GiveEquipment();
            Console.WriteLine();

            Console.Write("0. 다음\n>>");

            int input = GameStart.instance.Input();
            if (input == 0)
            {
                if (!victory)
                {
                    //GameOver();
                }
                else
                {
                    GameStart.instance.Start(); // 승리 후 다음 선택
                }
            }
            Console.Clear();
        }

      

        public void ShowBattleStatus()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("현재 전투 상황\n");

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

        public void WaitForNextStep()
        {
            Console.Write("\n게속 진행을 위해 1을 입력해주세요\n>>");

            int input = GameStart.instance.Input();
            while (input != 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다. 1을 입력해주세요.");
                Console.ForegroundColor = ConsoleColor.White;
                input = GameStart.instance.Input();
            }

            

        }

        public void GetExp()
        {
            int exp = 0;
            foreach (Monster monster in monsters)
            {
                exp += monster.exp;
            }
            player.exp += exp;

            if (player.exp >= 100 && player.level < 5)
            {
                player.level = 5;
                player.baseIQ += 0.5;
                player.baseFocus += 1;
                player.IQ += 0.5;
                player.focus += 1;
                player.job = Player.Job.담임매니저;
            }
            else if (player.exp >= 65 && player.level < 4)
            {
                player.level = 4;
                player.baseIQ += 0.5;
                player.baseFocus += 1;
                player.IQ += 0.5;
                player.focus += 1;
                player.job = Player.Job.튜터;
            }
            else if (player.exp >= 35 && player.level < 3)
            {
                player.level = 3;
                player.baseIQ += 0.5;
                player.baseFocus += 1;
                player.IQ += 0.5;
                player.focus += 1;
                player.job = Player.Job.튜터;
            }
            else if (player.exp >= 10 && player.level < 2)
            {
                player.level = 2;
                player.baseIQ += 0.5;
                player.baseFocus += 1;
                player.IQ += 0.5;
                player.focus += 1;
                player.job = Player.Job.수강생;
            }
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


