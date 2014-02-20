using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class FigureElement
{
    private const char BodyElementCharacter = '*';

    public char Body { get; private set; }
    public Position Position;

    public FigureElement(Position Position)
    {
        this.Body = BodyElementCharacter;
        this.Position = Position;
    }

    public override bool Equals(object obj)
    {
        var other = obj as FigureElement;
        if ((this.Position.Left == other.Position.Left) &&(this.Position.Top == other.Position.Top))
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
        // TODO: Implement self getHashCode
        return base.GetHashCode();
    }
}

