using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableInteract : MonoBehaviour
{
	private GameObject CollectableManagerObject;
	private CollectableManager CollectableManagerScript;
    public ParticleSystem collectedParticleSystem;
    public GameObject collectable;
    public AudioClip OnCollectAudioClip;

    private AudioSource audioSource;

	void Start() 
	{
		CollectableManagerObject = GameObject.Find("Collectable Manager");
		CollectableManagerScript = (CollectableManager)CollectableManagerObject.GetComponent(typeof(CollectableManager));
        audioSource = GetComponent<AudioSource>();
	}

	public void HandlePlayerInteraction () 
	{
		if (CollectableManagerScript.IsCurrentCollectable(gameObject)) {
            collectable.GetComponent<Collectable>().ScaleFade();
            collectedParticleSystem.transform.position = collectable.transform.position;
            CollectableManagerScript.Remove(gameObject);
            audioSource.clip = OnCollectAudioClip;
            audioSource.Play();
            collectedParticleSystem.Play();
		}
	}
}
