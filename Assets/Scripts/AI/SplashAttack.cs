using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashAttack : MonoBehaviour
{
    public GameObject splashAtk;
    public float explosionRange;
    public float explosionForce;

    public LayerMask whatIsPlayer;
    public float damage;

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
        Health health = other.GetComponent<Health>();
        if (health != null)
        {
            //Instantiate explosion
            if (splashAtk != null) Instantiate(splashAtk, transform.position, Quaternion.identity);

                Collider[] players = Physics.OverlapSphere(transform.position, explosionRange, whatIsPlayer);
                for (int i = 0; i < players.Length; i++)
                {
                    //Get component of enemy and call Take Damage

                    //Just an example!
                    ///enemies[i].GetComponent<ShootingAi>().TakeDamage(explosionDamage);

                    //Add explosion force (if enemy has a rigidbody)
                    if (players[i].GetComponent<Rigidbody>())
                        players[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);
                }

            health.ModifyHealth(damage);
            Destroy(gameObject);
        }
    }
}
