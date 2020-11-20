using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(Collider))]
public class Health : MonoBehaviour
{
    public float health = 100f;
    private float _minHealth, _maxHealth;
    //private Invulnerability _inv;
    public GameObject[] healthAware;

    private void Awake()
    {
        //_inv = GetComponent<Invulnerability>();
    }

    void Start()
    {
        _minHealth = 0f;
        _maxHealth = health;
        InitializeHealth(_maxHealth);
    }

    private void InitializeHealth(float maxHealth)
    {
        foreach (GameObject go in healthAware)
        {
            go.SendMessage(nameof(InitializeHealth), maxHealth, SendMessageOptions.DontRequireReceiver);
        }
    }

    private void SetHealth(float newHealth)
    {
        foreach (GameObject go in healthAware)
        {
            go.SendMessage(nameof(SetHealth), newHealth, SendMessageOptions.DontRequireReceiver);
        }
    }

    public void ModifyHealth(float delta)
    {
        //bool isInvulnerable = _inv == null ? false : _inv.isInvulnerable;
        bool isInvulnerable = false;

        if (!isInvulnerable)
        {
            health = Mathf.Clamp(health += delta, _minHealth, _maxHealth);
            if (health <= 0) Die();
            else if (health < _maxHealth) SetHealth(health);
        }
    }

    private void Die()
    {
        Time.timeScale = 0f;
    }
}