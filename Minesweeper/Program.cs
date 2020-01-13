namespace Minesweeper
{
    using System;

    internal class Program
    {
        internal static void Main(string[] args)
        {
            var menuSelect = MenuSelect();

            if (menuSelect == "2")
            {
                Environment.Exit(0);
            }

            var minefield = PopulateMinefield();

            bool gameover;
            do
            {
                Console.Clear();

                var displayMinefield = DisplayMinefield(minefield);

                Console.WriteLine("  0 1 2 3 4 5 6 7 8 9");
                for (var r = 0; r < 10; r++)
                {
                    Console.Write(r + " ");

                    for (var c = 0; c < 10; c++)
                    {
                        Console.Write(displayMinefield[r, c] + " ");
                    }

                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.Write("Row: ");
                var rowSelect = Console.ReadKey().KeyChar.ToString();

                Console.WriteLine();
                Console.Write("Column: ");
                var columnSelect = Console.ReadKey().KeyChar.ToString();

                int.TryParse(rowSelect, out int row);
                int.TryParse(columnSelect, out int column);

                minefield = SweepLocation(row, column, minefield, out bool isGameOver);
                gameover = isGameOver;

            } while (gameover == false);

            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Game Over");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        private static string[,] DisplayMinefield(int[,] minefield)
        {
            var minefieldOverlay = new string[10, 10];

            for (var r = 0; r < 10; r++)
            {
                for (var c = 0; c < 10; c++)
                {
                    // location cleared
                    if (minefield[r, c] == -1)
                    {
                        minefieldOverlay[r, c] = "0";
                    }
                    else
                    {
                        // location not cleared
                        minefieldOverlay[r, c] = "X";
                    }
                }
            }

            return minefieldOverlay;
        }

        private static string MenuSelect()
        {
            Console.WriteLine("1) New Game");
            Console.WriteLine("2) Exit");

            return Console.ReadKey().KeyChar.ToString();
        }

        private static int[,] PopulateMinefield()
        {
            var minefield = new int[10, 10];
            var rand = new Random();

            for (var r = 0; r < 10; r++)
            {
                for (var c = 0; c < 10; c++)
                {
                    var placement = rand.Next(0, 9);

                    // difficulty modifier
                    // can be moved to menu selection
                    if (placement <= 3)
                    {
                        // place a mine
                        minefield[r, c] = 1;
                    }
                    else
                    {
                        // no mine
                        minefield[r, c] = 0;
                    }
                }
            }

            return minefield;
        }

        private static int[,] SweepLocation(int row, int column, int[,] minefield, out bool isGameOver)
        {
            if (minefield[row, column] == 0)
            {
                // no mine found, continue game
                minefield[row, column] = -1;
                isGameOver = false;
            }
            else
            {
                // mine exploded, gameover
                isGameOver = true;
            }

            return minefield;
        }
    }
}
