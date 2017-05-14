using UnityEngine;

public class LostFoodScript : MonoBehaviour
{
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == GameManagerScript.FOOD)
        {
            print(other.name + " got out!");
            // Some food feel on the table    
        }
    }
}
