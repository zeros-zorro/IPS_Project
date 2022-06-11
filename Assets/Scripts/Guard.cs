using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Guard : AgentBehaviour
{
    public float speed = 5f;
    public float waitTime = 3f;
    public Transform pathHolder;
    public Transform targetWaypoint;
    private void Start()
    {
        //Vector3[] waypoints = new Vector3[pathHolder.childCount];
        Transform[] waypoints = new Transform[pathHolder.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            //waypoints[i] = pathHolder.GetChild(i).position;
            waypoints[i] = pathHolder.GetChild(i);
            //waypoints[i] = new Vector3(waypoints[i].x, transform.position.y, waypoints[i].z);

        }
        StartCoroutine(FollowPath(waypoints));

    }
    IEnumerator FollowPath(Transform[] waypoints)
    {
        //transform.position = waypoints[0];
        transform.position = waypoints[0].position;
        int targetWaypointIndex = 1;
        //this.targetWaypoint = waypoints[targetWaypointIndex];
        this.targetWaypoint = waypoints[targetWaypointIndex];
        while (true)
        {
            if (Vector3.Distance(transform.position, targetWaypoint.position) < 0.05f)
            {
                Debug.Log("Changed target");
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoints.Length;
                targetWaypoint = waypoints[targetWaypointIndex];
                yield return new WaitForSeconds(waitTime);
            }
            yield return null;
}
    }
    private void OnDrawGizmos()
    {
        Vector3 startPosition = pathHolder.GetChild(0).position;
        Vector3 previousPosition = startPosition;
        foreach (Transform waypoint in pathHolder)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(previousPosition, waypoint.position);
            previousPosition = waypoint.position;
        }
        Gizmos.DrawLine(previousPosition, startPosition);
    }

}

