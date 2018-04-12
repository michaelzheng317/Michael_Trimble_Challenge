using System;
using System.Collections.Generic;

namespace Michael_Trimble_Challenge.Code
{
    public class SolveMaze
    {
        private static char separator = ' ';

        /*
         * Solve maze main method
        */
        public static MazeSolution Solve(string map)
        {
            if (!validateMap(map))
            {
                return new MazeSolution("Invaild input", -1);
            }

            List<List<Cell>> matrix = convertToMatrix(map);

            Tuple<int, int> Acoordinate = findChar(matrix, 'A');

            solveMatrix(matrix, Acoordinate.Item1, Acoordinate.Item2);
            Tuple<int, int> Bcoordinate = findChar(matrix, 'B');
            if (drawPath(matrix, Acoordinate.Item1, Acoordinate.Item2, matrix[Bcoordinate.Item1][Bcoordinate.Item2].Path))
            {
                return new MazeSolution(convertToMap(matrix), matrix[Bcoordinate.Item1][Bcoordinate.Item2].Path.Length);
            }

            return new MazeSolution("No path found", -1);
        }

        /*
         * Draw path from A to B with @
        */
        private static bool drawPath(List<List<Cell>> matrix, int row, int column, string path)
        {
            foreach(char c in path)
            {
                switch(c)
                {
                    case 'U':
                        row -= 1;
                        break;
                    case 'D':
                        row += 1;
                        break;
                    case 'L':
                        column -= 1;
                        break;
                    case 'R':
                        column += 1;
                        break;
                    default:
                        return false;
                }
                if (row < 0 || column < 0 || row >= matrix.Count || column >= matrix[0].Count)
                {
                    return false;
                }
                switch(matrix[row][column].Char)
                {
                    case '#':
                    case 'A':
                        return false;
                    case 'B':
                        return true;
                    case '.':
                        matrix[row][column].Char = '@';
                        break;
                    default:
                        return false;
                }
            }
            return false;
        }

        /*
         * Solve maze in a matrix
        */
        private static void solveMatrix(List<List<Cell>> matrix, int row, int column)
        {
            List<Tuple<int, int, string>> currentList = new List<Tuple<int, int, string>>();
            currentList.Add(new Tuple<int, int, string>(row - 1, column, "U"));
            currentList.Add(new Tuple<int, int, string>(row + 1, column, "D"));
            currentList.Add(new Tuple<int, int, string>(row, column - 1, "L"));
            currentList.Add(new Tuple<int, int, string>(row, column + 1, "R"));

            matrix[row][column].Path = "";

            while(currentList.Count > 0)
            {
                List<Tuple<int, int, string>> nextList = new List<Tuple<int, int, string>>();

                foreach(Tuple<int, int, string> cell in currentList)
                {
                    updateCell(matrix, cell.Item1, cell.Item2, cell.Item3, nextList);
                }
                currentList = nextList;
            }
        }

        /*
         * Update shortest path to each cell of the matrix
        */
        private static void updateCell(List<List<Cell>> matrix, int row, int column, string path, List<Tuple<int,int, string>> nextList)
        {
            if (row < 0 || column < 0 || row >= matrix.Count || column >= matrix[0].Count)
            {
                return;
            }
            switch (matrix[row][column].Char)
            {
                case '.':
                case 'A':
                    if (matrix[row][column].Path == null || path.Length < matrix[row][column].Path.Length)
                    {
                        matrix[row][column].Path = path;
                        nextList.Add(new Tuple<int, int, string>(row - 1, column, path + "U"));
                        nextList.Add(new Tuple<int, int, string>(row + 1, column, path + "D"));
                        nextList.Add(new Tuple<int, int, string>(row, column - 1, path + "L"));
                        nextList.Add(new Tuple<int, int, string>(row, column + 1, path + "R"));
                    }
                    return;
                case 'B':
                    if (matrix[row][column].Path == null || path.Length < matrix[row][column].Path.Length)
                    {
                        matrix[row][column].Path = path;
                    }
                    return;
                case '#':
                case ' ':
                default:
                    return;
            }
        }

        /*
         * Convert matrix back to string format
        */

        private static string convertToMap(List<List<Cell>> matrix)
        {
            string map = "";
            foreach(List<Cell> row in matrix)
            {
                foreach(Cell cell in row)
                {
                    map += cell.Char;
                }
                map += '\n';
            }
            return map.Substring(0, map.Length - 1);
        }

        /*
         * Validate map
        */
        private static bool validateMap(string map)
        {
            int countA = 0;
            int countB = 0;
            foreach(char c in map)
            {
                if (c != '.' && c != '#' && c != 'A' && c != 'B' && c != separator)
                {
                    return false;
                }
                if(c == 'A')
                {
                    countA += 1;
                }
                if (c == 'B')
                {
                    countB += 1;
                }
            }
            if (countA != 1 || countB != 1)
            {
                return false;
            }
            return true;
        }

        /*
         * Find coordinate of the first character c
        */
        private static Tuple<int, int> findChar(List<List<Cell>> matrix, char c)
        {
            for (int row = 0; row < matrix.Count; row++)
            {
                for(int column = 0; column < matrix[row].Count; column++)
                {
                    if(matrix[row][column].Char == c)
                    {
                        return new Tuple<int, int>(row, column);
                    }
                }
            }
            return new Tuple<int, int>(-1, -1);
        }


        /*
        * Convert map into matrix format
       */
        private static List<List<Cell>> convertToMatrix(string map)
        {
            string[] rows = map.Split(separator);
            int columnNumber = 0;
            foreach(string row in rows)
            {
                if(row.Length > columnNumber)
                {
                    columnNumber = row.Length;
                }
            }

            List<List<Cell>> matrix = new List<List<Cell>>();

            for (int r = 0; r < rows.Length; r++)
            {
                List<Cell> row = new List<Cell>();
                for (int c = 0; c < columnNumber; c++)
                {
                    if (c >= rows[r].Length)
                    {
                        row.Add(new Cell(' '));
                    }
                    else
                    {
                        row.Add(new Cell(rows[r][c]));
                    }
                }
                matrix.Add(row);
            }

            return matrix;
        }
    }
}
