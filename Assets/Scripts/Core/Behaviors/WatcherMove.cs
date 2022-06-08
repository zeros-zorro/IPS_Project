using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WatcherMove : AgentBehaviour
{
  
    public float speed = 10.0f;
    public float angularThreshold = 10.0f;
    public float rotationSpeed = 10.0f;
    public Transform target;
    private GameManager game;
    private GuardBehavior guard;

    public void Start()
    {
        game = this.GetComponentInParent<GameManager>();
        guard = gameObject.GetComponent<GuardBehavior>();
    }

    public override Steering GetSteering()
    {
        Steering steering = new Steering();

        if (game.GetGameRunningStatus())
        {
            switch (guard.GetGuardState())
            {
                case GuardBehavior.WatcherState.IDLE:
                    break;

                case GuardBehavior.WatcherState.PURSUE:
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
                    break;

                case GuardBehavior.WatcherState.RETURN:
                    break;

                case GuardBehavior.WatcherState.SEARCH:
                    steering.angular += 20;
                    break;

                default:
                    break;
            }
            
        }

        return steering;
    }

}
