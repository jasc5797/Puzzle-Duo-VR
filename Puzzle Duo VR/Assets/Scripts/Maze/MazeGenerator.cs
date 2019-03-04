using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Header("Maze Dimensions")]
    public int RowsCount = 1;
    public int ColumnsCount = 1;

    [Header("Random")]
    public bool IsFullRandom = false;
    public int RandomSeed = 0;

    [Header("Start Position")]
    public int StartX = 0;
    public int StartY = 0;

    [Header("Assets")]
    public GameObject TunnelStraight;
    public GameObject TunnelCross;
    public GameObject TunnelEnd;
    public GameObject TunnelBend;

    void Start()
    {
        Maze maze = new Maze(RowsCount, ColumnsCount, StartX, StartY, IsFullRandom, RandomSeed);
        List<Cell> cells = maze.GetCells();
        foreach(Cell cell in cells)
        {
            TunnelType tunnelType = cell.GetTunnelType();
            float rotation = cell.GetRotation();
            GameObject currentTunnel;
            switch(tunnelType)
            {
                case TunnelType.Bend:
                    currentTunnel = TunnelBend;
                    break;
                case TunnelType.End:
                    currentTunnel = TunnelEnd;
                    break;
                case TunnelType.Cross:
                    currentTunnel = TunnelCross;
                    break;
                case TunnelType.Straight:
                    currentTunnel = TunnelStraight;
                    break;
                default:
                    currentTunnel = TunnelEnd;
                    break;
            }
            GameObject temp;
            temp = Instantiate(currentTunnel, new Vector3(cell.row * 2, 0, cell.column * 2), Quaternion.Euler(0, 0, 0)) as GameObject;
            temp.transform.parent = transform;
        }
    }

    void OnValidate()
    {
        RowsCount = Mathf.Clamp(RowsCount, 1, int.MaxValue);
        ColumnsCount = Mathf.Clamp(ColumnsCount, 1, int.MaxValue);
        StartX = Mathf.Clamp(StartX, 0, RowsCount);
        StartY = Mathf.Clamp(StartY, 0, ColumnsCount);
    }
}
