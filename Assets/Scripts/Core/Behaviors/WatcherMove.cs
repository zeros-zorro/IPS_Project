using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class WatcherMove : AgentBehaviour
{
    public Guard guard;
    public FieldOfView fov;
    enum WatcherState
    {
        IDLE,
        PURSUE,
        SEARCH,
        RETURN
    };

    public enum GuardType
    {
        FOLLOWPATH,
        STAYS
    };

    public float speed = 10.0f;
    public float angularThreshold = 10.0f;
    public float rotationSpeed = 10.0f;
    public Transform target;
    [SerializeField]
    private WatcherState watcherState;

    public float minimumAngularFactor = 0.001f;
    public Transform idlePoint;
    private Vector3 targetVector;

    private NavMeshPath navMeshPath;
    private float pathElapsed = 0.0f;

    public GuardType guardType;

    private void Start()
    {
        navMeshPath = new NavMeshPath();
        watcherState = WatcherState.SEARCH;
        //targetVector = Vector3.zero;
    }

    private void Update()
    {
        isInFieldOfView();
        if(guardType == GuardType.FOLLOWPATH)
        {
            if(watcherState == WatcherState.SEARCH)
            {
                target = guard.targetWaypoint;
            }
            else if(watcherState == WatcherState.RETURN)
            {
                returnPath();
            }
            else if(watcherState == WatcherState.PURSUE)
            {
                target = fov.visibleTargets[0];
            }
            else
            {
                watcherState = WatcherState.SEARCH;
            }
        }

        if(guardType == GuardType.STAYS)
        {
            if (watcherState == WatcherState.SEARCH)
            {
                target = null;
            }
            else if (watcherState == WatcherState.RETURN)
            {
                returnPath();
            }
            else if (watcherState == WatcherState.PURSUE)
            {
                target = fov.visibleTargets[0];
            }
            else //STATE IDLE
            {
                
            }
        }
    }
    public override Steering GetSteering()
    {
        Steering steering = new Steering();

        Vector2 position = new Vector2(this.transform.position.x, this.transform.position.z);
        Vector2 forward = new Vector2(this.transform.forward.x, this.transform.forward.z);
        Vector2 targetPosition;

        if(watcherState == WatcherState.RETURN)
        {
            targetPosition = new Vector2(targetVector.x, targetVector.z);
        }
        else
        {
            targetPosition = new Vector2(target.position.x, target.position.z);
        }

        float angleToTarget = -Vector2.SignedAngle(forward, targetPosition - position);
        float angularFactor = angleToTarget / 180f;

        steering.angular = Mathf.Sign(angularFactor) * rotationSpeed;

        if (Mathf.Abs(angleToTarget) < angularThreshold)
        {
            if (Mathf.Abs(angleToTarget) < angularThreshold / 3)
            {
                steering.angular = 0;
            }
            steering.linear = Vector3.forward * speed * agent.maxAccel;
            steering.linear = this.transform.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));
        }
        print(steering.angular);

        Debug.DrawRay(transform.position, -(transform.position - targetVector), Color.black);
        return steering;
    }

    private void isInFieldOfView()
    {
        if (fov.getIsInFOV()) watcherState = WatcherState.PURSUE;
        else
        {
            getNextState();
        }
    }

    private void returnPath()
    {
        idlePoint = guard.targetWaypoint;
        pathElapsed += Time.deltaTime;
        if (pathElapsed > 0.5f)
        {
            pathElapsed -= 0.5f;
            NavMesh.CalculatePath(transform.position, idlePoint.position, NavMesh.AllAreas, navMeshPath);

            int index = 0;
            float distance = Vector3.Distance(transform.position, navMeshPath.corners[index]);
            while (distance < 1f)
            {
                index++;
                distance = Vector3.Distance(transform.position, navMeshPath.corners[index]);
            }
            //print(targetVector);
            targetVector = navMeshPath.corners[index];
        }
        for (int i = 0; i < navMeshPath.corners.Length - 1; i++)
            Debug.DrawLine(navMeshPath.corners[i], navMeshPath.corners[i + 1], Color.red);
    }

    private void getNextState()
    {
        switch (guardType)
        {
            case GuardType.FOLLOWPATH:
                watcherState = WatcherState.RETURN;
                break;
            default:
                watcherState = WatcherState.SEARCH;
                break;
        }
    }
}

