using System.Collections;
using UnityEngine;

public class ChopstickMovement : MonoBehaviour
{
    //private Rigidbody rigidBody;

    private float maxRotation;
    //private float moveSpeed;

    private void Awake ()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        //rigidBody = GetComponent<Rigidbody>();
        maxRotation = 40f;
        //moveSpeed = 6f;
    }

    /*public void MoveChopstick(Hashtable inputs)
    {
        Vector3 moveDirection = new Vector3(moveSpeed * (float)inputs["horizontalInput"], 0, -moveSpeed * (float)inputs["verticalInput"]);
        rigidBody.velocity = moveDirection;
    }*/

    public void Rotate(Hashtable inputs)
    {
        Vector3 direction = -new Vector3((float)inputs["rightStickYInput"], 0, (float)inputs["rightStickXInput"]);
        transform.rotation = Quaternion.AngleAxis(maxRotation * direction.magnitude, direction);
    }
}
