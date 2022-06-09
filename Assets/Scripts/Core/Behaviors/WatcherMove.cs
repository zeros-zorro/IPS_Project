using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatcherMove : AgentBehaviour
{
    public Guard guard;

    enum WatcherState
    {
        IDLE,
        PURSUE,
        SEARCH,
        RETURN
    };
    public float speed = 10.0f;
    public float angularThreshold = 10.0f;
    public float rotationSpeed = 10.0f;
    public Transform target;
    private WatcherState watcherState;

    private void Start()
    {
        watcherState = WatcherState.SEARCH;
    }

    public override Steering GetSteering()
    {
        Steering steering = new Steering();

        Vector2 position = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 forward = new Vector2(this.transform.forward.x, this.transform.forward.z);
        Vector2 targetPosition = new Vector2(target.position.x, target.position.z);

        float angleToTarget = -Vector2.SignedAngle(forward, targetPosition - position);
        float angularFactor = angleToTarget / 180f;
        if(watcherState == WatcherState.PURSUE)
        {

            steering.angular = angularFactor * rotationSpeed;

            if (Mathf.Abs(angleToTarget) < angularThreshold)
            {
                if (agent.GetVelocity() == Vector3.zero)
                {
                    steering.angular = 0;
                }

                steering.linear = Vector3.forward * speed * agent.maxAccel;
                steering.linear = this.transform.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));

            }
        }
        if(watcherState == WatcherState.SEARCH)
        {
            Vector3 direction = guard.targetWaypoint - transform.position;
            if (Vector3.Distance(transform.position, guard.targetWaypoint) < 0.1f)
            {
                steering.linear = Vector3.zero;
                steering.angular = angularFactor * rotationSpeed;
            }
            else
            {
                steering.angular = 0;
                steering.linear = direction * agent.maxAccel;
                steering.linear = this.transform.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxSpeed));
            }
        }


        return steering;
    }
}