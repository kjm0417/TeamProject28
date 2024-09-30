using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TeamProject28.Item
{
    internal class ItemList
    {
        public static int itemCount = 8;
        public static Item[] items = new Item[itemCount];

        public static void Init()
        {
            items[0] = new Item("스파르타 : 기초편", ItemType.IQ, 2, "기초부터 차근차근 알려주는 스파르타 강의입니다.", 600, 1, true);
            items[1] = new Item("스파르타 : 심화편", ItemType.IQ, 4, "기본기를 다진 분들을 위한 심화편 스파르타 강의입니다.", 1500, 1, false);
            items[2] = new Item("스파르타 : 기출문제집", ItemType.IQ, 7, "강의를 마스터하신 분들에게 어울리는 기출문제집입니다.", 2400, 0, false);
            items[3] = new Item("잘 나오는 펜", ItemType.focus, 5, "적어도 펜이 안 나와서 집중이 안 되는 일은 없겠네요.", 700, 1, false);
            items[4] = new Item("노이즈캔슬링 이어폰", ItemType.focus, 10, "소음으로부터 나를 보호해주는 장비입니다.", 1400, 0, false);
            items[5] = new Item("독서실 책상", ItemType.focus, 15, "이 분위기라면 집중이 안 될 수가 없을 것 같습니다.", 2100, 0, false);
            items[6] = new Item("에너지드링크", ItemType.time, 30, "과제하는 시간을 효과적으로 올려주는 음료입니다.", 500, 3, false);
            items[7] = new Item("열정드링크", ItemType.passion, 20, "열정! 열정! 열정!!!", 500, 3, false);
        }
    }
}
