using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopstickController : MonoBehaviour
{
    private ChopstickMovement chopstickMovement;
    private LiftFoodScript liftFoodScript;
    private int joystickNumber;

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        chopstickMovement = GetComponentInChildren<ChopstickMovement>();
        liftFoodScript = GetComponentInParent<LiftFoodScript>();
    }

    public void SetController(int joystickNumber)
    {
        this.joystickNumber = joystickNumber;
    }

    private void FixedUpdate()
    {
        if (!liftFoodScript.IsLifting) chopstickMovement.Move(fetchInputs());
    }

    private void Update ()
    {
        if (!liftFoodScript.IsLifting) chopstickMovement.Rotate(fetchInputs());
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
