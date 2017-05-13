using System.Collections;
using Boo.Lang;
using UnityEngine;

public class LiftFoodScript : MonoBehaviour
{
    private List<GameObject> grappedFoods;
    private bool leftChopTryingToLifting;
    private bool rightChopTryingToLifting;
    private float delayMaxTime;
    private bool inDelay;

    public GameObject leftChopstick;
    public GameObject rightChopstick;

    private float initialeYposition;
    private float liftingSpeed;

    public bool IsLifting { get; private set; }

    private void Awake()
    {
        InitializeVariables();
    }
	
	private void InitializeVariables()
    {
		grappedFoods = new List<GameObject>();
        leftChopTryingToLifting = false;
        rightChopTryingToLifting = false;
        delayMaxTime = 0.8f;
        inDelay = false;
        IsLifting = false;
        initialeYposition = leftChopstick.transform.position.y;
        liftingSpeed = 0.15f;
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
        if (grappedFoods.Count > 0 && !IsLifting)
        {
            Hashtable inputs = FetchInputs();

            if ((bool)inputs["holdJoy1Input"] && !leftChopTryingToLifting)
            {
                leftChopTryingToLifting = true;
            }

            if ((bool)inputs["holdJoy2Input"] && !rightChopTryingToLifting)
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
        //rotationPoint = (leftChopstick.transform.position + rightChopstick.transform.position)/2;

        foreach (GameObject grappedFood in grappedFoods)
        {
            grappedFood.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    private Hashtable FetchInputs()
    {
        Hashtable inputs = new Hashtable();

        inputs.Add("holdJoy1Input", Input.GetButton("Joy1Hold"));
        inputs.Add("holdJoy2Input", Input.GetButton("Joy2Hold"));

        return inputs;
    }
}
