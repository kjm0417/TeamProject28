using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject28.GameManager;

namespace TeamProject28.Quest_Folder
{
    public class Quest
    {
        public QuestList questList;

        public bool is_Clear { get; set; }
        public bool is_Quest { get; set; }//퀘스트 진행중인지
        public string name { get; set; }//퀘스트 이름
        public string inform { get; set; }//퀘스트 정보
        public string goals_inform { get; set; }//퀘스 목표 정보
        public int goal { get; set; }
        public int tmp_goal { get; set; }
        public int reward { get; set; }//퀘스트 보상

        public Quest()
        {
            goal = 10;
            tmp_goal = 0;
            is_Clear = false;
            is_Quest = false;
            name = "으아아아";
            inform = "이야야야";
            goals_inform = "크아아아악" + " (" + tmp_goal + " / " + goal + ")";
            reward = 1000;
        }

        public Quest(bool is_Clear, bool is_Quest, string name, string inform, string goals_inform, int goal, int tmp_goal, int reward)
        {
            this.is_Clear = is_Clear;
            this.is_Quest = is_Quest;
            this.name = name;
            this.inform = inform;
            this.goals_inform = goals_inform;
            this.goal = goal;
            this.tmp_goal = tmp_goal;
            this.reward = reward;
        }

        public void OpenQuest()
        {
            questList = GameStart.instance.questList;
            // 이전 창 지우기
            Console.Clear();
            //상태창 띄우기
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Quest!!\n");
            Console.ForegroundColor = ConsoleColor.White;

            int index = 1;

            foreach (Quest quest in questList.quests)
            {
                if (questList.quests[index - 1].is_Clear == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0}. {1}(완료)", index, questList.quests[index - 1].name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (questList.quests[index - 1].is_Quest == true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("{0}. {1}(진행중)", index, questList.quests[index - 1].name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                    Console.WriteLine("{0}. {1}", index, questList.quests[index - 1].name);
                index++;
            }
            Console.WriteLine("0. 나가기\n\n");
            Console.WriteLine("원하시는 퀘스트를 선택해주세요.");

            int input = GameStart.instance.Input();
            while (input < 0 || input > 3)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                Console.WriteLine("원하시는 퀘스트를 선택해주세요.");
                input = GameStart.instance.Input();
            }

            if (input != 0)
            {
                while (questList.quests[input - 1].is_Clear == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("이미 클리어하였습니다.");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Console.WriteLine("원하시는 퀘스트를 선택해주세요.");
                    input = GameStart.instance.Input();
                }
            }

            switch (input)
            {
                case 1:
                    Quest_Open(input - 1);
                    break;
                case 2:
                    Quest_Open(input - 1);
                    break;
                case 3:
                    Quest_Open(input - 1);
                    break;
                case 0:
                    GameStart.instance.Start();
                    return;
            }
        }
        public void Quest_Open(int num)
        {
            if(num  == 0)
            {
                questList.quests[num].goals_inform = $"스테이지 3회 클리어 ( {questList.quests[num].tmp_goal} / {questList.quests[num].goal} )";
            }
            else if (num == 1)
            {
                questList.quests[num].goals_inform = $"아이템 3회 획득하기 ( {questList.quests[num].tmp_goal} / {questList.quests[num].goal} )";
            }
            else if(num == 2)
            {
                questList.quests[num].goals_inform = $"회복 아이템 3회 사용하기 ( {questList.quests[num].tmp_goal} / {questList.quests[num].goal} )";
            }
            // 이전 창 지우기
            Console.Clear();
            //상태창 띄우기
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Quest!!\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(questList.quests[num].name + "\n");
            Console.WriteLine(questList.quests[num].inform + "\n");
            Console.WriteLine(questList.quests[num].goals_inform + "\n");
            Console.WriteLine("보상");
            Console.WriteLine(questList.quests[num].reward + "G\n\n");

            if (questList.quests[num].is_Quest == true)
            {
                if (questList.quests[num].goal <= questList.quests[num].tmp_goal)
                {
                    Console.WriteLine("1. 보상 받기");
                    Console.WriteLine("2. 돌아가기\n\n");
                    Console.WriteLine("원하시는 행동을 입력해주세요.");

                    int input1 = GameStart.instance.Input();
                    while (input1 < 1 || input1 > 2)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("잘못된 입력입니다");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        input1 = GameStart.instance.Input();
                    }
                    switch (input1)
                    {
                        case 1:
                            {
                                GameStart.instance.player.gold += questList.quests[num].reward;
                                questList.quests[num].is_Clear = true;
                                questList.quests[num].is_Quest = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("진행중입니다.");
                    Console.WriteLine("0. 나가기\n\n");
                    Console.WriteLine("원하시는 행동을 입력해주세요.");
                    Console.Write(">>");
                    int input = GameStart.instance.Input();
                    while (input != 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("잘못된 입력입니다");
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine();
                        input = GameStart.instance.Input();
                    }
                }
                
            }
            else
            {
                Console.WriteLine("1. 수락");
                Console.WriteLine("2. 거절");
                Console.WriteLine("0. 나가기\n\n");
                Console.WriteLine("원하시는 행동을 입력해주세요.");
                Console.Write(">>");
                int input = GameStart.instance.Input();
                while (input < 0 || input > 2)
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
                        {
                            questList.quests[num].is_Quest = true;
                        }
                        break;
                    default:
                        break;
                }
            }

            

            GameStart.instance.Start();
        }
    }
}