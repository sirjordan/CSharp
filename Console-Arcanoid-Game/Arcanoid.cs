using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

public class Rocket
{
    public int LeftRocketPosition;
    public int RightRocketPosition;
    public int hPosition;

    public Rocket(int hPos, int left, int right)
    {
        this.hPosition = hPos;
        this.LeftRocketPosition = left;
        this.RightRocketPosition = right;
    }
    public void Launch()
    {
        Console.SetCursorPosition(LeftRocketPosition, hPosition);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write('^');
        Console.SetCursorPosition(RightRocketPosition, hPosition);
        Console.Write('^');
        hPosition--;

    }
}

public class bingBang
{
    static bool nuclearLaunchDetected = false;
    static Rocket missle;
    static int playGroundHeight = 50;
    static int score = 0;
    static int rightBorder = Console.WindowWidth - 2;
    static int bottomBorder = playGroundHeight - 1;
    static int timer = 90; // miliseconds for delay , smaller is faster
    static int positionX = rightBorder / 2; // in the middle of nowhere
    static int positionY = 6;   // on the top, below the bricks
    static bool moveDown = true;
    static bool moveRight = true;
    public static char[,] bricks = new char[4, Console.WindowWidth - 1];   // the whole playground
    static int paddleLenght = 16;
    static int paddlePosition = positionX - paddleLenght / 2;
    static char[] paddle = new char[paddleLenght];
    static bool runGame = true;

    static void Main()
    {
        //getting the bricks into the array;
        for (int i = 0; i < bricks.GetLength(0); i++)
        {
            for (int j = 0; j < bricks.GetLength(1); j++)
            {
                bricks[i, j] = '#';
            }
        }
        // getting the paddle
        for (int i = 0; i < paddleLenght; i++)
        {
            paddle[i] = '=';
        }

        RemoveScrolls();
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        while (runGame)
        {
            // moving the paddle keys
            if (Console.KeyAvailable)
            {
                ConsoleKeyInfo keyInfo = Console.ReadKey();
                if ((keyInfo.Key == ConsoleKey.LeftArrow) && (paddlePosition > 1))
                {
                    paddlePosition -= 2;
                }
                if ((keyInfo.Key == ConsoleKey.RightArrow) && (paddlePosition <= rightBorder - paddleLenght - 1))
                {
                    paddlePosition += 2;
                }
                if ((keyInfo.Key == ConsoleKey.Spacebar) && (!nuclearLaunchDetected))
                {
                    // launch the rocket ; ) myxaxa
                    // creating a new missle
                    missle = new Rocket(bottomBorder, paddlePosition, paddlePosition + paddleLenght -1 );
                    // when it is launched 
                    nuclearLaunchDetected = true;
                }
            }

            Thread.Sleep(timer);
            Console.Clear();
            BrickColusion(); // checks the brick is hitten
            DrawBricks();   // draw the ramain bricks
            MoveBall();     // draw the ball
            DrawPaddle();   // draw the paddle
            Score();    // display the current score
            HitGround();
            // when hit the spacebar launch a missles from the paddle
            if ((nuclearLaunchDetected) && (missle.hPosition > 0))
            {
                missle.Launch();
            }
            else
            {
                nuclearLaunchDetected = false;
            }
            
            Console.SetCursorPosition(positionX, positionY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("*");
        }
    }

    // counting the score
    static void Score()
    {
        Console.SetCursorPosition(0, 0);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("Score: {0}", score);

    }
    //Remove Scrol Bars of the console
    static void RemoveScrolls()
    {
        Console.WindowHeight = 50;
        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.WindowWidth;
    }

    //draw bricks
    static void DrawBricks()
    {
        Console.SetCursorPosition(0, 1);
        Console.ForegroundColor = ConsoleColor.White;

        for (int i = 0; i < bricks.GetLength(0); i++)
        {
            StringBuilder drawing = new StringBuilder();
            for (int j = 0; j < bricks.GetLength(1); j++)
            {
                drawing.Append(bricks[i, j]);
            }
            Console.WriteLine(drawing);
        }
    }

    // moving the ball
    static void MoveBall()
    {
        if (positionY >= bottomBorder)
        {
            moveDown = false;
        }
        else if (positionY < 1)  // well when hit a brick too
        {
            moveDown = true;
        }
        if (positionX >= rightBorder)
        {
            moveRight = false;
        }
        else if (positionX < 1)
        {
            moveRight = true;
        }

        // changing directions
        if (moveDown)
        {
            positionY++;
        }
        else
        {
            positionY--;
        }

        if (moveRight)
        {
            positionX++;
        }
        else
        {
            positionX--;
        }
    }

    // check if there a colusion
    static void BrickColusion()
    {
        bool hit = false;
        for (int i = 0; i < bricks.GetLength(0); i++)   // e.g. the rows of the brick array
        {
            if ((bricks[i, positionX] == '#') && (positionY == i + 1))
            {
                bricks[i, positionX] = ' ';
                score += 50;
                hit = true;
                break;
            }
        }
        if (hit)
        {
            if (moveDown)
            {
                moveDown = false;
            }
            else
            {
                moveDown = true;
            }
        }
    }

    // hit the ground
    static void HitGround()
    {
        if (positionY >= bottomBorder )// e.g. you miss the ball and it hit the ground
        {
            if ((positionX < paddlePosition) || (positionX > paddlePosition + paddleLenght))
            {
                // you loose
                runGame = false;
                DisplayGameOver();
            }
            else
            {
                // add  points for the catching the ball
                score += 10;
            }
            
        }

    }

    // when the game ends
    static void DisplayGameOver()
    {
        Console.SetCursorPosition(0, 0);
        Console.Clear();
        Console.WriteLine("GAME OVER\n\nYour score is {0}", score);
    }

    // drawing the paddle
    static void DrawPaddle()
    {
        Console.SetCursorPosition(paddlePosition, bottomBorder);
        Console.ForegroundColor = ConsoleColor.Yellow;
        foreach (var item in paddle)
        {
            Console.Write(item);
        }
    }
}
