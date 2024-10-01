﻿using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject28.Item;
using TeamProject28.Quest_Folder;
using static TeamProject28.Player;

namespace TeamProject28.GameManager
{
    internal class GameStart
    {
        public Player player = new Player();//player 정보 가져오기
        public Battle battle = new Battle();//전투 정보 가져오기
        public Inventory inventory = new Inventory(); //인벤토리 가져오기
        public Quest quest = new Quest(); //퀘스트 가져오기
        public static GameStart instance;
        public static ItemList itemList = new ItemList();

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
            //Load();

            if (instance == null)
            {
                instance = this;

                player = new Player();
                ItemList.Init();
                quest.Quest_Init();
            }       

            ActionSelect();

            Save();
        }


        public void ActionSelect()
        {
            Console.Clear();
            Console.WriteLine("스파르타 무한의 탑에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 무한의 탑을 오르면서 높을 층을 갱신하는 게임입니다");

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
                            quest.questList[0].tmp_goal+=1;
                        battle.Start();
                        break;
                    }
                case 4:
                    quest.OpenQuest();
                    break;
                case 0:
                    return;
            }
        }

        public void Load()
        {
            //if (File.Exists("./playerData.json"))
            //{
            //    //string json = File.ReadAllText("./playerData.json");
            //    ////player = JsonConvert.DeserializeObject<Player>(json);
            //    //json = File.ReadAllText("./itemData.json");
            //    //items = JsonConvert.DeserializeObject<ItemList>(json);
            //    //json = File.ReadAllText("./questData.json");
            //    //quest = JsonConvert.DeserializeObject<Quest>(json);
            //}
            //else
            //{
            //    player = new Player();
            //    ItemList.Init();
            //    quest.Quest_Init();
            //}

            /*player = new Player();
            ItemList.Init();
            quest.Quest_Init();*/
        }

        public void Save()
        {
            //string playerFileName = "playerData.json";
            //string itemFileName = "itemData.json";
            //string questFileName = "questData.json";
            //string jsonPlayer = JsonConvert.SerializeObject(player);
            //string jsonItem = JsonConvert.SerializeObject(items);
            //string jsonQuest = JsonConvert.SerializeObject(quest);
            //File.WriteAllText(playerFileName, jsonPlayer);
            //File.WriteAllText(itemFileName, jsonItem);
            //File.WriteAllText(questFileName, jsonQuest);
        }
    }
}