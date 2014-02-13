using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FallingRocks
{
    public class FallingRocks
    {
        static int windowWidth = 50;
        static int windowHeight = 31;
        static bool runGame = true;
        static int playerPosition = windowWidth / 2;
        static int gameSpeed = 150;
        static char[] rockSymbols = new char[] { '!', '@', '#', '$', '%', '^', '&', '*' };
        static string player = "(-)";       
        static int difficult = 3;
        static Stack<string> rockRows = new Stack<string>(windowHeight);
        static Random randomNumber = new Random();

        static void Main() 
        {
            Initialize();

            while (runGame)
            {
                ProcceedKeyboardEntry();
                Thread.Sleep(gameSpeed);
                Console.Clear();
                DisplayPlayer();
                DisplayRocks();
                rockRows.Push(GenerateRockRow());
                CheckCollusion();
            }

            // Game over
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("GAME OVER!");
        }

        static void CheckCollusion()
        {
            if (rockRows.Count > windowHeight - 1)
            {
                string lastRow = rockRows.ElementAt(windowHeight - 1);

                for (int i = 0; i < lastRow.Length; i++)
                {
                    if (lastRow[i] != '\0')
                    {
                        if (i == playerPosition || i == playerPosition + 1 || i == playerPosition + 2)
                        {
                            runGame = false;
                        }
                    }
                }
            }
        }

        static void DisplayPlayer() 
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(playerPosition, windowHeight - 1);
            Console.Write(player);
        }

        static void DisplayRocks()
        {
            for (int i = 0; i < rockRows.Count; i++)
            {
                if (i < windowHeight - 1)
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.SetCursorPosition(0, i);
                    Console.Write(rockRows.ElementAt(i));
                }
                else
                {
                    continue;
                }
            }
        }

        static void ProcceedKeyboardEntry()
        {
            while (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyboardEntry = Console.ReadKey();

                if (keyboardEntry.Key == ConsoleKey.LeftArrow)
                {
                    if (playerPosition > 1)
                    {
                        playerPosition--;
                    }
                }

                if (keyboardEntry.Key == ConsoleKey.RightArrow)
                {
                    if (playerPosition < windowWidth - 4)
                    {
                        playerPosition++;
                    }
                }

            }
        }

        static void Initialize() 
        {
            Console.WindowHeight = windowHeight;
            Console.WindowWidth = windowWidth;
            Console.BufferHeight = windowHeight;
            Console.BufferWidth = windowWidth;
        }

        static int GenerateRandom(int min, int max) 
        {
            return randomNumber.Next(min, max);
        }

        static string GenerateRockRow()
        {
            char[] rocks = new char[windowWidth];
            int numberOfRocks = GenerateRandom(0, difficult);
            for (int i = 0; i < numberOfRocks; i++)
            {
                char rock = rockSymbols[GenerateRandom(0, rockSymbols.Length)];
                int rockPosition = GenerateRandom(0, rocks.Length);
                rocks[rockPosition] = rock;
            }

            StringBuilder output = new StringBuilder();
            for (int i = 0; i < rocks.Length; i++)
            {
                output.Append(rocks[i]);
            }
            return output.ToString();
        }
    }
}
