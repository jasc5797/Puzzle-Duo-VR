using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maze
{
    private List<Cell> cells;

    private int rowsCount;
    private int columnCount;

    public Maze(int rowsCount, int columnCount, int startX, int startY, bool isFullRandom, int randomSeed)
    {
        this.rowsCount = rowsCount;
        this.columnCount = columnCount;

        if (isFullRandom)
        {
            Random.InitState(System.DateTime.Now.Millisecond);
        }
        else
        {
            Random.InitState(randomSeed);
        }
        GenerateMaze();
    }

    public List<Cell> GetCells()
    {
        return cells;
    }

    private void GenerateMaze()
    {
        InitializeMaze();

        int currentRow = 0;
        int currentColumn = 0;

        Cell currentCell = GetCell(currentRow, currentColumn);
        List<Cell> unvistedCells;
        Stack<Cell> cellStack = new Stack<Cell>();

        int maxIter = 1000;

        do
        {
            List<Cell> neighborCells = GetAvailableNeighbors(currentRow, currentColumn);

            if (neighborCells.Count > 0)
            {
                int randomIndex = Random.Range(0, neighborCells.Count);
                Cell randomNeighborCell = neighborCells[randomIndex];
                RemoveWallBetween(currentCell, randomNeighborCell);

                // currentCell.IsVisited = true;

                cellStack.Push(currentCell);

                randomNeighborCell.IsVisited = true;
                currentCell = randomNeighborCell;
                currentRow = currentCell.row;
                currentColumn = currentCell.column;

            }
            else if (cellStack.Count > 0)
            {
                currentCell = cellStack.Pop();
                currentRow = currentCell.row;
                currentColumn = currentCell.column;
                //currentCell.IsVisited = true;
            }

            unvistedCells = GetUnvisitedCells();
            maxIter--;
        } while (unvistedCells.Count > 0 && maxIter > 0);
        if(maxIter == 0)
        {
            Debug.Log("Max iter reached");
        }
    }

    private void InitializeMaze()
    {
        cells = new List<Cell>();
        for (int row = 0; row < rowsCount; row++)
        {
            for (int column = 0; column < columnCount; column++)
            {
                cells.Add(new Cell(row, column));
            }
        }
    }

    private List<Cell> GetUnvisitedCells()
    {
        List<Cell> unvistedCells = new List<Cell>();
        foreach (Cell cell in cells)
        {
            if (!cell.IsVisited)
            {
                unvistedCells.Add(cell);
            }
        }
        return unvistedCells;
    }

    private List<Cell> GetAvailableNeighbors(int row, int column)
    {
        List<Cell> availableNeighbors = new List<Cell>();
        //North cell
        if (IsInRange(row + 1, column) && IsCellVisited(row + 1, column))
        {
            availableNeighbors.Add(GetCell(row + 1, column));
        }
        //South cell
        if (IsInRange(row - 1, column) && IsCellVisited(row - 1, column))
        {
            availableNeighbors.Add(GetCell(row - 1, column));
        }
        //East cell
        if(IsInRange(row, column + 1) && IsCellVisited(row, column + 1))
        {
            availableNeighbors.Add(GetCell(row, column + 1));
        }
        //West cell
        if(IsInRange(row, column - 1) && IsCellVisited(row, column - 1))
        {
            availableNeighbors.Add(GetCell(row, column - 1));
        }
        return availableNeighbors;
    }

    private bool IsInRange(int row, int column)
    {
        return row >= 0 && row < rowsCount && column >= 0 && column < columnCount;
    }

    private bool IsCellVisited(int row, int column)
    {
        return GetCell(row, column).IsVisited;
    }

    private Cell GetCell(int row, int column)
    {
        foreach(Cell cell in cells)
        {
            if (cell.row == row && cell.column == column)
            {
                return cell;
            }
        }
        return null;
    }

    private void RemoveWallBetween(Cell currentCell, Cell neighborCell)
    {
        int rowDifference = currentCell.row - neighborCell.row;
        int columnDifference = currentCell.column - neighborCell.column;

        if (rowDifference == 1) //Current Cell is north of neighbor cell
        {
            currentCell.WallSouth = false;
            neighborCell.WallNorth = false;
        }
        else if (rowDifference == -1) //Current Cell is south of neighbor cell
        {
            currentCell.WallNorth = false;
            currentCell.WallSouth = false;
        }
        else if (columnDifference == 1) //Current Cell is east of neighbor cell
        {
            currentCell.WallWest = false;
            currentCell.WallEast = false;
        } 
        else if (columnDifference == -1) //Current Cell is west of neighbor cell
        {
            currentCell.WallEast = false;
            currentCell.WallWest = false;
        }
    }
}