using System;
using System.Threading;

namespace Shadow
{
    public class Player
    {
        //Public Variables
        public bool canControl;
        public bool isGoingUp;
        //Maintains where the player is located.
        public int playerX;
        public int playerY;
        public int playerHealth = 3;

        int playAreaH;
        int playAreaW;
        //Seconds until gravity takes affect.
        int dropTime = 3;
        //How fast the player will fall.
        float dropRate = 200;
        float countDown;
        float willDrop;
        bool upDraw;
        bool rightDraw;


        public Player(int playAreaW, int playAreaH, bool canControl)
        {
            this.playAreaW = playAreaW;
            this.playAreaH = playAreaH;
            this.canControl = canControl;
            

            playerX = (playAreaW / 2);
            playerY = (playAreaH / 2);
        }

        public void playerInital()
        {
            Console.SetCursorPosition(playAreaW / 2, (playAreaH - 1));
            playerX = Console.CursorLeft;
            playerY = Console.CursorTop;
            isGoingUp = false;
            Console.Write("l~~~~l");
        }

        /// <summary>
        /// Player Movement Controls
        /// </summary>
        public void playerMovement()
        {
            if (canControl)
            {
                if (Console.KeyAvailable)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.UpArrow:
                            upDraw = !upDraw;
                            break;

                        case ConsoleKey.RightArrow:
                            rightDraw = !rightDraw;
                            break;

                        case ConsoleKey.A:
                            if (playerX > 1)
                            {
                                playerX--;
                            }
                            else
                            {
                                playerX += 2;
                            }
                            break;

                        case ConsoleKey.D:
                            if (playerX < (playAreaW - 6))
                            {
                                playerX++;
                            }
                            else
                            {
                                playerX -= 1;
                            }
                            break;

                        case ConsoleKey.S:
                            //if (playerY < (playAreaH - 2) && playerX > 1)
                            //{
                            //    playerY++;
                            //}
                            break;

                        case ConsoleKey.W:
                            if (playerY > 1)
                            {
                                playerY--;
                                isGoingUp = true;
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }

        public void playerGravity()
        {
            if (isGoingUp)
            {
                countDown++;
                if (countDown > (dropTime * 1000))
                {
                    isGoingUp = false;
                }
            }
            else
            {
                //Handles the gravity of the player, uses WillDrop to make the player drop at a slower rate.
                countDown = 0;

                if (playerY >= 0 && isGoingUp == false && playerY <= playAreaH - 2)
                {
                    if (willDrop > dropRate)
                    {
                        willDrop = 0;
                        playerY++;
                    }
                    else
                    {
                        willDrop++;
                    }
                }
            }
        }

        public void drawPlayer(int px, int py)
        {
            if (px > 1)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                if (isGoingUp)
                {
                    if (playerY < 2)
                    {
                        Console.SetCursorPosition((playerX), (playerY + 1));
                        Console.Write("    ");
                    }
                    Console.SetCursorPosition((playerX), (playerY));
                    Console.Write(" ** ");
                    Console.SetCursorPosition((playerX), (playerY + 1));
                    Console.Write("    ");
                }
                else
                {
                    if (playerY > 1)
                    {
                        Console.SetCursorPosition((playerX), (playerY - 1));
                        Console.Write("    ");
                    }
                    Console.SetCursorPosition((playerX), (playerY));
                    Console.Write(" ** ");
                    if (playerY < playAreaH - 1 && playerY > 2)
                    {
                        Console.SetCursorPosition((playerX), (playerY - 1));
                        Console.Write("    ");
                    }
                }
            }

            if (upDraw)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write(" |----| ");
            }
        }
    }
}
