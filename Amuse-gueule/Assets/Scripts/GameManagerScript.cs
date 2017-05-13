using UnityEngine;

public class GameManagerScript : MonoSingleton<GameManagerScript>
{
    public GameObject leftChopstick;
    public GameObject rightChopstick;

    private void Awake ()
    {
        SetChopsticksJoystick();
    }

    private void SetChopsticksJoystick()
    {
        leftChopstick.GetComponent<ChopstickController>().SetController(1);
        rightChopstick.GetComponent<ChopstickController>().SetController(2);
    }
}
