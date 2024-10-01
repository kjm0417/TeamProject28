using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject28
{
    internal class Item
    {
        public string name { get; set; }
        public ItemType type { get; set; }
        public int status { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public int quantity { get; set; }
        public bool isEquipped { get; set; }

        public Item()
        {
            name = "에너지드링크";
            type = ItemType.time;
            status = 30;
            description = "과제하는 시간을 효과적으로 올려주는 음료입니다.";
            price = 500;
            quantity = 3;
            isEquipped = false;
        }

        public Item(string name, ItemType type, int status, string description, int price, int quantity, bool isEquipped)
        {
            this.name = name;
            this.type = type;
            this.status = status;
            this.description = description;
            this.price = price;
            this.quantity = quantity;
            this.isEquipped = isEquipped;
        }
    }

    enum ItemType{ IQ, focus, time, passion}
}
