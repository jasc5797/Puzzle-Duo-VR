using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteract : MonoBehaviour, Electrifiable
{
    public bool IsElectrified = false;
    public Rigidbody Rigidbody;
    public EnemyMovement enemyMovement;

    public ParticleSystem stunParticle;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        stunParticle.Stop();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsElectrified && enemyMovement.ElectrifiedTimer <= 0)
        {
            stunParticle.Stop();
        } else
        {
            Vector3 parent_pos = gameObject.transform.position;
            Vector3 stun_pos = stunParticle.transform.position;
            stunParticle.transform.position = new Vector3(parent_pos.x, stun_pos.y, parent_pos.z);
            stunParticle.Play();
            animator.SetTrigger("Stun");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            DamagePlayer(other.gameObject);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            DamagePlayer(other.gameObject);
        }
    }

    void DamagePlayer(GameObject player)
    {
        Debug.Log("Enemy hit");
        if (!IsElectrified && enemyMovement.ElectrifiedTimer <= 0)
        {
            animator.SetTrigger("Attack");
            player.SendMessageUpwards("TakeDamage", gameObject);
            Vector3 direction = transform.position - player.transform.position;
            if (direction.y < 0)
            {
                direction.y = -direction.y;
            }
            //Rigidbody.AddForce(direction.normalized * 15.0f);
        }

    }

    public void Electrify()
    {
        IsElectrified = true;
    }
}
