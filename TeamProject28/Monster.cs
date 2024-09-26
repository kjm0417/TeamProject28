using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject28
{
    enum MonsterType { Easy, Normal, Hard, Boss}
    internal class Monster
    {
        public int level;
        public string name;
        public int IQ;
        public int time;
        public Monster(MonsterType monsterType)
        {
            switch (monsterType)
            {
                case MonsterType.Easy:
                    this.level = 1;
                    this.name = "쉬운 과제";
                    this.IQ = 5;
                    this.time = 15;
                    break;
                case MonsterType.Normal:
                    this.level = 2;
                    this.name = "일반 과제";
                    this.IQ = 10;
                    this.time = 9;
                    break;
                case MonsterType.Hard:
                    this.level = 3;
                    this.name = "어려운 과제";
                    this.IQ = 8;
                    this.time = 25;
                    break;
                case MonsterType.Boss:
                    this.level = 5;
                    this.name = "극강 난이도 과제";
                    this.IQ = 10;
                    this.time = 30;
                    break;
                default:
                    this.level = 0;
                    this.name = "오류";
                    this.IQ = 0;
                    this.time = 1;
                    break;
            }
        }
    }
}
