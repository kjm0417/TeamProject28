using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamProject28.GameManager;

namespace TeamProject28
{
    internal class Give
    {
        public ItemList itemList;
        public Give()
        {
            if (itemList == null)
            {
                itemList = GameStart.instance.itemList;
            }
        }

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

            GameStart.instance.player.gold += sum;
            Console.WriteLine(sum + " Gold");
        }

        public void GiveItem()
        {
            // 포션 획득 확률
            Random random = new Random();
            int temp = GameStart.instance.player.stage_Tmp;
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

            itemList.items[6].quantity += timePotion;
            itemList.items[7].quantity += passionPotion;

            Console.WriteLine("에너지드링크 - {0}", timePotion);
            Console.WriteLine("열정드링크 - {0}", passionPotion);
        }

        public void GiveEquipment()
        {
            Random random = new Random();
            int temp = GameStart.instance.player.stage_Tmp + 5;

            // IQ 템 획득 확률
            if(random.Next(0, 5) < temp % 5)
            {
                for (int i = 1; i <= itemList.itemCount; i++)
                {
                    int j = itemList.itemCount - i;
                    if (itemList.items[j].price < (temp / 5) * 1000 && itemList.items[j].type == ItemType.IQ)
                    {
                        itemList.items[j].quantity++;
                        Console.WriteLine($"{ itemList.items[j].name } - 1");
                        break;
                    }
                }
            }

            // 집중력 템 획득 확률
            if (random.Next(0, 5) < temp % 5)
            {
                for (int i = 1; i <= itemList.itemCount; i++)
                {
                    int j = itemList.itemCount - i;
                    if (itemList.items[j].price < (temp / 5) * 1000 && itemList.items[j].type == ItemType.focus)
                    {
                        itemList.items[j].quantity++;
                        Console.WriteLine($"{itemList.items[j].name} - 1");
                        break;
                    }
                }
            }
        }
    }
}
