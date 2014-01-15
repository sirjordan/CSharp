using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

public class Position
{
    private int col;
    private int row;

    public int Row
    {
        get { return row; }
        set { row = value; }
    }

    public int Col
    {
        get { return col; }
        set { col = value; }
    }

    public Position(int row, int col)
    {
        this.col = col;
        this.row = row;
    }

    public Position(Position position)
    {
        this.Col = position.Col;
        this.Row = position.Row;
    }

    public override bool Equals(object obj)
    {
        var other = obj as Position;
        if ((this.Col == other.Col) && (this.Row == other.Row))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public override int GetHashCode()
    {
        return Row ^ Col;
    }
}

public class SnakeElement
{
    public Position Position { get; set; }
    public char Body { get; set; }

    public int Col
    {
        get { return this.Position.Col; }
        set { this.Position.Col = value; }
    }

    public int Row
    {
        get { return this.Position.Row; }
        set { this.Position.Row = value; }
    }

    public SnakeElement(Position position, char body)
    {
        this.Position = position;
        this.Body = body;
    }
}

public enum Direction
{
    left,
    right,
    up,
    down
}

class Snake
{
    const int Height = 40; // Plaing grid Height
    const int Width = 60; // Plaing grid Width
    static int gameSpeed = 100; // Miliseconds
    static int gameTimer = 0;
    static bool gameOn = true;
    static Position lastPosition;
    static Queue<SnakeElement> snake = new Queue<SnakeElement>(new[] { new SnakeElement(new Position(Height - 10, Width / 2), '@') }); // Head of the snake
    static Direction snakeDirection = Direction.up;
    static int score = 0;
    static Position giftPosition;

    // Game
    static void Main()
    {
        InitializeField();
        InitializeSnake();

        while (gameOn)
        {
            ProceedKeyboardEntry();
            MoveSnake();
            ColisionDispatcher();
            Render();
            Thread.Sleep(gameSpeed);
        }

        GameOver();
    }

    static void Timer()
    {
        gameTimer++;
        if (gameTimer > 10)
        {
            if (gameSpeed > 10)
            {
                gameSpeed -= 10;
            }
            gameTimer = 0;
        }
    }

    // First initializing of the snake
    static void InitializeSnake()
    {
        var headPositionRow = snake.ElementAt(0).Row;
        var headPositionCol = snake.ElementAt(0).Col;

        for (int i = 1; i < 5; i++)
        {
            var part = new SnakeElement(new Position(headPositionRow + i, headPositionCol), '*');
            snake.Enqueue(part);
        }
    }

    public static void InitializeField()
    {
        Console.SetWindowSize(Width, Height);
        Console.BackgroundColor = ConsoleColor.Blue;
        Console.Clear();

        // Remove the scrollbars
        Console.BufferHeight = Console.WindowHeight;
        Console.BufferWidth = Console.WindowWidth;

        // Create first gift
        CreateGift();
    }

    public static void ProceedKeyboardEntry()
    {
        // Move the snake thru keyboard arrows
        if (Console.KeyAvailable)
        {
            var keyInfo = Console.ReadKey();
            if (keyInfo.Key.Equals(ConsoleKey.LeftArrow))
            {
                snakeDirection = Direction.left;
            }

            if (keyInfo.Key.Equals(ConsoleKey.RightArrow))
            {
                snakeDirection = Direction.right;
            }

            if (keyInfo.Key.Equals(ConsoleKey.UpArrow))
            {
                snakeDirection = Direction.up;
            }

            if (keyInfo.Key.Equals(ConsoleKey.DownArrow))
            {
                snakeDirection = Direction.down;
            }
        }
    }

    // Place a gift at random position of the playing field
    public static void CreateGift()
    {
        int giftRow;
        int giftCol;
        Random randomPlace = new Random();

        giftCol = randomPlace.Next(0, Width - 1);
        giftRow = randomPlace.Next(0, Height - 1);
        giftPosition = new Position(giftRow, giftCol);
    }

    // When the snake hit something
    static void ColisionDispatcher()
    {
        var snakeHead = snake.ElementAt(0);

        // If the snake eat a gift
        if (snakeHead.Position.Equals(giftPosition))
        {
            // Collect the gift
            snake.Enqueue(new SnakeElement(lastPosition, '*'));
            score += 100;
            CreateGift();
            Timer(); // Accelerate the game speed ;)
        }

        // Move truth the walls
        if (snakeHead.Col <= 0)
        {
            snake.ElementAt(0).Col = Width - 1;
        }
        else if (snakeHead.Col >= Width)
        {
            snake.ElementAt(0).Col = 0;
        }
        else if (snakeHead.Row < 0)
        {
            snake.ElementAt(0).Row = Height - 1;
        }
        else if (snakeHead.Row >= Height)
        {
            snake.ElementAt(0).Row = 0;
        }

        // If snake hit herself
        for (int i = 1; i < snake.Count; i++)
        {
            if (snake.ElementAt(i).Position.Equals(snakeHead.Position))
            {
                gameOn = false;
            }
        }
    }

    static void GameOver()
    {
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("GAME OVER!");
        Console.WriteLine(string.Format("YOUR SCORE IS : {0}",score));
    }

    public static void MoveSnake()
    {
        lastPosition = new Position(snake.ElementAt(0).Position);

        switch (snakeDirection)
        {
            case Direction.left:
                snake.ElementAt(0).Col--;
                break;
            case Direction.right:
                snake.ElementAt(0).Col++;
                break;
            case Direction.up:
                snake.ElementAt(0).Row--;
                break;
            case Direction.down:
                snake.ElementAt(0).Row++;
                break;
            default:
                break;
        }
        Buffer();
    }

    static void Buffer()
    {
        for (int i = 1; i < snake.Count; i++)
        {
            Position temp = new Position(snake.ElementAt(i).Position);
            snake.ElementAt(i).Position = lastPosition;
            lastPosition = new Position(temp);
        }
    }

    // Draw the game elements to the console
    static void Render()
    {
        Console.Clear();
        // Render score
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.SetCursorPosition(1, 0);
        Console.Write(string.Format("{0} : {1}","Score",score));

        //Render gift
        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(giftPosition.Col, giftPosition.Row);
        Console.Write('$');

        // Render snake
        Console.ForegroundColor = ConsoleColor.Cyan;
        foreach (var sn in snake)
        {
            Console.SetCursorPosition(sn.Col, sn.Row);
            Console.Write(sn.Body);
        }
    }
}
