using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

public static class Game
{
    const int PlayGroundWidth = 20;
    const int PlayGroundHeight = 30;
    static bool runGame = true;
    static int gameSpeed = 200;     //ms
    static Stack<Figure> createdElements;   // list of all created elements
    static FigurePrototypeManager figureManager;
    static Figure currentFigure;
    static int score;

    static void Main()
    {
        // GAME ON
        Initialize();
        currentFigure = figureManager.GetRandomFigure();    // create the first figure
        createdElements.Push(currentFigure);

        while (runGame)
        {
            Console.Clear();
            ProcceedKeyboardEntry();

            if (currentFigure.Position.Top + currentFigure.Height < PlayGroundHeight)
            {
                // Creating a copy to figure the next move and shift the copy and the original
                Figure copy = currentFigure.Clone() as Figure;
                createdElements.Pop();
                createdElements.Push(copy);

                if (copy.Position.Top + copy.Height < PlayGroundHeight)
                {
                    copy.Move(Direction.Down);
                    if (!hasColusion(copy, createdElements))
                    {
                        // The regular falling down
                        currentFigure.Move(Direction.Down);
                    }
                    else
                    {
                        // shift them back
                        createdElements.Pop();
                        createdElements.Push(currentFigure);
                        GetNextFigure();
                    }
                }
            }
            else
            {
                GetNextFigure();
            }

            // Display all stacked figures
            DisplayTetrisGrid();
            Thread.Sleep(gameSpeed);

            // Check for full rows and explode them
            for (int i = 0; i < PlayGroundHeight; i++)
            {
                if (CheckFullRow(createdElements, i, PlayGroundWidth))
                {
                    ExplodeTetrisRow(i);
                    // Move the upper rows on down
                    for (int j = i -1 ; j > 0; j--)
                    {
                        FallDownTetris(createdElements, j);
                    }
                }
            }

            // Remove the figures that not contain any figure elements
            // TODO: RemoveCachedEmptyFigures(createdElements); 
        }

        // Game Over
    }

    static void GetNextFigure()
    {
        // Make new figure
        currentFigure = new Figure();
        currentFigure = figureManager.GetRandomFigure();
        createdElements.Push(currentFigure);

        // add some scrore ;)
        score += 10;
    }

    static void Initialize()
    {
        createdElements = new Stack<Figure>();
        // setting the manager not to browse figures from files tru the constructor parameter
        figureManager = new FigurePrototypeManager(false);

        // set the window properties
        Console.WindowHeight = PlayGroundHeight + 1;
        Console.WindowWidth = PlayGroundWidth;
        Console.BufferHeight = PlayGroundHeight + 1;
        Console.BufferWidth = PlayGroundWidth;
    }

    static void ProcceedKeyboardEntry()
    {
        while (Console.KeyAvailable)
        {
            ConsoleKeyInfo keyboardEntry = Console.ReadKey();

            if (keyboardEntry.Key == ConsoleKey.LeftArrow)
            {
                if (currentFigure.Position.Left > 0)
                {
                    currentFigure.Move(Direction.Left);
                }
            }

            if (keyboardEntry.Key == ConsoleKey.RightArrow)
            {
                if (currentFigure.Position.Left + currentFigure.Width < PlayGroundWidth)
                {
                    currentFigure.Move(Direction.Right);
                }
            }

            if (keyboardEntry.Key == ConsoleKey.DownArrow)
            {
                if (currentFigure.Position.Top + currentFigure.Height < PlayGroundHeight)
                {
                    currentFigure.Move(Direction.Down);
                }
            }
            if (keyboardEntry.Key == ConsoleKey.Spacebar)
            {
                // TODO: Rotate the current figue
                currentFigure.Rotate();
            }
        }
    }

    static void DisplayTetrisGrid()
    {
        // Display elements
        foreach (var elements in createdElements)
        {
            elements.DisplayOnConsole();
        }

        // Display score and level
        DisplayScore();
    }

    static void DisplayScore()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.SetCursorPosition(0, 0);
        Console.Write(string.Format("{0}:{1}", "Score", score));
    }

    static bool hasColusion(Figure figure, IEnumerable<Figure> otherElements)
    {
        foreach (Figure allFigures in otherElements)
        {
            foreach (FigureElement figureElementInAllFigures in allFigures.Elements)
            {
                foreach (FigureElement figureElementInCurrent in figure.Elements)
                {
                    if ((figureElementInCurrent.Equals(figureElementInAllFigures)) && (figure != allFigures))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }

    // Check is there full row
    static bool CheckFullRow(IEnumerable<Figure> elements, int rowNumber, int allColumns)
    {
        int colCounter = 0;

        foreach (Figure figure in elements)
        {
            foreach (FigureElement figureElement in figure.Elements)
            {
                if (figureElement.Position.Top == rowNumber)
                {
                    colCounter++;
                }
            }
        }

        if (colCounter == allColumns)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    static void ExplodeTetrisRow(int row)
    {
        foreach (Figure figure in createdElements)
        {
            for (int i = 0; i < figure.Elements.Count; i++)
            {
                if (figure.Elements[i].Position.Top == row)
                {
                    figure.Elements.RemoveAt(i);
                    i--;
                }
            }
        }

        // Add some bonus score
        score += 100;
    }

    static void RemoveCachedEmptyFigures()
    {
        // TODO: Implement
    }

    static void FallDownTetris(IEnumerable<Figure> elements, int rowToMoveDown)
    {
        foreach (Figure figure in elements)
        {
            foreach (FigureElement figureElement in figure.Elements)
            {
                if (figureElement.Position.Top == rowToMoveDown)
                {
                    figureElement.Position.Top++;
                }
            }
        }
    }
}

