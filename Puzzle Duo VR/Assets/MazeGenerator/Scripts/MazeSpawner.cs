using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//<summary>
//Game object, that creates maze and instantiates it in scene
//</summary>
public class MazeSpawner : MonoBehaviour {
	public enum MazeGenerationAlgorithm{
		PureRecursive,
		RecursiveTree,
		RandomTree,
		OldestTree,
		RecursiveDivision,
	}

	public MazeGenerationAlgorithm Algorithm = MazeGenerationAlgorithm.PureRecursive;
	public bool FullRandom = false;
	public int RandomSeed = 12345;
	public GameObject Floor = null;
	public GameObject Wall = null;
	public GameObject Pillar = null;
	public int Rows = 5;
	public int Columns = 5;
	public float CellWidth = 5;
	public float CellHeight = 5;
	public bool AddGaps = true;


	public GameObject CollectablePrefab1 = null;
	public GameObject CollectablePrefab2 = null;
	public GameObject CollectablePrefab3 = null;
	public GameObject CollectablePrefab4 = null;
	public GameObject CollectablePrefab5 = null;
    public int DoorCount = 10;
    public GameObject DoorPrefab = null;
    public int EnemyCount = 5;
    public GameObject EnemyPrefab = null;

	private BasicMazeGenerator mMazeGenerator = null;

	private List<Vector3> PossibleGoalCells = new List<Vector3>();
    private List<DoorPosition> PossibleDoorPositions = new List<DoorPosition>();
    private List<Vector3> PossibleEnemyCells = new List<Vector3>();

	private GameObject CollectableManagerObject;
	private CollectableManager CollectableManagerScript;

