using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shadow
{
    public class Program
    {
        public delegate void del();
        //Sets the play area. [******]
        public static int playAreaW = (Console.WindowWidth - 1);
        public static int playAreaH = (Console.WindowHeight - 1);
        //Enables/Disables player control.
        public static bool canControl = false;
        //How many enemies
        public static int enemyCount = 5;
        static bool playAgain = true;
        public static int playerScore = 0;

        static void Main(string[] args)
        {
            while (playAgain)
            {
                Player player = new Player(playAreaW, playAreaH, canControl);
                Berries berry = new Berries(playAreaW, playAreaH, player);

                //Used for spawning multiple enemies
                Enemy[] enemies = new Enemy[enemyCount];
                for (int ndx = 0; ndx < enemyCount; ndx++)
                {
                    enemies[ndx] = new Enemy(playAreaW, playAreaH, enemyCount, player);
                }

                bool doRestart = false;
                initialization();
                int orgWidth = Console.WindowWidth;
                int orgHeight = Console.WindowHeight;

                setBoundaries();
                player.playerInital();
                drawTitle();
                //Inital loop waiting for the Enter key to start the game.
                while (!canControl)
                {
                    //Random RNG for spawning enemies;
                    Random spawnRNG = new Random();

                    Console.CursorVisible = false;
                    for (int ndx = 0; ndx < enemyCount; ndx++)
                    {
                        enemies[ndx].randomizeSpawn(spawnRNG);
                    }
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        canControl = true;
                        player.canControl = true;
                        Console.Clear();
                        initialization();
                        setBoundaries();
                        Console.SetCursorPosition((player.playerX), (player.playerY));
                    }
                }

                //Loop for the game.
                while (canControl)
                {

                    playerScore = berry.playerScore;
                    Random updateRNG = new Random();
                    Console.CursorVisible = false;
                    player.playerMovement();
                    player.playerGravity();
                    //Task.Factory.StartNew(del.CreateDelegate(typeof(Action),berry, player.playerGravity()));
                    if (!(Console.WindowWidth == orgWidth) || !(Console.WindowHeight == orgHeight))
                    {
                        Console.Clear();
                        setBoundaries();
                        player.playerInital();
                        Console.SetWindowSize(orgWidth, orgHeight);
                    }

                    berry.spawnBerries();
                    player.drawPlayer(player.playerX, player.playerY);

                    //Part of spawning multiple enemies
                    for (int ndx = 0; ndx < enemyCount; ndx++)
                    {
                        enemies[ndx].updateEnemy(updateRNG);
                        enemies[ndx].drawEnemy();
                    }
                    //Checks if player should be dead.
                    if (player.playerHealth <= 0)
                    {
                        canControl = false;
                    }
                }


                //END SCREEN FOR GAME
                drawEnd(playerScore);
                doRestart = false;

                while (!doRestart)
                {
                    if (Console.ReadKey(true).Key == ConsoleKey.Enter)
                    {
                        doRestart = true;
                        playAgain = false;
                    }
                    else if (Console.ReadKey(true).Key == ConsoleKey.R)
                    {
                        doRestart = true;
                        playAgain = true;
                    }
                }
            }
        }

        public static void initialization()
        {

            Console.ForegroundColor = ConsoleColor.Red;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
        }

        public static void setBoundaries()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            for (int ndx = 0; ndx < playAreaW; ndx++)
            {
                if (ndx == 0)
                {
                    Console.Write("|");
                }
                else
                {
                    Console.Write("*");
                }
            }
            for (int ndx = 0; ndx < playAreaH; ndx++)
            {
                Console.Write("|");
                Console.SetCursorPosition(0, ndx);
                Console.WriteLine("|");
                Console.SetCursorPosition(playAreaW, (ndx + 1));
            }
            for (int ndx = 0; ndx < playAreaW; ndx++)
            {
                if (ndx == 0)
                {
                    Console.SetCursorPosition(ndx, playAreaH);
                    Console.Write(" ");
                }
                else
                {
                    Console.Write("*");
                }
            }
        }

        public static void drawTitle()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(((playAreaW / 2) - 25), (playAreaH / 2 - 5));
            Console.Write("   ______                            _______           ");
            Console.SetCursorPosition(((playAreaW / 2) - 25), (playAreaH / 2) - 4);
            Console.Write("  (____  \\                          (_______)          ");
            Console.SetCursorPosition(((playAreaW / 2) - 25), (playAreaH / 2) - 3);
            Console.Write("   ____)  )_____  ____ ____ _   _    _____ _   _ ____  ");
            Console.SetCursorPosition(((playAreaW / 2) - 25), (playAreaH / 2) - 2);
            Console.Write("  |  __  (| ___ |/ ___) ___) | | |  |  ___) | | |  _ \\ ");
            Console.SetCursorPosition(((playAreaW / 2) - 25), (playAreaH / 2) - 1);
            Console.Write("  | |__)  ) ____| |  | |   | |_| |  | |   | |_| | | | |");
            Console.SetCursorPosition(((playAreaW / 2) - 25), (playAreaH / 2) + 0);
            Console.Write("  |______/|_____)_|  |_|    \\__  |  |_|   |____/|_| |_|");
            Console.SetCursorPosition(((playAreaW / 2) - 25), (playAreaH / 2) + 1);
            Console.Write("                           (____/                      ");
            Console.SetCursorPosition(((playAreaW / 2) - 9), (playAreaH / 2) + 2);
            Console.Write("");
            Console.SetCursorPosition(((playAreaW / 2) - 14), (playAreaH / 2) + 3);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Collect Berries and Avoid Enemies");
            Console.SetCursorPosition(((playAreaW / 2) - 9), (playAreaH / 2) + 4);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("PRESS ENTER TO START");
            Console.SetCursorPosition(((playAreaW / 2) - 19), (playAreaH / 2) + 6);
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.Write("BY:  Joseph McIntyre - Nikhil Prakashbabu");
        }

        public static void drawEnd(int score)
        {
            Console.Clear();
            Console.SetCursorPosition(((playAreaW / 2) - 12), (playAreaH / 2) - 1);
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Berries Collected: " + playerScore);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(((playAreaW / 2) - 10), (playAreaH / 2) + 1);
            Console.Write("Press R to retry");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(((playAreaW / 2) - 16), (playAreaH / 2) + 3);
            Console.Write("Press Enter to quit the game");
        }
    }
}
