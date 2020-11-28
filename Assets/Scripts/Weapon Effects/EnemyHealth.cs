using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] float maxHealth;
    [SerializeField] ParticleSystem dieVfx;
    private float _currentHealth;
    [SerializeField] private float explosionForce = 150f, explosionRadius = 15f, upwardsModifier = 50f;

    public float MaxHealth
    {
        get
        {
            return maxHealth;
        }
    }

    private void Start()
    {
        _currentHealth = maxHealth;
    }

    public void ModifyHealth(float amount)
    {
        _currentHealth -= amount;
        if (_currentHealth <= 0) Die();
    }

    public void Die()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            if (rb != null) rb.AddExplosionForce(explosionForce, explosionPos, explosionRadius, upwardsModifier);
        }

        Instantiate(dieVfx, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
