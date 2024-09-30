using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject28.Quest_Folder
{
    internal class QuestList
    {
        public bool is_Quest;//퀘스트 진행중인지
        public string name;//퀘스트 이름
        public string inform;//퀘스트 정보
        public int temp_Goals;//현재 상황
        public int goals;//퀘스 목표
        public string reward;//퀘스트 보상

        //퀘스트 초기화
        public virtual void Quest_Init()
        {
        }
    }

    internal class Quest1 : QuestList
    {
        public override void Quest_Init()
        {
            is_Quest = false;
            name = "과제 진행하기";
            inform = "과제는 미리미리 진행해야 도움이 된다.";
            temp_Goals = 0;
            goals = 5;
            reward = "1000G";
        }
    }

    internal class Quest2 : QuestList
    {
        public override void Quest_Init()
        {
            is_Quest = false;
            name = "아이템 장착하기";
            inform = "장인은 도구탓을 하지 않지만 좋은 도구를 쓴다";
            temp_Goals = 0;
            goals = 5;
            reward = "1000G";


        }
    }
}
