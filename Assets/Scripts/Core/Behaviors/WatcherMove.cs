using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatcherMove : AgentBehaviour
{
  
    public float speed = 10.0f;
    public float angularThreshold = 10.0f;
    private float rotationSpeed = 0.3f;
    public Transform target;
    private GameManager game;
    private GuardBehavior guard;
    private bool collisionBehavior = false;
    Steering steering = new Steering();

    public void Start()
    {
        //agent.SetGoalPose(0, 0, 0, 0, 1);
        game = this.GetComponentInParent<GameManager>();
        guard = gameObject.GetComponent<GuardBehavior>();
    }

    public override Steering GetSteering()
    {
        if (game.GetGameRunningStatus() && !collisionBehavior)
        {
            switch (guard.GetGuardState())
            {
                case GuardBehavior.WatcherState.IDLE:
                    steering = new Steering();
                    break;

                case GuardBehavior.WatcherState.PURSUE:
                    Vector2 position = new Vector2(this.transform.position.x, this.transform.position.z);
                    Vector2 forward = new Vector2(this.transform.forward.x, this.transform.forward.z);
                    Vector2 targetPosition = new Vector2(target.position.x, target.position.z);

                    float angleToTarget = -Vector2.SignedAngle(forward, targetPosition - position);
                    float angularFactor = angleToTarget / 180f;

                    steering.angular = Mathf.Sign(angularFactor) * rotationSpeed * agent.maxAngularSpeed;

                    if (Mathf.Abs(angleToTarget) < angularThreshold)
                    {
                        if (Mathf.Abs(angleToTarget) < angularThreshold / 3)
                        {
                            steering.angular = 0;
                        }

                        steering.linear = Vector3.forward * speed * agent.maxAccel;
                        steering.linear = this.transform.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));
                    }
                    break;

                case GuardBehavior.WatcherState.RETURN:
                    break;

                case GuardBehavior.WatcherState.SEARCH:
                    steering.angular += 20;
                    break;

                default:
                    break;
            }

        } else if (!game.GetGameRunningStatus())
        {
            steering = new Steering();
        }

        return steering;
    }

    void OnCollisionEnter(Collision collision)
    {
        agent.ActivateDirectionalHapticFeedback();
        foreach (ContactPoint contact in collision.contacts)
        steering.linear = collision.contacts[0].normal.normalized * agent.maxAccel;
        collisionBehavior = true;
    }

    void OnCollisionExit(Collision collision)
    {
        agent.DeActivateDirectionalHapticFeedback();
        collisionBehavior = false;
    }

}
