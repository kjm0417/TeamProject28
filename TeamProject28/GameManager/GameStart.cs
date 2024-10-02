using System.Text.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject28.Quest_Folder;
using static TeamProject28.Player;

namespace TeamProject28.GameManager
{
    internal class GameStart
    {
        public List<Skill> skills = new List<Skill> { new SuperPower(), new DoubleAttack(), new AttackBuff(), new Heal() };
        public Player player = new Player();//player 정보 가져오기
        public Battle battle = new Battle();//전투 정보 가져오기
        public Inventory inventory = new Inventory(); //인벤토리 가져오기
        public Quest quest = new Quest(); //퀘스트 가져오기
        public static GameStart instance;
        public ItemList itemList = new ItemList();

        public int Input() //선택 입력 기능
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

        public void Start()
        {
            if (instance == null)
            {
                instance = this;
            }

            Save();

            ActionSelect();

            Save();
        }


        public void ActionSelect()
        {
            if (player.stage <= player.stage_Tmp)
                player.stage = player.stage_Tmp;
            Console.Clear();
            Console.WriteLine("스파르타 무한의 탑에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 무한의 탑을 오르면서 높을 층을 갱신하는 게임입니다");
            Console.WriteLine("도달한 최고 스테이지 : {0}", player.stage);
            Console.WriteLine("현재 스테이지 : {0}\n\n", player.stage_Tmp);

            Console.WriteLine("\n1. 상태 보기");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 전투 시작");
            Console.WriteLine("4. 퀘스트 ");
            Console.WriteLine("0. 게임 종료");
            Console.Write("\n원하시는 행동을 입력해주세요\n >>");
            int input = Input();

            while (input < 0 || input > 4)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                input = Input();
            }

            switch (input)
            {
                case 1:
                    player.PlayerStats();
                    break;
                case 2:
                    inventory.OpenInventory();
                    break;
                case 3:
                    {
                        if (quest.questList[0].is_Quest == true)
                            quest.questList[0].tmp_goal += 1;
                        battle.Start();
                        break;
                    }
                case 4:
                    quest.OpenQuest();
                    break;
                case 0:
                    Environment.Exit(0);
                    break;

            }
        }

        public void Load()
        {
            if (File.Exists("./playerData.json"))
            {
                string json = File.ReadAllText("./playerData.json");
                player = JsonSerializer.Deserialize<Player>(json);
            }
            else
            {
                player = new Player();
                player.name = Intro.CollectName();
            }

            if(File.Exists("./itemData.json"))
            {
                string json = File.ReadAllText("./itemData.json");
                var datas = JsonSerializer.Deserialize<ItemList>(json);
                itemList.items = datas.items;
            }
            else
            {
                itemList.Init();
            }

            if (File.Exists("./questData.json"))
            {
                string json = File.ReadAllText("./questData.json");
                quest = JsonSerializer.Deserialize<Quest>(json);
            }
            else
            {
                quest.Quest_Init();
            }
        }

        public void Save()
        {
            string playerFileName = "./playerData.json";
            string itemFileName = "./itemData.json";
            string questFileName = "./questData.json";
            string jsonPlayer = JsonSerializer.Serialize<Player>(player);
            string jsonItem = JsonSerializer.Serialize<ItemList>(itemList);
            string jsonQuest = JsonSerializer.Serialize<Quest>(quest);
            File.WriteAllText(playerFileName, jsonPlayer);
            File.WriteAllText(itemFileName, jsonItem);
            File.WriteAllText(questFileName, jsonQuest);
        }
    }
}