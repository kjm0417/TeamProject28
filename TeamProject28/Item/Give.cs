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
                sum += monster.level * 300;
                if(monster.level == 5)
                {
                    sum += 1000;
                }
            }

            Console.WriteLine(sum + " Gold");
        }

        public void GiveItem()
        {
            Random random = new Random();
            int temp = Battle.stage;
            int timePotion = 0;
            int passionPotion = 0;
            while (temp < 10)
            {
                timePotion++;
                passionPotion++;
                temp -= 10;
            }
            if (random.Next(0, 10) < temp)
            {
                timePotion++;
            }
            if (random.Next(0, 10) < temp)
            {
                passionPotion++;
            }

            Console.WriteLine("에너지드링크 - {0}", timePotion);
            Console.WriteLine("열정드링크 - {0}", passionPotion);
        }

        public void GiveEquipment()
        {
            Random random = new Random();
            int temp = Battle.stage + 5;

            if(random.Next(0, 5) < temp % 5)
            {

            }
            if (random.Next(0, 5) < temp % 5)
            {

            }
        }
    }
}
