using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TeamProject28.Player;

namespace TeamProject28.GameManager
{
    internal class GameStart
    {
        public Player player = new Player();//player 정보 가져오기
        public Battle battle = new Battle();//전투 정보 가져오기
        public static GameStart instance;

        public int Input() //선택 입력 기능
        {
            string input = Console.ReadLine();
            if (int.TryParse(input, out int i))
            {
                return i;
            }
            else
            {
                return -1;
            }
        }

        public void Start()
        {
            instance = this;
            Console.WriteLine("스파르타 무한의 탑에 오신 여러분 환영합니다.");
            Console.WriteLine("이곳에서 무한의 탑을 오르면서 높을 층을 갱신하는 게임입니다");
            ActionSelect();
        }


        public void ActionSelect()
        {
            Console.WriteLine("\n1. 상태 보기");
            Console.WriteLine("2. 전투 시작");
            Console.WriteLine("0. 게임 종료");
            Console.Write("\n원하시는 행동을 입력해주세요\n >>");
            int input = Input();

            while (input < 0 || input > 2)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("잘못된 입력입니다");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine();
                input = Input();
            }

            switch (input)
            {
                case 1:
                    player.PlayerStats();
                    break;
                case 2:
                    battle.Start();
                    break;
                case 0:
                    return;
            }
        }

    }
}
