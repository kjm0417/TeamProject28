using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject28
{
    public enum MonsterType { Easy, Normal, Hard, Boss}
    public class Monster
    {
        public int level;
        public string name;
        public int IQ;
        public int maxTime;
        public int currentTime;
        public int exp;
        public Monster(MonsterType monsterType)
        {
            switch (monsterType)
            {
                case MonsterType.Easy:
                    this.level = 1;
                    this.name = "쉬운 과제";
                    this.IQ = 5;
                    this.maxTime = 15;
                    this.currentTime = maxTime;
                    this.exp = 1;
                    break;
                case MonsterType.Normal:
                    this.level = 2;
                    this.name = "일반 과제";
                    this.IQ = 10;
                    this.maxTime = 9;
                    this.currentTime = maxTime;
                    this.exp = 2;
                    break;
                case MonsterType.Hard:
                    this.level = 3;
                    this.name = "어려운 과제";
                    this.IQ = 8;
                    this.maxTime = 15;
                    this.currentTime = maxTime;
                    this.exp = 3;
                    break;
                case MonsterType.Boss:
                    this.level = 5;
                    this.name = "극강 난이도 과제";
                    this.IQ = 10;
                    this.maxTime = 30;
                    this.currentTime = maxTime;
                    this.exp = 5;
                    break;
                default:
                    this.level = 0;
                    this.name = "오류";
                    this.IQ = 0;
                    this.maxTime = 1;
                    this.currentTime = maxTime;
                    this.exp = 0;
                    break;
            }
        }
    }
}
