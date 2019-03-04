using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TunnelType
{
    Straight,
    Cross,
    End,
    Bend
}

public class Cell
{
    public bool IsVisited = false;
    public bool IsStart = false;

    public bool WallNorth = true;
    public bool WallSouth = true;
    public bool WallEast = true;
    public bool WallWest = true;

    public bool IsEndPoint = false;

    public int row;
    public int column;

    public Cell(int row, int column)
    {
        this.row = row;
        this.column = column;
    }

    public TunnelType GetTunnelType()
    {
        int wallCount = 0;
        if(WallNorth)
        {
            wallCount++;
        }
        if(WallSouth)
        {
            wallCount++;
        }
        if (WallEast)
        {
            wallCount++;
        }
        if (WallWest)
        {
            wallCount++;
        }

        if (wallCount == 1)
        {
            return TunnelType.Cross;
        }
        else if (wallCount == 2)
        {
            if ((!WallNorth && !WallSouth) || (!WallEast && !WallWest))
            {
                //Walls are parralel
                return TunnelType.Straight;
            }
            else
            {
                return TunnelType.Bend;
            }
        }
        else //wall count = 3
        {
            return TunnelType.End;
        }
    }

    public float GetRotation()
    {
        return 0.0f;
    }
}
