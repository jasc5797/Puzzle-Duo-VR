using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour, Electrifiable
{
    public float MoveSpeed = 4;

    public float MinDistance = 2;
    public float MaxDistance = 15;

    public Rigidbody rigidbody;

    private Transform PlayerTransform;
    private Vector3? PreviousPos;

    private bool AdvancedMovement = false;

    private bool IsElectrified = false;



    public void Electrify()
    {
        IsElectrified = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {


        
        if (!IsElectrified)
        {
            float distance = Vector3.Distance(transform.position, PlayerTransform.position);
            if (distance >= MinDistance && distance <= MaxDistance)
            {
                Vector3 heading = PlayerTransform.position - transform.position;
                Vector3 direction = heading / distance;


                RaycastHit hit;
                Ray ray = new Ray(transform.position, direction);
                //, ~LayerMask.NameToLayer("Trigger")
                int mask = ~(1 << 10);
                if (Physics.Raycast(ray, out hit, MaxDistance, mask) && hit.collider.tag == "Player")
                {
                    Vector3 PlayerPos = PlayerTransform.position;
                    Vector3 targetPos = new Vector3(PlayerPos.x, transform.position.y, PlayerPos.z);

                    transform.LookAt(targetPos);
                    transform.position += transform.forward * MoveSpeed * Time.deltaTime;

                    PreviousPos = targetPos;
                }
                else if (AdvancedMovement && PreviousPos != null)
                {
                    transform.LookAt(PreviousPos.GetValueOrDefault());
                    transform.position += transform.forward * MoveSpeed * Time.deltaTime;

                    if (Vector3.Distance(transform.position, PreviousPos.GetValueOrDefault()) < 0.5)
                    {
                        PreviousPos = null;
                    }
                }

            }
            else if (distance <= MaxDistance)
            {
                Vector3 heading = PlayerTransform.position - transform.position;
                Vector3 direction = heading / distance;

                RaycastHit hit;
                Ray ray = new Ray(transform.position, direction);
                //, ~LayerMask.NameToLayer("Trigger")
                int mask = ~(1 << 10);
                if (Physics.Raycast(ray, out hit, MaxDistance, mask) && hit.collider.tag == "Player")
                {
                    Vector3 PlayerPos = PlayerTransform.position;
                    Vector3 targetPos = new Vector3(PlayerPos.x, transform.position.y, PlayerPos.z);

                    transform.LookAt(targetPos);

                    PreviousPos = targetPos;
                }
            }
        }
        else
        {
            rigidbody.AddForce(transform.forward * 0.5f);
            IsElectrified = false;
        }
    }
}
