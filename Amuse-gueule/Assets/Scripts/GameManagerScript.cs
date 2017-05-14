using UnityEngine;

public class GameManagerScript : MonoSingleton<GameManagerScript>
{
    public GameObject leftChopstick;
    public GameObject rightChopstick;

    public const string CHOPSTICK = "Chopstick";
    public const string FOOD = "Food";

    private void Awake ()
    {
        SetChopsticksJoystick();
        FoodSpawnerScript.Instance.GetComponent<FoodSpawnerScript>().SpawnFood(10);
    }

    private void SetChopsticksJoystick()
    {
        leftChopstick.GetComponent<ChopstickControllerScript>().SetController(1);
        rightChopstick.GetComponent<ChopstickControllerScript>().SetController(2);
    }
}
