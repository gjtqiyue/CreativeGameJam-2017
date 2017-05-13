using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour
{
    private ArmMovement armMovement;
    private ChopstickMovement chopstickMovement;
    private int joystickNumber;

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        armMovement = GetComponentInChildren<ArmMovement>();
        chopstickMovement = GetComponentInChildren<ChopstickMovement>();
    }

    public void SetController(int joystickNumber)
    {
        this.joystickNumber = joystickNumber;
    }

    private void FixedUpdate()
    {
        armMovement.Move(fetchInputs());
    }

    private void Update ()
    {
        chopstickMovement.Rotate(fetchInputs());
    }

    Hashtable fetchInputs()
    {
        Hashtable inputs = new Hashtable();

        inputs.Add("horizontalInput", Input.GetAxis("Joy" + joystickNumber + "Horizontal"));
        inputs.Add("verticalInput", Input.GetAxis("Joy" + joystickNumber + "Vertical"));
        inputs.Add("rightStickYInput", Input.GetAxis("Joy" + joystickNumber + "RightStickY"));
        inputs.Add("rightStickXInput", Input.GetAxis("Joy" + joystickNumber + "RightStickX"));

        return inputs;
    }
}
