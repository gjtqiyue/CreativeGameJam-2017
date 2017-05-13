using System.Collections;
using UnityEngine;

public class LiftFoodScript : MonoBehaviour
{
    private GameObject grappedFood;
    private bool leftChopstickLiftingFood;
    private bool rightChopstickLiftingFood;
    private bool inDelay;

    private void Awake ()
    {
        InitializeVariables();
    }

    private void InitializeVariables()
    {
        grappedFood = null;
        leftChopstickLiftingFood = false;
        rightChopstickLiftingFood = false;
        inDelay = false;
    }

    public void NotifyGrappedFood(GameObject grappedFood)
    {
        this.grappedFood = grappedFood;
        //if (grappedFood != null) FrameDelay = MaxFrameDelay;
    }

    private void Update()
    {
        // Si les joueurs tiennent de la nourriture
        if (grappedFood != null)
        {
            Hashtable inputs = FetchInputs();

            // Si un des joueurs tente de soulever la nourriture
            if ((bool)inputs["holdJoy1Input"] && !leftChopstickLiftingFood)
            {
                leftChopstickLiftingFood = true;
            }

            if ((bool)inputs["holdJoy2Input"] && !rightChopstickLiftingFood)
            {
                rightChopstickLiftingFood = true;
            }

            // Si un seul joueur a pesé sur le bouton et aucun délay n'a commencé
            if (!(leftChopstickLiftingFood && rightChopstickLiftingFood) && (leftChopstickLiftingFood || rightChopstickLiftingFood) && !inDelay)
            {
                StartCoroutine(StartDelay());
            }

            if (leftChopstickLiftingFood && rightChopstickLiftingFood)
            {
                print("Grapped in time");
            }
        }
    }

    private IEnumerator StartDelay()
    {
        inDelay = true;
        yield return new WaitForSeconds(1);
        leftChopstickLiftingFood = false;
        rightChopstickLiftingFood = false;
        inDelay = false;
        print("to late");
    }

    Hashtable FetchInputs()
    {
        Hashtable inputs = new Hashtable();

        inputs.Add("holdJoy1Input", Input.GetButton("Joy1Hold"));
        inputs.Add("holdJoy2Input", Input.GetButton("Joy2Hold"));

        return inputs;
    }
}
