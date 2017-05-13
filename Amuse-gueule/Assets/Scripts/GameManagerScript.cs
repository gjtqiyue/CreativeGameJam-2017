using UnityEngine;

public class GameManagerScript : MonoSingleton<GameManagerScript>
{
    public GameObject leftArm;
    public GameObject rightArm;

    public const string CHOPSTICK = "Chopstick";
    public const string CAN_BE_GRAPPED = "CanBeGrapped";

    private void Awake ()
    {
        SetChopsticksJoystick();
    }

    private void SetChopsticksJoystick()
    {
        leftArm.GetComponent<ArmController>().SetController(1);
        rightArm.GetComponent<ArmController>().SetController(2);
    }
}
