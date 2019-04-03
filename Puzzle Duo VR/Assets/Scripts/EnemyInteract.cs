using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInteract : MonoBehaviour, Electrifiable
{
    public bool IsElectrified = false;
    public Rigidbody Rigidbody;
    public EnemyMovement enemyMovement;

    public ParticleSystem stunParticle;

    // Start is called before the first frame update
    void Start()
    {
        stunParticle.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsElectrified && enemyMovement.ElectrifiedTimer <= 0)
        {
            stunParticle.Stop();
        } else
        {
            stunParticle.transform.position = gameObject.transform.position;
            stunParticle.Play();
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
            player.SendMessageUpwards("TakeDamage", gameObject);
            Vector3 direction = transform.position - player.transform.position;
            if (direction.y < 0)
            {
                direction.y = -direction.y;
            }
            Rigidbody.AddForce(direction.normalized * 15.0f);
        }

    }

    public void Electrify()
    {
        IsElectrified = true;
    }
}
