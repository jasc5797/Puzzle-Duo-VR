using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [Header("Maze Dimensions")]
    public int rows = 1;
    public int columns = 1;

    void Start()
    {
        Maze maze = new Maze(rows, columns);
        //maze.Generate();
    }

    void OnValidate()
    {
        rows = Mathf.Clamp(rows, 1, int.MaxValue);
        columns = Mathf.Clamp(columns, 1, int.MaxValue);
    }
}
