﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using TeamProject28.GameManager;

namespace TeamProject28.Item
{
    internal class Inventory
    {
        public Item[] itemList = ItemList.items;
        public void OpenInventory()
        {
            // 이전 창 지우기
            Console.Clear();
            //상태창 띄우기
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("인벤토리");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("보유중인 아이템이 표시됩니다. (장착중일 경우 [E])");
            Console.WriteLine();

            foreach (Item item in itemList)
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
                    if (item.type == ItemType.time || item.type == ItemType.passion)
                    {
                        Console.Write($" \t| 보유중 : {item.quantity}개");
                    }
                    Console.WriteLine();
                }
            }

            Console.WriteLine("\n1. 장착 관리");
            Console.WriteLine("0. 돌아가기");
            Console.Write("\n원하시는 행동을 입력해주세요\n >>");

            int input = GameStart.instance.Input();

            while (input < 0 || input > 1)
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
                case 0:
                    ItemList.items = itemList;
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

            foreach (Item item in itemList)
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
                        case ItemType.time:
                            Console.Write("시간");
                            break;
                        case ItemType.passion:
                            Console.Write("열정");
                            break;
                    }
                    Console.Write($" + {item.status}\t| {item.description}");
                    Console.WriteLine();
                    i++;
                }
            }

            Console.WriteLine("\n0. 돌아가기");
            Console.Write("\n원하시는 행동을 입력해주세요\n >>");

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
                    foreach(Item item in itemList)
                    {
                        if(item.type == items[input - 1].type && item != items[input - 1])
                        {
                            item.isEquipped = false;
                        }
                    }
                    items[input - 1].isEquipped = !items[input - 1].isEquipped;
                    Equipment();
                    break;
            }
        }
    }
}