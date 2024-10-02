using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject28.GameManager;

namespace TeamProject28
{
    internal class Shop
    {
        public Player player;
        public ItemList itemList;

        public Shop()
        {
        }

        public void Shopping()
        {
            this.player = GameStart.instance.player;
            this.itemList = GameStart.instance.itemList;

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("상점");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G\n");

            foreach (Item item in itemList.items)
            {
                Console.Write(item.name + "\t| ");
                if (item.type == ItemType.IQ)
                {
                    Console.Write("IQ");
                }
                else if (item.type == ItemType.focus) 
                {
                    Console.Write("집중력");
                }
                else if (item.type == ItemType.time)
                {
                    Console.Write("시간");
                }
                else if (item.type == ItemType.passion)
                {
                    Console.Write("열정");
                }
                Console.Write(" + " + item.status);
                Console.Write("\t| " + item.description);
                Console.WriteLine("\t| " + item.price + " G");

            }
            Console.WriteLine();

            Console.WriteLine("1. 아이템 구매");
            Console.WriteLine("2. 아이템 판매");
            Console.WriteLine("0. 나가기");
            Console.Write("\n원하시는 행동을 입력해주세요\n >>");

            int input = GameStart.instance.Input();
            while (input < 0 && input > 2)
            {
                Console.WriteLine("잘못된 입력입니다.");
                input = GameStart.instance.Input();
            }

            switch (input)
            {
                case 1:
                    Purchase();
                    break;
                case 2:
                    Sell();
                    break;
                case 0:
                    GameStart.instance.Start();
                    break;
            }
        }

        public void Purchase()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G\n");

            int i = 1;

            List<Item> list = new List<Item>();
            list.Clear();
            Item temp = new Item();
            list.Add(temp);

            foreach (Item item in itemList.items)
            {
                list.Add(item);
                Console.Write(i + ". ");
                Console.Write(item.name + "\t\t| ");
                if (item.type == ItemType.IQ)
                {
                    Console.Write("IQ");
                }
                else if (item.type == ItemType.focus)
                {
                    Console.Write("집중력");
                }
                else if (item.type == ItemType.time)
                {
                    Console.Write("시간");
                }
                else if (item.type == ItemType.passion)
                {
                    Console.Write("열정");
                }
                Console.Write(" + " + item.status);
                Console.Write("\t| " + item.description);
                Console.WriteLine("\t| " + item.price + " G");
                
                i++;
            }
            Console.WriteLine();

            Console.WriteLine("0. 나가기");
            Console.Write("\n구매할 아이템 번호를 입력해주세요\n >>");

            int input = GameStart.instance.Input();

            while (input < 0 || input > itemList.items.Length)
            {
                Console.WriteLine("잘못된 입력입니다.");
                input = GameStart.instance.Input();
            }

            if (input == 0)
            {
                Shopping();
                return;
            }
            else
            {
                for (int k = 0; k < itemList.items.Length; k++)
                {
                    if (list[input].name == itemList.items[k].name)
                    {
                        if (player.gold >= itemList.items[k].price)
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.WriteLine("구매를 완료했습니다.");
                            Console.ForegroundColor = ConsoleColor.White;
                            itemList.items[k].quantity++;
                            player.gold -= itemList.items[k].price;
                        }
                        else if (player.gold < itemList.items[k].price)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("골드가 부족합니다.");
                            Console.ForegroundColor = ConsoleColor.White;
                        }
                    }
                }
            }
            Thread.Sleep(3000);

            Purchase();
        }

        public void Sell()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("필요한 아이템을 얻을 수 있는 상점입니다.\n");

            Console.WriteLine("[보유 골드]");
            Console.WriteLine($"{player.gold} G\n");

            int i = 1;

            List<Item> list = new List<Item>();
            list.Clear();
            Item temp = new Item();
            list.Add(temp);

            foreach (Item item in itemList.items)
            {
                if (item.quantity > 0)
                {
                    list.Add(item);
                    Console.Write(i + ". ");
                    Console.Write(item.name + "\t\t| ");
                    if (item.type == ItemType.IQ)
                    {
                        Console.Write("IQ");
                    }
                    else if (item.type == ItemType.focus)
                    {
                        Console.Write("집중력");
                    }
                    else if (item.type == ItemType.time)
                    {
                        Console.Write("시간");
                    }
                    else if (item.type == ItemType.passion)
                    {
                        Console.Write("열정");
                    }
                    Console.Write(" + " + item.status);
                    Console.Write("\t| " + item.description);
                    Console.Write($" \t| 보유중 : {item.quantity}개");
                    Console.WriteLine("\t| " + item.price * 0.85 + " G");

                    i++;
                }
            }
            Console.WriteLine();

            Console.WriteLine("0. 나가기");
            Console.Write("\n판매할 아이템 번호를 입력해주세요\n >>");

            int input = GameStart.instance.Input();

            while (input < 0 || input > itemList.items.Length)
            {
                Console.WriteLine("잘못된 입력입니다.");
                input = GameStart.instance.Input();
            }

            if (input == 0)
            {
                Shopping();
                return;
            }
            else
            {
                for (int k = 0; k < itemList.items.Length; k++)
                {
                    if (list[input].name == itemList.items[k].name)
                    {
                        itemList.items[k].quantity--;
                        if (itemList.items[k].isEquipped && itemList.items[k].quantity <= 0)
                        {
                            itemList.items[k].isEquipped = false;
                            if (itemList.items[k].type == ItemType.IQ)
                                player.IQ = player.baseIQ;
                            else if (itemList.items[k].type == ItemType.focus)
                                player.focus = player.baseFocus;
                        }
                        player.gold += (int)(itemList.items[k].price * 0.85);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("판매를 완료했습니다.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            Thread.Sleep(3000);

            Sell();

        }
    }
}
