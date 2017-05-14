using UnityEngine;

public class FoodScript : MonoBehaviour
{
    private int chopstickCounter;

    private void Awake()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        chopstickCounter = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        FoodLifterScript liftFoodScript = other.GetComponentInParent<FoodLifterScript>();

        if (liftFoodScript != null && !liftFoodScript.IsLifting && other.transform.tag == GameManagerScript.CHOPSTICK)
        {
            chopstickCounter++;

            if (chopstickCounter == 2)
            {
                liftFoodScript.AddGrappedFood(gameObject);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        FoodLifterScript liftFoodScript = other.GetComponentInParent<FoodLifterScript>();

        if (liftFoodScript != null && !liftFoodScript.IsLifting && other.transform.tag == GameManagerScript.CHOPSTICK)
        {
            if (chopstickCounter == 2)
            {
                liftFoodScript.RemoveGrappedFood(gameObject);
            }

            chopstickCounter--;
        }
    }
}
