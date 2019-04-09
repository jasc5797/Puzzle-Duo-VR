using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollectableManager : MonoBehaviour
{


	public GameObject DisplayLocation;

	private GameObject CurrentCollectable;
    private List<GameObject> CollectableList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(CurrentCollectable != null)
		{
			//Debug.Log(currentCollectable.transform.position.x + ", " + currentCollectable.transform.position.y);
		}
    }

	public void Add(GameObject collectable) {
		Debug.Log("Collectable added");
		CollectableList.Add(collectable);
		ShuffleCollectableOrder();
		SetCurrentCollectable();
	}

	public void Remove(GameObject collectable) {
		Debug.Log("Collectable removed");

        CollectableList.Remove(collectable);
        
		if (CollectableList.Count > 0) {
			SetCurrentCollectable();
		} else {
            StartCoroutine(EndLevel());
		}

	}

    private IEnumerator EndLevel()
    {
        GameObject MazeSpawner = GameObject.Find("Maze Spawner");
        MazeSpawner.GetComponent<MazeSpawner>().StartFireWorks();
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("Main Menu");
    }

	public bool IsCurrentCollectable(GameObject collectable) {
		return CollectableList.Count > 0 && CollectableList[0] == collectable;
	}

	void ShuffleCollectableOrder() {
		Debug.Log("Shuffling Collectables");
		for (int i = 0; i < CollectableList.Count; i++) {
			GameObject temp = CollectableList[i];
			int rand = Random.Range(i, CollectableList.Count);
			CollectableList[i] = CollectableList[rand];
			CollectableList[rand] = temp;
		}
	}

	void SetCurrentCollectable() {
		Debug.Log("Setting current collectable");
		if (CollectableList.Count > 0) {
			Debug.Log("Set");
			Debug.Log(CollectableList[0].name);
			GameObject newCurrentCollectable = Instantiate(CollectableList[0], DisplayLocation.transform.position, DisplayLocation.transform.rotation) as GameObject;
            newCurrentCollectable.transform.localScale *= 2;
            HideFromPlayer(newCurrentCollectable);
			if (CurrentCollectable != null) {
				Destroy(CurrentCollectable);
			}
			CurrentCollectable = newCurrentCollectable;
		}
	}

	void HideFromPlayer(GameObject gameObject) {
        int vrOnlyLayer = LayerMask.NameToLayer("VR Only");
        gameObject.layer = vrOnlyLayer;
		foreach (Transform child in gameObject.transform) {
            GameObject childGameObject = child.gameObject;
            childGameObject.layer = vrOnlyLayer;
            HideFromPlayer(childGameObject);
		}
	}
}