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
        LookAtMousePosition();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeaponTest(_camTransform);
        }
        //GetMousePosition();
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

    void FireWeaponTest(Transform camera)
    {
        if (_fireTiming <= Time.time - _fireRate)
        {
            _fireTiming = Time.time;


            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
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


    
    /// <summary>
    /// Third Person Camera Follow Mouse Position
    /// </summary>

    // speed is the rate at which the object will rotate
    public float speed;

    void LookAtMousePosition()
    {
        // Generate a plane that intersects the transform's position with an upwards normal.
        Plane playerPlane = new Plane(Vector3.up, transform.position);

        // Generate a ray from the cursor position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // Determine the point where the cursor ray intersects the plane.
        float hitdist = 0.0f;
        // If the ray is parallel to the plane, Raycast will return false.
        if (playerPlane.Raycast(ray, out hitdist))
        {
            // Get the point along the ray that hits the calculated distance.
            Vector3 targetPoint = ray.GetPoint(hitdist);

            // Determine the target rotation.  This is the rotation if the transform looks at the target point.
            Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

            // Smoothly rotate towards the target point.
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
        }
    }

}
