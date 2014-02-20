using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

public class FigurePrototypeManager
{
    const string FilePath = "../../figuresPrototypes.txt";
    private static IList<Figure> loadedFigures;
    private static Random randomGen = new Random();

    public FigurePrototypeManager(bool loadFromFile)
    {
        loadedFigures = new List<Figure>();
        if (loadFromFile)
        {
            // load from txt file
            Load();
        }
        else
        {
            // load from inline written figure elements
            loadedFigures = LoadInlineFigures();
        }
    }

    private static void Load()
    {
        StringBuilder input = new StringBuilder();
        try
        {
            using (StreamReader textFileReadeder = new StreamReader(FilePath))
            {
                string line;
                while ((line = textFileReadeder.ReadLine()) != null)
                {
                    if (line == string.Empty)
                    {
                        // create new object of type figure
                    }
                    input.AppendLine(line);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("The file could not be read!");
            Console.WriteLine(e.Message);
        }
        // TODO: Streamreader read and create figures from .txt file,
        // and method for serialization and parse the .txt info into Figure data.
    }

    // Get specific figure thruth position
    public Figure GetFigure(int fromPosition)
    {
        var specificFigure = loadedFigures[fromPosition];
        return new Figure(specificFigure.Body);
    }

    // Get random figure
    public Figure GetRandomFigure()
    {
        var randomFigure = loadedFigures[randomGen.Next(0, loadedFigures.Count)];
        return new Figure(randomFigure.Body);
    }

    private static List<Figure> LoadInlineFigures()
    {
        // Primari initialization of all possible game elements
        List<Figure> gameElementsContainer = new List<Figure>();

        gameElementsContainer.Add(new Figure(new char[,] { { '1', '1', '1' }, { '\0', '\0', '1' } }));
        gameElementsContainer.Add(new Figure(new char[,] { { '1', '1' }, { '1', '1' } }));
        gameElementsContainer.Add(new Figure(new char[,] { { '1', '1', '1', '1' } }));
        gameElementsContainer.Add(new Figure(new char[,] { { '1', '1', '\0' }, { '\0', '1', '1' } }));

        return gameElementsContainer;
    }
}

