using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject28.Quest_Folder
{
    public class QuestList
    {
        public int QuestCount = 3;
        public Quest[] quests { get; set; }

        public void Quest_Init()
        {
            quests = new Quest[QuestCount];

            quests[0] = new Quest(false,false, "과제 진행하기", "과제는 미리미리 진행해야 도움이 된다.", $"스테이지 3회 클리어 ( 0 / 1 )", 3,0,1000);
            quests[1] = new Quest(false, false, "아이템 획득하기", "장인은 도구탓을 하지 않지만 좋은 도구를 쓴다", $"아이템 3회 획득하기 ( 0 / 1 )", 3, 0, 1000);
            quests[2] = new Quest(false, false, "카페인 섭취하기", "개발자는 혈소판 대신 카페인이 돌아다는다는 소문이 있다..", $"회복 아이템 3회 사용하기 ( 0 / 1 )", 3, 0, 1000);
        }
    }
}
