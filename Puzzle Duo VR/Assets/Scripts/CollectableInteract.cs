using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
	private GameObject CollectableManagerObject;
	private CollectableManager CollectableManagerScript;

	void Start() 
	{
		CollectableManagerObject = GameObject.Find("Collectable Manager");
		CollectableManagerScript = (CollectableManager)CollectableManagerObject.GetComponent(typeof(CollectableManager));
	}

	void OnTriggerEnter (Collider other) 
	{
		Debug.Log("Object entered the trigger");
		if (other.tag == "Player" && CollectableManagerScript.IsCurrentCollectable(gameObject)) {
			gameObject.SetActive(false);
			CollectableManagerScript.Remove(gameObject);
		}
	}
}
