using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject28.Quest_Folder
{
    public class QuestList
    {
        public bool is_Clear { get; set; }
        public bool is_Quest { get; set; }//퀘스트 진행중인지
        public string name { get; set; }//퀘스트 이름
        public string inform { get; set; }//퀘스트 정보
        public string goals_inform { get; set; }//퀘스 목표 정보
        public int goal { get; set; }
        public int tmp_goal { get; set; }
        public int reward { get; set; }//퀘스트 보상

        //퀘스트 초기화
        public virtual void Quest_Init()
        {
        }

        //궤스트 진행상황
        public virtual void Quest_Tmp()
        {
        }
    }

    public class Quest1 : QuestList
    {
        public override void Quest_Init()
        {
            goal = 3;
            tmp_goal = 0;
            is_Clear = false;
            is_Quest = false;
            name = "과제 진행하기";
            inform = "과제는 미리미리 진행해야 도움이 된다.";
            goals_inform = "스테이지 3회 클리어" + " (" + tmp_goal + " / " + goal + ")";
            reward = 1000;
        }

        public override void Quest_Tmp()
        {
            goals_inform = "스테이지 3회 클리어" + " (" + tmp_goal + " / " + goal + ")";
        }
    }

    public class Quest2 : QuestList
    {
        public override void Quest_Init()
        {
            goal = 3;
            tmp_goal = 0;
            is_Clear = false;
            is_Quest = false;
            name = "아이템 획득하기";
            inform = "장인은 도구탓을 하지 않지만 좋은 도구를 쓴다";
            goals_inform = "아이템 3회 획득하기" + " (" + tmp_goal + " / " + goal + ")";
            reward = 1000;
        }

        public override void Quest_Tmp()
        {
            goals_inform = "스테이지 3회 클리어" + " (" + tmp_goal + " / " + goal + ")";
        }
    }
}
