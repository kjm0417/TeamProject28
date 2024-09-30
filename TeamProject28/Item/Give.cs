using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject28.Item
{
    internal class Give
    {
        public Give() { }

        public void GiveGold(List<Monster> monsters)
        {
            int sum = 0;
            foreach (Monster monster in monsters)
            {
                sum += monster.level * 100;
                if(monster.level == 5)
                {
                    sum += 1000;
                }
            }

            Console.WriteLine(sum + " Gold");
        }

        public void GiveItem()
        {
            // 포션 획득 확률
            Random random = new Random();
            int temp = Battle.stage;
            int timePotion = 0;
            int passionPotion = 0;
            while (temp > 5)
            {
                timePotion++;
                passionPotion++;
                temp -= 5;
            }
            if (random.Next(0, 5) < temp)
            {
                timePotion++;
            }
            if (random.Next(0, 5) < temp)
            {
                passionPotion++;
            }

            ItemList.items[6].quantity += timePotion;
            ItemList.items[7].quantity += passionPotion;

            Console.WriteLine("에너지드링크 - {0}", timePotion);
            Console.WriteLine("열정드링크 - {0}", passionPotion);
        }

        public void GiveEquipment()
        {
            Random random = new Random();
            int temp = Battle.stage + 5;

            // IQ 템 획득 확률
            if(random.Next(0, 5) < temp % 5)
            {
                for (int i = 1; i <= ItemList.itemCount; i++)
                {
                    int j = ItemList.itemCount - i;
                    if (ItemList.items[j].price < (temp / 5) * 1000 && ItemList.items[j].type == ItemType.IQ)
                    {
                        ItemList.items[j].quantity++;
                        Console.WriteLine($"{ ItemList.items[j].name } - 1");
                        break;
                    }                    
                }
            }

            // 집중력 템 획득 확률
            if (random.Next(0, 5) < temp % 5)
            {
                for (int i = 1; i <= ItemList.itemCount; i++)
                {
                    int j = ItemList.itemCount - i;
                    if (ItemList.items[j].price < (temp / 5) * 1000 && ItemList.items[j].type == ItemType.focus)
                    {
                        ItemList.items[j].quantity++;
                        Console.WriteLine($"{ItemList.items[j].name} - 1");
                        break;
                    }
                }
            }
        }
    }
}
