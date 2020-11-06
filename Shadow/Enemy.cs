using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shadow
{
    class Enemy
    {
        public enum enemyMovement { Left, Right }
        int[] enemy = new int[4];
        int enemyCount;
        enemyMovement mov;
        Player player;

        //Private Enemy Stats
        int playAreaW = 0;
        int playAreaH = 0;
        //Used for clocks, slowing down enemy movement & changing rate of movement.
        int counts = 0;
        int willMove = 0;
        //Used to control speed of enemy.
        int enemySpeed = 500;

        public Enemy(int playAreaW, int playAreaH,int enemyCount, Player player)
        {
            this.playAreaW = playAreaW;
            this.playAreaH = playAreaH;
            this.player = player;

            this.enemyCount = enemyCount;
        }
        public void randomizeSpawn(Random spawnRNG)
        {
            enemy[1] = spawnRNG.Next(3, playAreaW - 5);
            enemy[2] = spawnRNG.Next(4, playAreaH - 1);
        }

        public void updateEnemy(Random rng)
        {
            //Randomly decides moves at random intervals
            if (counts >= (rng.Next(1000, 5000)))
            {
                int move = rng.Next(0, Enum.GetNames(typeof(enemyMovement)).Length);

                mov = (enemyMovement)move;

                willMove++;
                movement();
            }
            counts++;
            hitDetection(rng);
        }

        public void movement()
        {
            switch (mov)
            {
                case enemyMovement.Left:
                    {
                        if (enemy[1] > 2 && enemy[1] < playAreaW)
                        {
                            if (willMove > enemySpeed)
                            {
                                enemy[1] -= 1;
                                willMove = 0;
                            }
                        }
                        else
                        {
                            enemy[1] += 2;
                        }
                        break;
                    }
                case enemyMovement.Right:
                    {
                        if (enemy[1] > 2 && enemy[1] < playAreaW)
                        {
                            if (willMove > enemySpeed)
                            {
                                enemy[1] += 1;
                                willMove = 0;
                            }
                        }
                        else
                        {
                            enemy[1] -= 2;
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

        public void hitDetection(Random rng)
        {
            //Makes hit area square around enemy
            if ((player.playerX < (enemy[1] + 6) && player.playerX > (enemy[1] - 3)) && player.playerY == enemy[2])
            {
                player.playerHealth--;
                Console.SetCursorPosition((enemy[1]), (enemy[2]));
                Console.Write("      ");
                randomizeSpawn(rng);
            }
        }

        public void drawEnemy()
        {
            if (enemy[1] > 1)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.SetCursorPosition((enemy[1]), (enemy[2]));
                Console.Write(" ~--~ ");
            }
        }
    }
}