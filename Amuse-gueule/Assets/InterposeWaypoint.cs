using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SteeringBasics))]
[RequireComponent(typeof(CollisionAvoidance))]
public class InterposeWaypoint : MonoBehaviour {


    private SteeringBasics steeringBasics;
    private CollisionAvoidance collisionAvoidance;
    private Transform currentTarget;
    private List<Transform> targets = new List<Transform>();
    private List<Rigidbody> WallsRb = new List<Rigidbody>();
    private int currentTargetId = -1;
    private float timeSinceLastUpdate;
    public float minTimeBeforeUpdateNextTarget = 1.5f;
    // Use this for initialization
    void Start ()
    {
        steeringBasics = GetComponent<SteeringBasics>();
        collisionAvoidance = GetComponent<CollisionAvoidance>();

        foreach (var t in GameObject.FindObjectsOfType<TargetWaypoint>())
        {
            targets.Add(t.transform);
        }

        foreach (var w in GameObject.FindGameObjectsWithTag("Wall"))
        {
            WallsRb.Add(w.GetComponent<Rigidbody>());
        }

        UpdateCurrentTarget();
    }

    void UpdateCurrentTarget()
    {
        int nextTargerId;
        do
        {
            nextTargerId = Mathf.FloorToInt(Random.Range(0, targets.Count));
        } while (currentTargetId == nextTargerId);

        currentTargetId = nextTargerId;
        currentTarget = targets[nextTargerId];
    }

    // Update is called once per frame
	void FixedUpdate () {

        Vector3 accel = steeringBasics.seek(currentTarget.position) - 0.2f * collisionAvoidance.getSteering(WallsRb);


        steeringBasics.steer(accel);
        steeringBasics.lookWhereYoureGoing();

        timeSinceLastUpdate += Time.deltaTime;
        if (timeSinceLastUpdate >= minTimeBeforeUpdateNextTarget)
	    {
            UpdateCurrentTarget();
	        timeSinceLastUpdate = 0;
	    }
	}
    /// <summary>
    /// Draws the current target as a yello cube  {gizmos}
    /// </summary>
    void OnDrawGizmos()
    {
        if (currentTarget)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawCube(currentTarget.position, Vector3.one);
        }
    }
}
