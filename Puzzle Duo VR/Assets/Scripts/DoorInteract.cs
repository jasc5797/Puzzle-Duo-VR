using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteract : MonoBehaviour, Electrifiable
{
    public GameObject SlidingDoor = null;
    public GameObject GlowingRing = null;
    public float Speed = 4f;


    private bool IsPlayerInteract = false;
    private bool IsElectrified = false;

    private Vector3 StartPos;
    private Vector3 EndPos;

    private bool IsOpenning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartPos = SlidingDoor.transform.position;
        EndPos = StartPos - new Vector3(0, 4, 0);
    }

    // Update is called once per frame
    void Update()
    {
        Renderer renderer = GlowingRing.GetComponent<Renderer>();
        Material material = renderer.material;
        float emission;
        // Mathf.PingPong(Time.time, 1.0f);


        Vector3 currentPos = SlidingDoor.transform.position;
        if (IsElectrified || (IsOpenning && IsPlayerInteract))
        {
            SlidingDoor.transform.position = Vector3.MoveTowards(currentPos, EndPos, Time.deltaTime * Speed);
            //-= new Vector3(0, speed, 0);
            emission = 5.0f;
            IsElectrified = false;
            IsOpenning = true;
        } else
        {
            SlidingDoor.transform.position = Vector3.MoveTowards(currentPos, StartPos, Time.deltaTime * Speed);
            emission = 0.0f;
            if (SlidingDoor.transform.position == StartPos)
            {
                IsOpenning = false;
            }
        }

        Color finalColor = material.color * Mathf.LinearToGammaSpace(emission);
        material.SetColor("_EmissionColor", finalColor);
    }

    void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Player")
        {
            IsPlayerInteract = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
            IsPlayerInteract = false;
    }

    public void Electrify()
    {
        IsElectrified = true;
    }
}
