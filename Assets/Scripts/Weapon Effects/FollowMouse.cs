using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouse : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        //CrossHairFollowMouse();

        Vector3 position = Input.mousePosition;
        position.z = -Camera.main.transform.position.z;
        //position = Camera.main.ScreenToWorldPoint(position);

        transform.position = position;
    }

}
