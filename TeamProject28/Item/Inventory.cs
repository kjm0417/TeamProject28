using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TeamProject28.GameManager;
using TeamProject28.Quest_Folder;

namespace TeamProject28
{
    internal class Inventory
    {
        Player player;
        public ItemList itemList;
        public Shop shop = new Shop(); //상점 가져오기


        public void OpenInventory()
        {
            player = GameStart.instance.player;
            itemList = GameStart.instance.itemList;
            // 이전 창 지우기
            Console.Clear();
            //상태창 띄우기
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("보유중인 아이템이 표시됩니다. (장착중일 경우 [E])");
            Console.WriteLine();
            
            foreach (Item item in itemList.items)
            {
                if (item.quantity > 0)
                {
                    if (item.isEquipped)
                    {
                        Console.Write("[E] ");
                    }
                    Console.Write(item.name + "\t| ");
                    switch (item.type)
                    {
                        case ItemType.IQ:
                            Console.Write("IQ");
                            break;
                        case ItemType.focus:
                            Console.Write("집중력");
                            break;
                        case ItemType.time:
                            Console.Write("시간");
                            break;
                        case ItemType.passion:
                            Console.Write("열정");
                            break;
                    }
                    Console.Write($" + {item.status}\t| {item.description}");
                    Console.Write($" \t| 보유중 : {item.quantity}개");
                    Console.WriteLine();
                }
            }

            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("2. 포션 마시기");
            Console.WriteLine("3. 상점 이용");
            Console.WriteLine("0. 돌아가기");
            Console.Write("\n원하시는 행동을 입력해주세요\n >>");

            int input = GameStart.instance.Input();

            while (input < 0 || input > 3)
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
                    //장착관리
                    Equipment();
                    break;
                case 2:
                    //포션 마시기
                    DrinkPotion();
                    break;
                case 3:
                    //상점 이용
                    shop.Shopping();
                    break;
                case 0:
                    itemList.items = itemList.items;
                    GameStart.instance.Start();
                    return;
            }
        }

        public void Equipment()
        {
            int i = 0;
            List<Item> items = new List<Item>();

            // 이전 창 지우기
            Console.Clear();
            //상태창 띄우기
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("장착 관리");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("아이템 장착을 관리할 수 있습니다. (장착중일 경우 [E])");
            Console.WriteLine();

            foreach (Item item in itemList.items)
            {
                if (item.quantity > 0 && item.type != ItemType.time && item.type != ItemType.passion)
                {
                    items.Add(item);

                    Console.Write((i + 1) + ". ");
                    if (item.isEquipped)
                    {
                        Console.Write("[E] ");
                    }
                    Console.Write(item.name + "\t| ");
                    switch (item.type)
                    {
                        case ItemType.IQ:
                            Console.Write("IQ");
                            break;
                        case ItemType.focus:
                            Console.Write("집중력");
                            break;
                    }
                    Console.Write($" + {item.status}\t| {item.description}");
                    Console.WriteLine();
                    i++;
                }
            }

            Console.WriteLine("\n0. 돌아가기");
            Console.Write("\n장착하거나 해제할 아이템을 입력해주세요\n >>");

            int input = GameStart.instance.Input();

            while (input < 0 || input > items.Count)
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
                    OpenInventory();
                    return;
                default:
                    foreach(Item item in itemList.items)
                    {
                        if(item.type == items[input - 1].type && item != items[input - 1])
                        {
                            item.isEquipped = false;                            
                        }
                    }
                    items[input - 1].isEquipped = !items[input - 1].isEquipped;

                    player.IQ = player.baseIQ;
                    player.focus = player.baseFocus;

                    foreach(Item item in itemList.items)
                    {
                        if (item.isEquipped)
                        {
                            if(item.type == ItemType.IQ)
                            {
                                player.IQ += item.status;
                            }
                            else
                            {
                                player.focus += item.status;
                            }
                        }
                    }
                    Equipment();
                    break;
            }
        }

        public void DrinkPotion()
        {
            int i = 0;
            List<Item> items = new List<Item>();

            // 이전 창 지우기
            Console.Clear();
            //상태창 띄우기
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("회복");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("포션을 통해 시간과 열정을 회복할 수 있습니다.");
            Console.WriteLine();

            foreach (Item item in itemList.items)
            {
                if (item.type == ItemType.time || item.type == ItemType.passion)
                {
                    items.Add(item);

                    Console.Write((i + 1) + ". ");
                    Console.Write(item.name + "\t| ");
                    switch (item.type)
                    {
                        case ItemType.time:
                            Console.Write("시간");
                            break;
                        case ItemType.passion:
                            Console.Write("열정");
                            break;
                    }
                    Console.Write($" + {item.status}\t| {item.description}");
                    Console.Write($" \t| 보유중 : {item.quantity}개");
                    Console.WriteLine();
                    i++;
                }
            }

            Console.WriteLine("\n0. 돌아가기");
            Console.Write("\n마실 포션을 입력해주세요\n >>");

            int input = GameStart.instance.Input();

            while (input < 0 || input > items.Count)
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
                    OpenInventory();
                    return;
                case 1:
                    if (itemList.items[6].quantity <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("포션이 부족합니다.");
                    }
                    else
                    {
                        if (GameStart.instance.questList.quests[2].is_Quest == true)
                            GameStart.instance.questList.quests[2].tmp_goal += 1;

                        itemList.items[6].quantity -= 1;
                        player.currentTime += 30;
                        if (player.currentTime > player.maxTime)
                        {
                            player.currentTime = player.maxTime;
                        }                        
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("회복을 완료했습니다.");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Thread.Sleep(2000);
                    DrinkPotion();
                    break;
                case 2:
                    if (itemList.items[7].quantity <= 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("포션이 부족합니다.");
                    }
                    else
                    {
                        itemList.items[7].quantity -= 1;
                        player.currentPassion += 30;
                        if (player.currentPassion > player.maxPassion)
                        {
                            player.currentPassion = player.maxPassion;
                        }
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("회복을 완료했습니다.");
                    }
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine();
                    Thread.Sleep(2000);
                    DrinkPotion();
                    break;
            }
        }
    }
}