	void Start () {

		CollectableManagerObject = GameObject.Find("Collectable Manager");
		CollectableManagerScript = (CollectableManager)CollectableManagerObject.GetComponent(typeof(CollectableManager));


		if (FullRandom)
        {
			Random.InitState(System.DateTime.Now.Millisecond);
		}
        else
        {
            Random.InitState(RandomSeed);
        }

		switch (Algorithm) {
		case MazeGenerationAlgorithm.PureRecursive:
			mMazeGenerator = new RecursiveMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RecursiveTree:
			mMazeGenerator = new RecursiveTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RandomTree:
			mMazeGenerator = new RandomTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.OldestTree:
			mMazeGenerator = new OldestTreeMazeGenerator (Rows, Columns);
			break;
		case MazeGenerationAlgorithm.RecursiveDivision:
			mMazeGenerator = new DivisionMazeGenerator (Rows, Columns);
			break;
		}
		mMazeGenerator.GenerateMaze ();
		for (int row = 0; row < Rows; row++) {
			for(int column = 0; column < Columns; column++){
				float x = column*(CellWidth+(AddGaps?.2f:0));
				float z = row*(CellHeight+(AddGaps?.2f:0));
				MazeCell cell = mMazeGenerator.GetMazeCell(row,column);
				GameObject tmp;
				tmp = Instantiate(Floor,new Vector3(x,0,z), Quaternion.Euler(0,0,0)) as GameObject;
				tmp.transform.parent = transform;

               // tmp = Instantiate(Floor, new Vector3(x, 4, z), Quaternion.Euler(0, 0, 0)) as GameObject;
                //tmp.transform.parent = transform;

                if (cell.WallRight){
					tmp = Instantiate(Wall,new Vector3(x+CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,90,0)) as GameObject;// right
					tmp.transform.parent = transform;
				}
                else
                {
                    int neighborColumn = column + 1;
                    if (neighborColumn < Columns) {
                        MazeCell neighborCell = mMazeGenerator.GetMazeCell(row, neighborColumn);
                        if (!neighborCell.WallLeft)
                        {
                            PossibleDoorPositions.Add(new DoorPosition(new Vector3(x + CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 90, 0)));
                        }
                    }
                }

				if(cell.WallFront){
					tmp = Instantiate(Wall,new Vector3(x,0,z+CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,0,0)) as GameObject;// front
					tmp.transform.parent = transform;
				}
                else
                {
                    int neighborRow = row + 1;
                    if (neighborRow < Rows)
                    {
                        MazeCell neighborCell = mMazeGenerator.GetMazeCell(neighborRow, column);
                        if (!neighborCell.WallBack)
                        {
                            PossibleDoorPositions.Add(new DoorPosition(new Vector3(x, 0, z + CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 0, 0)));
                        }
                    }
                    
                }

				if(cell.WallLeft){
					tmp = Instantiate(Wall,new Vector3(x-CellWidth/2,0,z)+Wall.transform.position,Quaternion.Euler(0,270,0)) as GameObject;// left
					tmp.transform.parent = transform;
				}
                else
                {
                    int neighborColumn = column - 1;
                    if (neighborColumn >= 0)
                    {
                        MazeCell neighborCell = mMazeGenerator.GetMazeCell(row, neighborColumn);
                        if (!neighborCell.WallRight)
                        {
                            PossibleDoorPositions.Add(new DoorPosition(new Vector3(x - CellWidth / 2, 0, z) + Wall.transform.position, Quaternion.Euler(0, 270, 0)));
                        }
                    }
                }

				if(cell.WallBack){
					tmp = Instantiate(Wall,new Vector3(x,0,z-CellHeight/2)+Wall.transform.position,Quaternion.Euler(0,180,0)) as GameObject;// back
					tmp.transform.parent = transform;
				}
                else
                {
                    int neighborRow = row - 1;
                    if (neighborRow >= 0)
                    {
                        MazeCell neighborCell = mMazeGenerator.GetMazeCell(neighborRow, column);
                        if (!neighborCell.WallFront)
                        {
                            PossibleDoorPositions.Add(new DoorPosition(new Vector3(x, 0, z - CellHeight / 2) + Wall.transform.position, Quaternion.Euler(0, 180, 0)));
                        }
                    }
                }

				if(cell.IsGoal){
					PossibleGoalCells.Add(new Vector3(x,1.5f,z));
				}
                else
                {
                    PossibleEnemyCells.Add(new Vector3(x, 1.5f, z));
                }
			}
		}
		if(Pillar != null){
			for (int row = 0; row < Rows+1; row++) {
				for (int column = 0; column < Columns+1; column++) {
					float x = column*(CellWidth+(AddGaps?.2f:0));
					float z = row*(CellHeight+(AddGaps?.2f:0));
					GameObject tmp = Instantiate(Pillar,new Vector3(x-CellWidth/2,0,z-CellHeight/2),Quaternion.identity) as GameObject;
					tmp.transform.parent = transform;
				}
			}
		}

        Random.InitState(System.DateTime.Now.Millisecond);

        for (int GoalCount = 0; GoalCount < 5; GoalCount++) {
			int rand = Random.Range(0, PossibleGoalCells.Count);
			Vector3 vector3 = PossibleGoalCells[rand];

			PossibleGoalCells.RemoveAt(rand);

			//Add Goals
			GameObject GoalPrefab;
			switch (GoalCount) {
			case 0:
				GoalPrefab = CollectablePrefab1;
				break;
			case 1:
				GoalPrefab = CollectablePrefab2;
				break;
			case 2:
				GoalPrefab = CollectablePrefab3;
				break;
			case 3:
				GoalPrefab = CollectablePrefab4;
				break;
			case 4:
				GoalPrefab = CollectablePrefab5;
				break;
			default:
				GoalPrefab = CollectablePrefab1;
				break;
			}

			GameObject tmp = Instantiate(GoalPrefab, vector3, Quaternion.Euler(0,0,0)) as GameObject;
			tmp.transform.parent = transform;

			CollectableManagerScript.Add(tmp);
		}

        for (int i = 0; i < DoorCount; i++)
        {
            int rand = Random.Range(0, PossibleDoorPositions.Count);
            DoorPosition doorPosition = PossibleDoorPositions[rand];

            PossibleDoorPositions.RemoveAt(rand);

            GameObject tmp = Instantiate(DoorPrefab, doorPosition.Position, doorPosition.Rotation) as GameObject;
            tmp.transform.parent = transform;
        }

        for(int i = 0; i < EnemyCount; i++)
        {
            int rand = Random.Range(0, PossibleEnemyCells.Count);
            Vector3 vector3 = PossibleEnemyCells[rand];

            PossibleEnemyCells.RemoveAt(rand);

            GameObject tmp = Instantiate(EnemyPrefab, vector3, Quaternion.Euler(0, 0, 0)) as GameObject;
            tmp.transform.parent = transform;
        }
	}

    private class DoorPosition
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public DoorPosition(Vector3 Rosition, Quaternion Rotation)
        {
            this.Position = Rosition;
            this.Rotation = Rotation;
        }
    }
}
