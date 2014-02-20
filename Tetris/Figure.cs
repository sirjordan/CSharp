using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Figure : ICloneable
{
    private char[,] body;
    public IList<FigureElement> Elements { get; private set; }
    public Position Position;  // TopLeft of the first element
    public int Height { get; set; }
    public int Width { get; set; }

    public char[,] Body
    {
        get { return this.body; }
        private set { this.body = value; }
    }

    public Figure()
    {
        //this.Position = Position;
        this.Elements = new List<FigureElement>();
    }

    public Figure(char[,] body)
        : this()
    {
        this.body = body;
        this.Elements = GetFigureFromArray(body);
    }

    public void DisplayOnConsole()
    {
        foreach (var el in Elements)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(el.Position.Left, el.Position.Top);
            Console.Write(el.Body);
        }
    }

    public void Rotate()
    { 
        // TODO: Implement
        char[,] rotatedBody = new char[body.GetLength(1), body.GetLength(0)];   // if 2x3 -> 3x2
        for (int i = 0, j2 = 0; i < rotatedBody.GetLength(0); i++, j2++)
        {
            for (int j = 0, i2 = 0; j < rotatedBody.GetLength(1); j++, i2++)
            {
                rotatedBody[i, j] = this.body[i2, j2];
            }
        }
        // TODO: Create a new figure and raplace it with .this
    }

    public void Move(Direction direction)
    {
        if (direction == Direction.Down)
        {
            this.Position.Top++;

            foreach (FigureElement fe in Elements)
            {
                fe.Position.Top++;
            }
        }
        if (direction == Direction.Left)
        {
            this.Position.Left--;

            foreach (FigureElement fe in Elements)
            {
                fe.Position.Left--;
            }
        }
        if (direction == Direction.Right)
        {
            this.Position.Left++;

            foreach (FigureElement fe in Elements)
            {
                fe.Position.Left++;
            }
        }
    }

    private List<FigureElement> GetFigureFromArray(char[,] array)
    {
        this.Width = array.GetLength(1);
        this.Height = array.GetLength(0);

        List<FigureElement> result = new List<FigureElement>();
        
        for (int i = 0; i < array.GetLength(0); i++)
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j] != '\0')
                {
                    result.Add(new FigureElement(new Position(i + this.Position.Top, j + this.Position.Left)));
                }
            }
        }
        return result;
    }

    public object Clone()
    {
        var copied = new Figure(this.Body);
        copied.Elements = GetFigureFromArray(copied.Body);
        copied.Position = this.Position;
        for (int i = 0; i < copied.Elements.Count; i++)
        {
            copied.Elements[i].Position.Left = this.Elements[i].Position.Left;
            copied.Elements[i].Position.Top = this.Elements[i].Position.Top;
        }
        return copied;
    }
}
