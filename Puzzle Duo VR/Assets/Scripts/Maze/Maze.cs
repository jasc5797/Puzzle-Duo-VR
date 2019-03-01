using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze
{
    private Cell[,] cells;

    private int rowsCount;
    private int columnCount;

    public Maze(int rowsCount, int columnCount)
    {
        this.rowsCount = rowsCount;
        this.columnCount = columnCount;

        cells = GenerateCells();
    }

    private Cell[,] GenerateCells()
    {
        Cell[,] cells = new Cell[rowsCount, columnCount];

        return cells;
    }

    public bool IsInRange(int x, int y)
    {
        return x >= 0 && x < rowsCount && y >= 0 && y < columnCount;
    }
}
