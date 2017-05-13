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
        if (other.transform.tag == GameManagerScript.CHOPSTICK)
        {
            chopstickCounter++;

            if (chopstickCounter == 2)
            {
                FoodLifterScript liftFoodScript = other.GetComponentInParent<FoodLifterScript>();

                if (liftFoodScript != null)
                {
                    liftFoodScript.AddGrappedFood(gameObject);
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == GameManagerScript.CHOPSTICK)
        {
            FoodLifterScript liftFoodScript = other.GetComponentInParent<FoodLifterScript>();

            if (liftFoodScript != null)
            {
                liftFoodScript.RemoveGrappedFood(gameObject);
            }

            chopstickCounter--;
        }
    }
}
