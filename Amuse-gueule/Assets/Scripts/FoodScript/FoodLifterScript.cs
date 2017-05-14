using System.Collections;
using Boo.Lang;
using UnityEngine;

public class FoodLifterScript : MonoBehaviour
{
    private List<GameObject> grappedFoods;

    private float lastJoy1TriggerValue;
    private bool leftChopTryingToLifting;
    private float lastJoy2TriggerValue;
    private bool rightChopTryingToLifting;

    private float delayMaxTime;
    private bool inDelay;

    public GameObject leftChopstick;
    public GameObject rightChopstick;

    private float initialeYposition;
    private float liftingSpeed;

    private string liftingParamName;

    public bool IsLifting { get; private set; }

    private void Awake()
    {
        InitializeVariables();
    }
	
	private void InitializeVariables()
    {
		grappedFoods = new List<GameObject>();
        lastJoy1TriggerValue = 0;
        leftChopTryingToLifting = false;
        lastJoy2TriggerValue = 0;
        rightChopTryingToLifting = false;
        delayMaxTime = 0.8f;
        inDelay = false;
        IsLifting = false;
        initialeYposition = leftChopstick.transform.position.y;
        liftingSpeed = 0.15f;
        liftingParamName = "Lift";
    }

    public void AddGrappedFood(GameObject grappedFood)
    {
        this.grappedFoods.Add(grappedFood);
    }

    public void RemoveGrappedFood(GameObject grappedFood)
    {
        this.grappedFoods.Remove(grappedFood);
    }

    private void Update()
    {
        Hashtable inputs = FetchInputs();

        float newJoy1TriggerValue = (float)inputs["holdJoy1Input"];
        float newJoy2TriggerValue = (float)inputs["holdJoy2Input"];

        if (grappedFoods.Count > 0 && !IsLifting)
        {
            if (lastJoy1TriggerValue == 0 && newJoy1TriggerValue != 0 && !leftChopTryingToLifting)
            {
                leftChopTryingToLifting = true;
            }

            if (lastJoy2TriggerValue == 0 && newJoy2TriggerValue != 0 && !rightChopTryingToLifting)
            {
                rightChopTryingToLifting = true;
            }

            if (!(leftChopTryingToLifting && rightChopTryingToLifting) && (leftChopTryingToLifting || rightChopTryingToLifting) && !inDelay)
            {
                StartCoroutine(StartDelay());
            }

            if (leftChopTryingToLifting && rightChopTryingToLifting)
            {
                StopTryingToLifting();
                LiftChopstick();
            }
        }

        lastJoy1TriggerValue = newJoy1TriggerValue;
        lastJoy2TriggerValue = newJoy2TriggerValue;

        if (IsLifting)
        {
            foreach (GameObject grappedFood in grappedFoods)
            {
                if (grappedFood == null)
                {
                    grappedFoods.Remove(grappedFood);
                }
                else
                {
                    grappedFood.transform.position += new Vector3(0, liftingSpeed, 0);
                }
            }

            if (grappedFoods.Count == 0)
            {
                leftChopstick.transform.position = new Vector3(leftChopstick.transform.position.x, initialeYposition, leftChopstick.transform.position.z);
                rightChopstick.transform.position = new Vector3(rightChopstick.transform.position.x, initialeYposition, rightChopstick.transform.position.z);
                IsLifting = false;
                return;
            }

            leftChopstick.transform.position += new Vector3(0, liftingSpeed, 0);
            rightChopstick.transform.position += new Vector3(0, liftingSpeed, 0);
        }
    }

    private IEnumerator StartDelay()
    {
        inDelay = true;
        yield return  new WaitForSeconds(delayMaxTime);
        StopTryingToLifting();
    }

    private void StopTryingToLifting()
    {
        leftChopTryingToLifting = false;
        rightChopTryingToLifting = false;
        inDelay = false;
    }

    private void LiftChopstick()
    {
        IsLifting = true;

        foreach (GameObject grappedFood in grappedFoods)
        {
            grappedFood.GetComponent<Rigidbody>().isKinematic = true;

            Animator foodAnimator = grappedFood.GetComponent<Animator>();
            foodAnimator.SetTrigger(liftingParamName);
        }
    }

    private Hashtable FetchInputs()
    {
        Hashtable inputs = new Hashtable();

        inputs.Add("holdJoy1Input", Input.GetAxis("Joy1Hold"));
        inputs.Add("holdJoy2Input", Input.GetAxis("Joy2Hold"));

        return inputs;
    }
}
