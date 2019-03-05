using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
	private GameObject collectableManagerObject;
	private CollectableManager collectableManagerScript;

	void Start() 
	{
		collectableManagerObject = GameObject.Find("Collectable Manager");
		collectableManagerScript = (CollectableManager)collectableManagerObject.GetComponent(typeof(CollectableManager));
	}

	void OnTriggerEnter (Collider other) 
	{
		Debug.Log("Object entered the trigger");
		if (other.tag == "Player" && collectableManagerScript.IsCurrentCollectable(gameObject)) {
			gameObject.SetActive(false);
			collectableManagerScript.Remove(gameObject);
		}
	}
}
