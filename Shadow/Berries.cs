using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadow
{
    class Berries
    {
        public int playerScore = 0;

        int fruitcount = 0;
        int playAreaW = 0;
        int playAreaH = 0;
        int[] berry = new int[2];
        Player player;
       
        public Berries(int playAreaW,int playAreaH,Player player)
        {
            this.playAreaW = playAreaW;
            this.playAreaH = playAreaH;
            this.player = player;
        }
        public void spawnBerries()
        {
            updateScore();
            if (fruitcount < 1)
            {
                Random rng = new Random();
                berry[0] = rng.Next(3, playAreaW - 2);
                berry[1] = rng.Next(1, playAreaH - 2);
                Console.SetCursorPosition(berry[0], berry[1]);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("■");
                fruitcount++;
            }
            BerryHit();
        }
        public void BerryHit()
        {
            //Direct location of berry on playerX & Y, doesn't work as the player's position doesn't exactly match that of the berry.
            /*(player.playerX == berry[0] && player.playerY == berry[1])*/

            //Creates a diagonal rectangle around the berry for hit collision.
            if ((player.playerX < (berry[0] + 1) && player.playerX > (berry[0] - 6)) && player.playerY == berry[1])
            {
                //Increase Score
                playerScore++;
                //Removes Berry
                Console.SetCursorPosition(berry[0], berry[1]);
                Console.Write(" ");
                //Respawns the Berry
                fruitcount = 0;
                spawnBerries();
            }
        }
        //Used for player to gain healthy after certain score;
        //public void gainHealth()
        //{
        //    bool gain = false;

        //    if (!gain)
        //    {
        //        if (playerScore > 5)
        //        {
        //            player.playerHealth++;
        //            gain = true;
        //        }
        //    }
        //}
        public void updateScore()
        {
            Console.SetCursorPosition(2,1);
            Console.Write("Score: " + playerScore);
            Console.SetCursorPosition(2, 2);
            Console.Write("Health: " + player.playerHealth);
        }
    }
}
