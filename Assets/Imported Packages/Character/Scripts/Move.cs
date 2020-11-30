using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    private CharacterController _characterController;

    public float Speed = 5.0f;

    public float RotationSpeed = 100.0f;

    public float Gravity = 800.0f;
    public float jump_Force = 1000.0f;
    private float vertical_Velocity; 


    private Vector3 _moveDir = Vector3.zero;

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Sprint
        Sprint();

        // Get Input for axis
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Calculate the forward vector
        Vector3 camForward_Dir = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 move = v * camForward_Dir + h * Camera.main.transform.right;

        if (move.magnitude > 1f) move.Normalize();

        // Calculate the rotation for the player
        move = transform.InverseTransformDirection(move);

        // Get Euler angles
        float turnAmount = Mathf.Atan2(move.x, move.z);

        transform.Rotate(0, turnAmount * RotationSpeed * Time.deltaTime, 0);

        _moveDir = transform.forward * move.magnitude;

        _moveDir *= Speed;

        _moveDir.y -= Gravity * Time.deltaTime;

        ApplyGravity();

        _characterController.Move(_moveDir * Time.deltaTime);
    }

    void ApplyGravity()
    {
        vertical_Velocity -= Gravity * Time.deltaTime;

        PlayerJump();

        _moveDir.y = vertical_Velocity * Time.deltaTime;
    }

    void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("jump");
            vertical_Velocity = jump_Force;
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            print("not jump");
            vertical_Velocity = 0.0f;
        }
    }

    void Sprint() {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            Speed = 24.0f;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Speed = 5.0f;
        }
    }
}
