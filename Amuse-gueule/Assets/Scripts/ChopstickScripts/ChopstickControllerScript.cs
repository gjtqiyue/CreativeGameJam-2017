using System.Collections;
using UnityEngine;

public class ChopstickControllerScript : MonoBehaviour
{
    private ChopstickMovementScript chopstickMovement;
    private FoodLifterScript foodLifterScript;
    private int joystickNumber;

    public bool IsActif { get; private set; }

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        chopstickMovement = GetComponentInChildren<ChopstickMovementScript>();
        foodLifterScript = GetComponentInParent<FoodLifterScript>();
    }

    public void SetController(int joystickNumber)
    {
        this.joystickNumber = joystickNumber;
    }

    public void EnableControls(bool enable)
    {
        IsActif = enable;
    }

    private void FixedUpdate()
    {
        if (IsActif && !foodLifterScript.IsLifting) chopstickMovement.Move(fetchInputs());
    }

    private void Update ()
    {
        if (IsActif && !foodLifterScript.IsLifting) chopstickMovement.Rotate(fetchInputs());
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
