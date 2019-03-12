using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteract : MonoBehaviour, Electrifiable
{
    public bool IsElectrified = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Enemy hit");
        }
    }

    public void Electrify()
    {
        IsElectrified = true;
    }
}
