using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject28
{
     public abstract class Skill
    {
        public string skillName { get; set; }
        public int numberPerson { get; set; }
        public int manaCost { get; set; }

        public Skill(string skillName, int numberPerson, int manaCost)
        {
            this.skillName = skillName;
            this.numberPerson = numberPerson;
            this.manaCost = manaCost;
        }

        // 스킬 사용 시 추상 메서드
        public abstract void Use(Player player, List<Monster> targets);
    }

    //한명 적을 2배 피해 주는 기술
    public class DoubleAttack : Skill
    {
        public DoubleAttack() : base("해설 문제 보기", 1,15)
        {

        }

        public override void Use(Player player, List<Monster> targets)
        {
            if (targets.Count == 1)
            {
                Monster target = targets[0];
                int damage = (int)(player.IQ * 2);
                Console.WriteLine($"{player.name}가 {target.name}에게 {damage}의 피해를 입혔습니다.");
                target.currentTime -= damage;
            }
            else
            {
                Console.WriteLine("잘못된 대상 수입니다.");
            }
        }

        public class MultiAttack : Skill
        {
            public MultiAttack() : base("Multi Attack", 2, 15) { }

            public override void Use(Player player, List<Monster> targets)
            {
                if (targets.Count == 2)
                {
                    foreach (Monster target in targets)
                    {
                        int damage = (int)(player.IQ * 1.5);
                        Console.WriteLine($"{player.name}가 {target.name}에게 {damage}의 피해를 입혔습니다.");
                        target.currentTime -= damage;
                    }
                }
                else
                {
                    Console.WriteLine("대상 수가 맞지 않습니다.");
                }
            }
        }


        // 공격력 버프 스킬
        public class AttackBuff : Skill
        {
            public int BuffAmount { get; set; }

            public AttackBuff() : base("Attack Buff", 5, 0) // 0은 적을 공격하지 않음
            {
                BuffAmount = 5;
            }

            public override void Use(Player player, List<Monster> targets)
            {
                Console.WriteLine($"{player.name}의 공격력이 {BuffAmount}만큼 증가했습니다.");
                player.IQ += BuffAmount;
            }
        }

        // 힐 스킬
        public class Heal : Skill
        {
            public int HealAmount { get; set; }

            public Heal() : base("Heal", 8, 0) // 0은 적을 공격하지 않음
            {
                HealAmount = 30;
            }

            public override void Use(Player player, List<Monster> targets)
            {
                player.currentTime = Math.Min(player.maxTime, player.currentTime + HealAmount);
                Console.WriteLine($"{player.name}가 {HealAmount}만큼 체력을 회복했습니다. 현재 체력: {player.currentTime}/{player.maxTime}");
            }
        }

    }




}
