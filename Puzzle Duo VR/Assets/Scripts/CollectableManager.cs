using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
	List<GameObject> collectableList = new List<GameObject>();

	public GameObject displayLocation;
	private GameObject currentCollectable;

	bool ready = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if(currentCollectable != null)
		{
			//Debug.Log(currentCollectable.transform.position.x + ", " + currentCollectable.transform.position.y);
		}
    }

	public void Add(GameObject collectable) {
		Debug.Log("Collectable added");
		collectableList.Add(collectable);
		ShuffleCollectableOrder();
		SetCurrentCollectable();
	}

	public void Remove(GameObject collectable) {
		Debug.Log("Collectable removed");
		collectableList.Remove(collectable);
		if (collectableList.Count > 0) {
			SetCurrentCollectable();
		} else {
			Application.LoadLevel(Application.loadedLevel);
		}

	}

	public bool IsCurrentCollectable(GameObject collectable) {
		return collectableList.Count > 0 && collectableList[0] == collectable;
	}

	void ShuffleCollectableOrder() {
		Debug.Log("Shuffling Collectables");
		for (int i = 0; i < collectableList.Count; i++) {
			GameObject temp = collectableList[i];
			int rand = Random.Range(i, collectableList.Count);
			collectableList[i] = collectableList[rand];
			collectableList[rand] = temp;
		}
	}

	void SetCurrentCollectable() {
		Debug.Log("Setting current collectable");
		if (collectableList.Count > 0) {
			Debug.Log("Set");
			Debug.Log(collectableList[0].name);
			GameObject newCurrentCollectable = Instantiate(collectableList[0], displayLocation.transform.position, displayLocation.transform.rotation) as GameObject;
			newCurrentCollectable.transform.localScale *= 2;
			newCurrentCollectable.transform.GetChild(0).gameObject.layer = displayLocation.layer;
			newCurrentCollectable.layer = displayLocation.layer;
			if (currentCollectable != null) {
				Destroy(currentCollectable);
			}
			currentCollectable = newCurrentCollectable;
		}
	}
}
