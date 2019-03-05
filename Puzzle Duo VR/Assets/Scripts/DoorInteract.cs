using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour
{
    public GameObject slidingDoor = null;
    public GameObject glowyBit = null;
    public float speed = 3f;

    private bool IsOpenning = false;
    private Vector3 startPos;
    private Vector3 endPos;

    // Start is called before the first frame update
    void Start()
    {
        startPos = slidingDoor.transform.position;
        endPos = startPos - new Vector3(0, 4, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Renderer renderer = glowyBit.GetComponent<Renderer>();
        Material material = renderer.material;
        float emission;
        // Mathf.PingPong(Time.time, 1.0f);


        Vector3 currentPos = slidingDoor.transform.position;
        if (IsOpenning)
        {
            slidingDoor.transform.position = Vector3.MoveTowards(currentPos, endPos, Time.deltaTime * speed);
            //-= new Vector3(0, speed, 0);
            emission = 1.0f;
        } else
        {
            slidingDoor.transform.position = Vector3.MoveTowards(currentPos, startPos, Time.deltaTime * speed);
            emission = 0.0f;
        }

        Color finalColor = material.color * Mathf.LinearToGammaSpace(emission);
        material.SetColor("_EmissionColor", finalColor);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
            IsOpenning = true;
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            IsOpenning = false;
    }
}
