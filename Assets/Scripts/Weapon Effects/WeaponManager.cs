using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private Transform _camTransform;

    [SerializeField] GameObject decal;
    [SerializeField] float _impactForce;
    [SerializeField] int _damageAmount;
    [SerializeField] float _fireRate;
    private float _fireTiming;

    private void Awake()
    {
        _camTransform = Camera.main.transform;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            FireWeapon(_camTransform);
        }
    }

    void FireWeapon(Transform camera)
    {
        if (_fireTiming <= Time.time - _fireRate)
        {
            // PlayFireVfx();
            /*ChaScript chaScript = GetComponent<ChaScript>();
            chaScript.FireWeapon();*/

            _fireTiming = Time.time;

            if (Physics.Raycast(camera.position, camera.forward,
                    out RaycastHit hit))
            {

                GameObject mark = Instantiate(decal);
                mark.transform.position = hit.point;
                mark.transform.LookAt(hit.point + hit.normal);
                ApplyForce(hit);
                ModifyHealth(hit);
            }

        }
    }// Fire weapon

    public virtual void ApplyForce(RaycastHit hit)
    {
        hit.rigidbody?.AddForce(-hit.normal * _impactForce);
    }// Apply force

    public virtual void ModifyHealth(RaycastHit hit)
    {
        GameObject target = hit.transform.gameObject;
        if (target.GetComponent<IDamageable>() != null)
        {
            target.SendMessage(nameof(ModifyHealth), _damageAmount, SendMessageOptions.DontRequireReceiver);
        }
    }// Modify health
}
