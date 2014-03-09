using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class StaticMethods
{
    // The method 'rotates' a 2d array 90 deg.  clockwise
    // TODO: parameter for number of flips
    public static char[,] Flip2DArray(char[,] input)
    {
        // The result will be a new array with switched array dimentinons
        char[,] result = new char[input.GetLength(1), input.GetLength(0)];
        for (int i = 0; i < input.GetLength(0); i++)
        {
            for (int j = 0; j < input.GetLength(1); j++)
            {
                result[j, i] = input[i, j];
            }
        }

        for (int i = 0; i < result.GetLength(0); i++)
        {
            for (int j = 0; j < result.GetLength(1) / 2; j++)
            {
                var tempLeft = result[i, j];
                result[i, j] = result[i, result.GetLength(1) - 1 - j];
                result[i, result.GetLength(1) - 1 - j] = tempLeft;
            }
        }

        return result;
    }
}
