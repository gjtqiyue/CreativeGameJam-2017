using UnityEngine;

public class GrappeFoodScript : MonoBehaviour
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
                LiftFoodScript liftFoodScript = other.GetComponentInParent<LiftFoodScript>();

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
            LiftFoodScript liftFoodScript = other.GetComponentInParent<LiftFoodScript>();

            if (liftFoodScript != null)
            {
                liftFoodScript.RemoveGrappedFood(gameObject);
            }

            chopstickCounter--;
        }
    }
}
