using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodEaterScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == GameManagerScript.FOOD)
        {
            Destroy(other.gameObject);
            // Change the score...
        }
    }
}
