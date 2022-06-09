using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : AgentBehaviour
{

    public float speed = 5f;
    public float waitTime = 3f;

    public Transform pathHolder;

    public Vector3 targetWaypoint;
    private Vector3 nextTargetWaypoint;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3[] waypoints = new Vector3[pathHolder.childCount];
        for(int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);
        }

        StartCoroutine(FollowPath(waypoints));
        
    }

    IEnumerator FollowPath(Vector3[] waypoints)
    {
        transform.position = waypoints[0];
        //transform.forward = 
        int targetWaypointIndex = 1;
        this.targetWaypoint = waypoints[targetWaypointIndex];
        this.targetWaypoint = waypoints[targetWaypointIndex + 1];

        yield return new WaitForSeconds(waitTime);
        while (true)
        {
            //transform.position = Vector3.MoveTowards(transform.position, targetWaypoint, speed * Time.deltaTime);
            if(isSpeedNull() && isFacingTarget())
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                nextTargetWaypoint = waypoints[(targetWaypointIndex + 1) % waypoints.Length];
                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach(Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);
    }
    /*
    public override Steering GetSteering()
    {
        Steering steering = new Steering();

        //TODO: if statement of if the game is running
        Vector3 direction = targetWaypoint - transform.position;
        if(Vector3.Distance(transform.position, targetWaypoint) < 0.05f)
        {
            steering.linear = Vector3.zero;
        }
        else
        {
            steering.linear = direction * agent.maxAccel;
            steering.linear = this.transform.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));
        }

        return steering;
    }
    */

    private bool isSpeedNull()
    {
        return (rb.velocity.magnitude < Vector3.kEpsilon);
    }
    private bool isFacingTarget()
    {
        float angleToTarget = -Vector2.SignedAngle(this.transform.forward, nextTargetWaypoint - this.transform.position);
        float angularFactor = angleToTarget / 180f;
        return (Mathf.Abs(angleToTarget) < 10.0f);
    }
}
