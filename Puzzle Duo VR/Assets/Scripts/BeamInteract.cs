using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamInteract : MonoBehaviour
{
    //public Camera cam;

    public GameObject Model;

    public LineRenderer Beam;
    public ParticleSystem StartingParticles;
    public ParticleSystem HitParticles;

    public float MaxLength = 25;

    //public bool isLeftController = true;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Beam.SetPosition(0, Model.transform.position);
        StartingParticles.transform.position = Model.transform.position;

       // var mousePos = Input.mousePosition;
       // var rayMouse = cam.ScreenPointToRay(mousePos);
        
        RaycastHit hit;
        if (Physics.Raycast(Model.transform.position, Model.transform.rotation * Vector3.forward, out hit, MaxLength))
        {
            if (hit.collider)
            {
                Beam.SetPosition(1, hit.point - transform.position);
                HitParticles.transform.position = hit.point;
                if (HitParticles.isStopped)
                    HitParticles.Play();
                if (StartingParticles.isStopped)
                    StartingParticles.Play();

                if(hit.collider.tag == "Electrifiable")
                { 
                    hit.collider.gameObject.SendMessageUpwards("Electrify");
                }
            }
        }
        else
        {
            var pos = Model.transform.rotation * Vector3.forward * MaxLength;
            // var pos = rayMouse.GetPoint(MaxLength);
           // Beam.SetPosition(1, pos);
           Beam.SetPosition(1, Beam.GetPosition(0));
           HitParticles.Stop();
           // StartingParticles.Stop();
        }

       // StartingParticles.transform.position = laser.GetPosition(0);

        StartingParticles.transform.LookAt(Beam.GetPosition(1));
    }
}
