using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMovement : MonoBehaviour
{
    private Rigidbody rigidBody;

    private float moveSpeed;

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        rigidBody = GetComponent<Rigidbody>();
        moveSpeed = 6f;
    }

    public void Move(Hashtable inputs)
    {
        Vector3 moveDirection = new Vector3(moveSpeed * (float)inputs["horizontalInput"], 0, -moveSpeed * (float)inputs["verticalInput"]);
        rigidBody.velocity = moveDirection;
    }
}
