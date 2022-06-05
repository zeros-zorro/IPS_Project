using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatcherMove : AgentBehaviour
{
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


    public override Steering GetSteering()
    {
        Steering steering = new Steering();

        Vector2 position = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 forward = new Vector2(this.transform.forward.x, this.transform.forward.z);
        Vector2 targetPosition = new Vector2(target.position.x, target.position.z);

        float angleToTarget = -Vector2.SignedAngle(forward, targetPosition - position);
        float angularFactor = angleToTarget / 180f;

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
        return steering;
    }

    void SetTarget(Transform transform)
    {
        this.target = transform;
    }
}