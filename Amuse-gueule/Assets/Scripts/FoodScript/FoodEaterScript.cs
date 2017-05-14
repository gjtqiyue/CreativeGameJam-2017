using UnityEngine;

public class FoodEaterScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == GameManagerScript.FOOD)
        {
            Destroy(other.gameObject);
            ScoreManager.Instance.Eat();
            GameManagerScript.Instance.remainingNumOfBugs--;
        }
    }
}
