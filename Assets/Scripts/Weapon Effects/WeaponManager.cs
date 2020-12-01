using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
   public AudioClip[] audioClips;
 
    private Transform _camTransform;

    [SerializeField] GameObject decal;
    [SerializeField] float _impactForce;
    [SerializeField] int _damageAmount;
    [SerializeField] float _fireRate;
    private float _fireTiming;
    private Rigidbody rb;
    public Transform attackPoint;

    private void Awake()
    {
        Initialize();
        _camTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    IEnumerator StartAudio()
    {
        AudioSource audio = GetComponent<AudioSource>();

        audio.Play();
        yield return new WaitForSeconds(0f);
        if (audioClips.Length != 0) {
            audio.clip = audioClips[Random.Range(0, audioClips.Length)];
            audio.volume = Random.Range(0f, 1f);
            audio.Play();
        }
    }
    void Update()
    {
        LookAtMousePosition();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            FireWeaponTest(_camTransform);
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

    void FireWeaponTest(Transform camera)
    {
        if (_fireTiming <= Time.time - _fireRate)
        {
            _fireTiming = Time.time;


            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity))
            {
                Rigidbody rb = Instantiate(decal, attackPoint.position, Quaternion.identity).GetComponent<Rigidbody>();
                //rb.AddForce(transform.InverseTransformPoint(hit.point) * 10f, ForceMode.Impulse); // test
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 3, ForceMode.Impulse);
                StartCoroutine(StartAudio());                
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

    public void DashInDirection(Vector3 dir, float force)
    {
        rb.AddForce(dir * force, ForceMode.Impulse);
    }

    /// <summary>
    /// Third Person Camera Follow Mouse Position
    /// </summary>

    // speed is the rate at which the object will rotate
    public float speed;

    void LookAtMousePosition()
    {   
        // check mouse position
        // print("Mouse position: " + Input.mousePosition.y + " " + Input.mousePosition.x +" " + Input.mousePosition.z);
        

            if (Input.mousePosition.y > (ScreenTop * .2) && Input.mousePosition.x > (ScreenRight * 0)
                && Input.mousePosition.y < (ScreenTop * .8) && Input.mousePosition.x < (ScreenRight * 1))
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





    //GET SCREEN COORDINATES

    #region Fields

    // cached for efficient boundary checking
    static float screenLeft;
    static float screenRight;
    static float screenTop;
    static float screenBottom;

    #endregion

    #region Properties

    /// <summary>
    /// Gets the left edge of the screen in world coordinates
    /// </summary>
    /// <value>left edge of the screen</value>
    public static float ScreenLeft
    {
        get { return screenLeft; }
    }

    /// <summary>
    /// Gets the right edge of the screen in world coordinates
    /// </summary>
    /// <value>right edge of the screen</value>
    public static float ScreenRight
    {
        get { return screenRight; }
    }

    /// <summary>
    /// Gets the top edge of the screen in world coordinates
    /// </summary>
    /// <value>top edge of the screen</value>
    public static float ScreenTop
    {
        get { return screenTop; }
    }

    /// <summary>
    /// Gets the bottom edge of the screen in world coordinates
    /// </summary>
    /// <value>bottom edge of the screen</value>
    public static float ScreenBottom
    {
        get { return screenBottom; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initializes the screen utilities
    /// </summary>
    public static void Initialize()
    {
        screenLeft = 0f;
        screenRight = Camera.main.pixelWidth;
        screenTop = Camera.main.pixelHeight;
        screenBottom = 0f;

        // check screen coordinates
        // print("Screen Coord: " + ScreenTop + " " + screenBottom + " " + screenLeft + " " + screenRight);
    }

    #endregion
}
