using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;


public class BeamInteract : MonoBehaviour
{
    public GameObject Model;

    public LineRenderer Beam;
    public ParticleSystem StartingParticles;
    public ParticleSystem HitParticles;

    public float MaxLength = 25;

    public enum Hand { Left, Right };

    public SteamVR_Input_Sources hand;

    [SteamVR_DefaultAction("Squeeze")]
    public SteamVR_Action_Single squeezeAction;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float triggerValue = squeezeAction.GetAxis(hand);
        if (triggerValue > 0) 
        {
            Beam.SetPosition(0, Model.transform.position);
            StartingParticles.transform.position = Model.transform.position;

            Vector3 pos = Model.transform.rotation * Vector3.forward * MaxLength;
            StartingParticles.transform.LookAt(pos);

            RaycastHit hit;
            int mask = ~(1 << 11);
            if (Physics.Raycast(Model.transform.position, Model.transform.rotation * Vector3.forward, out hit, MaxLength, mask))
            {
                if (hit.collider)
                {
                    Beam.SetPosition(1, hit.point - transform.position);
                    HitParticles.transform.position = hit.point;
                    if (HitParticles.isStopped)
                        HitParticles.Play();
                    if (StartingParticles.isStopped)
                        StartingParticles.Play();

                    if (hit.collider.tag == "Electrifiable")
                    {
                        hit.collider.gameObject.SendMessageUpwards("Electrify");
                    }
                }
            }
            else
            {     
                Beam.SetPosition(1, Beam.GetPosition(0));
                HitParticles.Stop();
                if (StartingParticles.isStopped)
                    StartingParticles.Play();
            }
        }
        else
        {
            Beam.SetPosition(1, Beam.GetPosition(0));
            HitParticles.Stop();
            StartingParticles.Stop();
        }
    }
}
