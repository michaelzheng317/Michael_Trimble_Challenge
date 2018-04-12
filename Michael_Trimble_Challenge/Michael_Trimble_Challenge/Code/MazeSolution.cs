using System;
namespace Michael_Trimble_Challenge.Code
{
    public class MazeSolution
    {
        public int steps;
        public string solution;

        public MazeSolution(string _solution, int _steps)
        {
            solution = _solution;
            steps = _steps;
        }
    }

    public class Cell
    {
        public char Char;
        public string Path;

        public Cell(char c)
        {
            Char = c;
            Path = null;
        }
    }
}
