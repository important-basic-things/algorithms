using System.Collections.Generic;

namespace Leet.ZeroOneMatrix
{
    public static class Solution
    {
        public static int[][] UpdateMatrix(int[][] matrix)
        {
            int rowCount = matrix.Length;
            int columnCount = matrix[0].Length;
            int[][] result = CreateEmptyMatrix(rowCount, columnCount);
            int[][] visitedCells = CreateEmptyMatrix(rowCount, columnCount);
            for (int r = 0; r < rowCount; ++r)
            {
                for (int c = 0; c < columnCount; ++c)
                {
                    if (matrix[r][c] == 0)
                    {
                        result[r][c] = 0;
                        continue;
                    }
                    
                    UpdateMatrix(result, matrix, r, c, rowCount, columnCount, visitedCells);
                }
            }

            return result;
        }

        static int[][] CreateEmptyMatrix(int rowCount, int columnCount)
        {
            int[][] result = new int[rowCount][];
            for (int r = 0; r < rowCount; ++r)
            {
                result[r] = new int[columnCount];
            }

            return result;
        }

        static void UpdateMatrix(int[][] matrixToUpdate, int[][] original, int r, int c, int rowCount, int columnCount, int[][] visitedCells)
        {
            Erase(visitedCells, rowCount, columnCount);
            var toBeGrown = new List<(int row, int column)>();
            toBeGrown.Add((r, c));
            int currentStep = 0;
            while (true)
            {
                (int row, int column)[] nextToVisit = toBeGrown.ToArray();
                toBeGrown.Clear();
                foreach ((int row, int column) cell in nextToVisit)
                {
                    visitedCells[cell.row][cell.column] = 1;
                }
                
                foreach ((int row, int column) cell in nextToVisit)
                {
                    if (original[cell.row][cell.column] == 0)
                    {
                        matrixToUpdate[r][c] = currentStep;
                        return;
                    }
                    
                    AddIfGood(visitedCells, toBeGrown, GetTop(rowCount, columnCount, cell.row, cell.column));
                    AddIfGood(visitedCells, toBeGrown, GetDown(rowCount, columnCount, cell.row, cell.column));
                    AddIfGood(visitedCells, toBeGrown, GetLeft(rowCount, columnCount, cell.row, cell.column));
                    AddIfGood(visitedCells, toBeGrown, GetRight(rowCount, columnCount, cell.row, cell.column));
                }
                ++currentStep;
            }
        }

        static void Erase(int[][] visitedCells, int rowCount, int columnCount)
        {
            for (int r = 0; r < rowCount; ++r)
            {
                for (int c = 0; c < columnCount; ++c)
                {
                    visitedCells[r][c] = 0;
                }
            }
        }

        static void AddIfGood(int[][] visited, List<(int row, int column)> toBeGrown, (int row, int column) cellToAdd)
        {
            if (cellToAdd.column != -1 && visited[cellToAdd.row][cellToAdd.column] == 0)
            {
                toBeGrown.Add(cellToAdd);
            }
        }

        static (int row, int column) GetRight(int rowCount, int columnCount, int r, int c)
        {
            if (c == columnCount - 1) return (-1, -1);
            return (r, c + 1);
        }

        static (int row, int column) GetLeft(int rowCount, int columnCount, int r, int c)
        {
            if (c == 0) return (-1, -1);
            return (r, c - 1);
        }

        static (int row, int column) GetDown(int rowCount, int columnCount, int r, int c)
        {
            if (r == rowCount - 1) return (-1, -1);
            return (r + 1, c);
        }

        static (int row, int column) GetTop(int rowCount, int columnCount, int r, int c)
        {
            if (r == 0) return (-1, -1);
            return (r - 1, c);
        }
    }
}