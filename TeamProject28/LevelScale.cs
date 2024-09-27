using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamProject28
{
    internal class LevelScale
    {
        public void LevelUp()
        {
            GameManager.GameStart.instance.player.level += 1;

            if (GameManager.GameStart.instance.player.level > 5)
            {
                GameManager.GameStart.instance.player.job = Player.Job.담임매니저;
            } 
            else if (GameManager.GameStart.instance.player.level > 3)
            {
                GameManager.GameStart.instance.player.job = Player.Job.튜터;
            }
            else        
            {
                GameManager.GameStart.instance.player.job = Player.Job.수강생;
            }
        }
    }
}
