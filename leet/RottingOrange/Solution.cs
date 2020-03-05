using System;
using System.Collections.Generic;
using System.Linq;

namespace Leet.RottingOrange
{
    public static class Solution
    {
        public static int FindRottingTime(int[][] grid)
        {
            var rotTimeGrid = new RotTimeGrid(grid);
            return rotTimeGrid.Grow();
        }
    }

    class RotTimeGrid
    {
        private const int WillNotRot = int.MaxValue;
        private const int Empty = -1;
        private readonly int[] data;
        private readonly int rowCount;
        private readonly int columnCount;
        
        public RotTimeGrid(int[][] grid)
        {
            rowCount = grid.Length;
            columnCount = grid[0].Length;
            data = DeepCopy(grid);
            Translate(data);
        }

        private static void Translate(int[] grid)
        {
            const int empty = 0;
            const int rot = 2;
            const int normal = 1;
            for (var i = 0; i < grid.Length; i++)
            {
                switch (grid[i])
                {
                    case empty: grid[i] = Empty; break;
                    case rot: grid[i] = 0; break;
                    case normal: grid[i] = WillNotRot; break;
                    default: throw new NotSupportedException();
                }
            }
        }

        private static int[] DeepCopy(int[][] grid)
        {
            int columnLength = grid[0].Length;
            var copied = new int[grid.Length * columnLength];
            for (var rowIndex = 0; rowIndex < grid.Length; rowIndex++)
            {
                Array.Copy(
                    grid[rowIndex],
                    0,
                    copied,
                    rowIndex * columnLength,
                    columnLength);
            }

            return copied;
        }

        public int Grow()
        {
            int timestamp = 0;
            while (GrowOneTime(timestamp))
            {
                ++timestamp;
            }

            return CalculateMaxRotTime();
        }

        private int CalculateMaxRotTime()
        {
            if (data.Any(value => value == WillNotRot))
            {
                return -1;
            }

            int maxRotValue = data.Max();
            if (maxRotValue == Empty)
            {
                return 0;
            }

            return maxRotValue;
        }

        private bool GrowOneTime(int timestamp)
        {
            CellPoint[] rotCells = FindRotValue(timestamp).ToArray();
            bool grew = false;
            
            foreach (CellPoint rotCell in rotCells)
            {
                IEnumerable<CellPoint> neighbors = FindNeighbors(rotCell.RowIndex, rotCell.ColumnIndex);
                foreach (var neighbor in neighbors)
                {
                    var rotValue = this[neighbor.RowIndex, neighbor.ColumnIndex];
                    if (rotValue > timestamp + 1)
                    {
                        this[neighbor.RowIndex, neighbor.ColumnIndex] = timestamp + 1;
                        grew = true;
                    }
                }
            }

            return grew;
        }

        private IEnumerable<CellPoint> FindRotValue(int rotValue)
        {
            if (rotValue < 0) { throw new ArgumentOutOfRangeException(nameof(rotValue)); }

            for (var i = 0; i < data.Length; i++)
            {
                if (data[i] != rotValue)
                {
                    continue;
                }

                int rowIndex = i / columnCount;
                int columnIndex = i % columnCount;
                yield return new CellPoint(rowIndex, columnIndex);
            }
        }
        
        private IEnumerable<CellPoint> FindNeighbors(int rowIndex, int columnIndex)
        {
            ValidateIndex(rowIndex, columnIndex);
            if (rowIndex != 0) { yield return new CellPoint(rowIndex - 1, columnIndex); }
            if (columnIndex != columnCount - 1) { yield return new CellPoint(rowIndex, columnIndex + 1); }
            if (rowIndex != rowCount - 1) { yield return new CellPoint(rowIndex + 1, columnIndex); }
            if (columnIndex != 0) { yield return new CellPoint(rowIndex, columnIndex - 1); }
        }

        private int this[int rowIndex, int columnIndex]
        {
            get
            {
                ValidateIndex(rowIndex, columnIndex);
                return data[rowIndex * columnCount + columnIndex];
            }

            set
            {
                ValidateIndex(rowIndex, columnIndex);
                data[rowIndex * columnCount + columnIndex] = value;
            }
        }

        private void ValidateIndex(int rowIndex, int columnIndex)
        {
            if (rowIndex < 0 || rowIndex >= rowCount || columnIndex < 0 || columnIndex >= columnCount)
            {
                throw new ArgumentException($"Invalid index {rowIndex}, {columnIndex}");
            }
        }

        class CellPoint
        {
            public CellPoint(int rowIndex, int columnIndex)
            {
                RowIndex = rowIndex;
                ColumnIndex = columnIndex;
            }

            public int RowIndex { get; }
            public int ColumnIndex { get; }
        }
    }
}