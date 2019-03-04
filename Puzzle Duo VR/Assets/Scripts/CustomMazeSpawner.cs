using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomMazeSpawner : MonoBehaviour
{
    public enum MazeGenerationAlgorithm
    {
        PureRecursive,
        RecursiveTree,
        RandomTree,
        OldestTree,
        RecursiveDivision,
    }

    [Header("Maze Generation")]
    public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;

    [Header("Maze Dimensions")]
    public int Rows = 1;
    public int Columns = 1;

    [Header("Random")]
    public bool IsFullRandom = false;
    public int RandomSeed = 0;

    [Header("Start Position")]
    public int StartX = 0;
    public int StartY = 0;

    [Header("Tunnel Assets")]
    public GameObject TunnelStraight;
    public GameObject TunnelCross;
    public GameObject TunnelEnd;
    public GameObject TunnelBend;

    [Header("Tunnel Dimension")]
    public float TunnelWidth = 2;
    public float TunnelHeight = 2;

    // Start is called before the first frame update
    void Start()
    {
        if (IsFullRandom)
        {
            Random.InitState(System.DateTime.Now.Millisecond);
        }
        else
        {
            Random.InitState(RandomSeed);
        }

        BasicMazeGenerator mazeGenerator = GetMazeGenerator();
        mazeGenerator.GenerateMaze();
        ParseMaze(mazeGenerator);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private BasicMazeGenerator GetMazeGenerator()
    {
        switch (Algorithm)
        {
            default:
            case MazeGenerationAlgorithm.PureRecursive:
                return new RecursiveMazeGenerator(Rows, Columns);
            case MazeGenerationAlgorithm.RecursiveTree:
                return new RecursiveTreeMazeGenerator(Rows, Columns);
            case MazeGenerationAlgorithm.RandomTree:
                return new RandomTreeMazeGenerator(Rows, Columns);
            case MazeGenerationAlgorithm.OldestTree:
                return new OldestTreeMazeGenerator(Rows, Columns);
            case MazeGenerationAlgorithm.RecursiveDivision:
                return new DivisionMazeGenerator(Rows, Columns);
        }
    }

    private void ParseMaze(BasicMazeGenerator mazeGenerator)
    {
        for (int row = 0; row < Rows; row++)
        {
            for (int column = 0; column < Columns; column++)
            {
                float x = row * TunnelWidth;
                float z = column * TunnelHeight;

                MazeCell mazeCell = mazeGenerator.GetMazeCell(row, column);
                GameObject tunnel = InstantiateTunnel(mazeCell, x, z);
                tunnel.transform.parent = transform;
            }
        }
    }

    private GameObject InstantiateTunnel(MazeCell mazeCell, float x, float z)
    {
        int wallCount = 0;
        if (mazeCell.WallBack)
        {
            wallCount++;
        }
        if (mazeCell.WallFront)
        {
            wallCount++;
        }
        if (mazeCell.WallLeft)
        {
            wallCount++;
        }
        if (mazeCell.WallRight)
        {
            wallCount++;
        }
        GameObject tunnelAsset;
        switch(wallCount)
        {
            case 1:
                tunnelAsset = TunnelCross;
                break;
            case 2:
                if ((!mazeCell.WallBack && !mazeCell.WallFront) || (!mazeCell.WallLeft && !mazeCell.WallRight))
                {
                    tunnelAsset = TunnelStraight;
                }
                else
                {
                    tunnelAsset = TunnelBend;
                }
                break;
            case 3:
            default:
                tunnelAsset = TunnelEnd;
                break;
        }
        Vector3 position = new Vector3(x, 0, z);
        Quaternion rotation = Quaternion.Euler(0, 0, 0);
        return Instantiate(tunnelAsset, position, rotation) as GameObject;
    }

    void OnValidate()
    {
        Rows = Mathf.Clamp(Rows, 1, int.MaxValue);
        Columns = Mathf.Clamp(Columns, 1, int.MaxValue);
        StartX = Mathf.Clamp(StartX, 0, Rows);
        StartY = Mathf.Clamp(StartY, 0, Columns);
    }
}
