using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserInteract : MonoBehaviour
{
    public Camera camera;

    public LineRenderer Beam;
    public ParticleSystem StartingParticles;
    public ParticleSystem HitParticles;

    public float MaxLength;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //laser.SetPosition(0, transform.position);

        var mousePos = Input.mousePosition;
        var rayMouse = camera.ScreenPointToRay(mousePos);

        RaycastHit hit;
        if(Physics.Raycast(rayMouse.origin, rayMouse.direction, out hit, MaxLength))
        {
            if (hit.collider)
            {
                Beam.SetPosition(1, hit.point - transform.position);
                HitParticles.transform.position = hit.point;
                HitParticles.Play();

                if(hit.collider.tag == "Electrifiable")
                { 
                    hit.collider.gameObject.SendMessageUpwards("Electrify");
                }
            }
        }
        else
        {
            var pos = rayMouse.GetPoint(MaxLength);
            Beam.SetPosition(1, pos);
            HitParticles.Stop();
        }

       // StartingParticles.transform.position = laser.GetPosition(0);

        StartingParticles.transform.LookAt(Beam.GetPosition(1));
    }
}
