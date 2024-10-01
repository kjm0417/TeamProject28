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
        public List<QuestList> questList = new List<QuestList>();

        public void Quest_Init()
        {
            questList.Add(new Quest1());
            questList.Add(new Quest2());
            questList.Add(new Quest3());

            questList[0].Quest_Init();
            questList[1].Quest_Init();
            questList[2].Quest_Init();
        }

        public void OpenQuest()
        {
            questList[0].Quest_Tmp();
            questList[1].Quest_Tmp();
            questList[2].Quest_Tmp();

            // 이전 창 지우기
            Console.Clear();
            //상태창 띄우기
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Quest!!\n");
            Console.ForegroundColor = ConsoleColor.White;

            int index = 1;
            foreach(QuestList quest in questList)
            {
                if(questList[index - 1].is_Clear == true)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("{0}. {1}(완료)", index, questList[index - 1].name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else if (questList[index - 1].is_Quest == true)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("{0}. {1}(진행중)", index, questList[index - 1].name);
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine("{0}. {1}", index, questList[index - 1].name);
                }
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

            if(input != 0)
            {
                while (questList[input - 1].is_Clear == true || input != 0)
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
            // 이전 창 지우기
            Console.Clear();
            //상태창 띄우기
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Quest!!\n");
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine(questList[num].name + "\n");
            Console.WriteLine(questList[num].inform + "\n");
            Console.WriteLine(questList[num].goals_inform + "\n");
            Console.WriteLine("보상");
            Console.WriteLine(questList[num].reward + "G\n\n");

            if (questList[num].is_Quest == true)
            {
                if (questList[num].goal <= questList[num].tmp_goal)
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
                                GameManager.GameStart.instance.player.gold += questList[num].reward;
                                questList[num].is_Clear = true;
                                questList[num].is_Quest = false;
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
                            questList[num].is_Quest = true;
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