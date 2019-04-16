using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float MoveSpeed = 4;

    public float MinDistance = 2;
    public float MaxDistance = 15;

    public Rigidbody Rigidbody;

    public EnemyInteract EnemyInteractScript;

    public float TimeElectrified = 5.0f;

    private Transform PlayerTransform;
    private Vector3? PreviousPos;

    private bool AdvancedMovement = false; //remove before turning in this project


    public float ElectrifiedTimer = 0.0f;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0.1f, transform.position.z);
        if (!EnemyInteractScript.IsElectrified && ElectrifiedTimer > 0)
        {
            ElectrifiedTimer -= Time.deltaTime;
        } else if (EnemyInteractScript.IsElectrified)
        {
            ElectrifiedTimer = TimeElectrified;
        }

        if(ElectrifiedTimer <= 0)
        {
            ElectrifiedTimer = 0;
        }

        if (!EnemyInteractScript.IsElectrified && ElectrifiedTimer <= 0)
        {
            Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
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
                    animator.SetTrigger("Walk");
                    Vector3 PlayerPos = PlayerTransform.position;
                    Vector3 targetPos = new Vector3(PlayerPos.x, transform.position.y, PlayerPos.z);

                    transform.LookAt(targetPos);
                    transform.position += transform.forward * MoveSpeed * Time.deltaTime;
                    transform.position = new Vector3(transform.position.x, 0, transform.position.z);

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
            else if (distance > MaxDistance)
            {
                animator.SetTrigger("Idle");
            }
            if (distance <= MinDistance)
            {
                //animator.SetTrigger("Attack");
            }
        }
        else
        {
           // Rigidbody.AddForce(transform.forward * 0.5f);
            EnemyInteractScript.IsElectrified = false;
            Rigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
        }
    }
}
