using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum GuardState
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

public class GuardBehavior : AgentBehaviour
{

    public GuardPath guardPath = null;

    public float speed = 10.0f;
    public float angularThreshold = 10.0f;
    public float rotationSpeed = 10.0f;

    public float minimumAngularFactor = 0.001f;

    private Transform idlePoint;

    public GuardType guardType;

    [SerializeField]
    private GuardState state;
    private bool collisionBehavior = false;

    private GameManager game;
    private Audio audioGuard;

    private FieldOfView fov;

    private Transform target;

    private Vector3 targetVector;

    private NavMeshPath navMeshPath;
    private float pathElapsed = 0.0f;
    private CelluloAgent cellulo;

    Steering steering = new Steering();

    private void Start()
    {
        navMeshPath = new NavMeshPath();
        state = GuardState.SEARCH;
        if (guardType == GuardType.FOLLOWPATH) {
            targetVector = guardPath.targetWaypoint.position;
        }
        game = this.GetComponentInParent<GameManager>();

        fov = this.GetComponent<FieldOfView>();

        cellulo = gameObject.GetComponent<CelluloAgent>();

        audioGuard = GameObject.FindGameObjectWithTag(GameManager.AUDIO_TAG).GetComponent<Audio>();
        gameObject.tag = GameManager.GUARD_TAG;
    }

    private void Update()
    {
        CheckFieldOfView();
        UpdateColor();
        if (guardType == GuardType.FOLLOWPATH)
        {
            switch (state)
            {
                case GuardState.SEARCH:
                    target = guardPath.targetWaypoint;
                    break;
                case GuardState.RETURN:
                    ReturnPath();
                    break;
                case GuardState.PURSUE:
                    target = fov.GetTarget();
                    break;
                default:
                    state = GuardState.SEARCH;
                    break;
            }
        }

        else if (guardType == GuardType.STAYS)
        {
            switch (state) {
                case GuardState.PURSUE:
                    target = fov.GetTarget();
                    break;
                default:
                    target = null;
                    break;
            }
        }
    }
    public override Steering GetSteering()
    {
        if (game.GetGameRunningStatus() && !collisionBehavior)
        {
            Vector2 position = new Vector2(this.transform.position.x, this.transform.position.z);
            Vector2 forward = new Vector2(this.transform.forward.x, this.transform.forward.z);
            Vector2 targetPosition;

            if (state == GuardState.RETURN)
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
        }
        else if (!game.GetGameRunningStatus())
        {
            steering = new Steering();
        }

        return steering;
    }

    private void CheckFieldOfView()
    {
        if (fov.GetIsInFOV()) state = GuardState.PURSUE;
        else
        {
            SetNextState();
        }
    }

    private void ReturnPath()
    {
        idlePoint = guardPath.targetWaypoint;
        pathElapsed += Time.deltaTime;
        if (pathElapsed > 0.5f)
        {
            pathElapsed -= 0.5f;
            NavMesh.CalculatePath(transform.position, idlePoint.position, NavMesh.AllAreas, navMeshPath);

            int index = 0;
            float distance = Vector3.Distance(transform.position, navMeshPath.corners[index]);

            int pathLen = navMeshPath.corners.Length - 1;
            while (distance < 1f && index < pathLen)
            {
                index++;
                distance = Vector3.Distance(transform.position, navMeshPath.corners[index]);
            }
            targetVector = navMeshPath.corners[index];
        }

    }

    private void SetNextState()
    {
        switch (guardType)
        {
            case GuardType.FOLLOWPATH:
                state = GuardState.RETURN;
                break;
            default:
                state = GuardState.SEARCH;
                break;
        }
    }

    public GuardState GetGuardState()
    {
        return state;
    }

    public void SetGuardState(GuardState nextState)
    {
        state = nextState;
    }

    public void ResetGuardState()
    {
        state = GuardState.SEARCH;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
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

        if (collision.transform.gameObject.CompareTag(GameManager.PLAYER_TAG))
        {
            game.GuardCaughtPlayer();
        }
    }

    private void UpdateColor()
    {
        switch (state)
        {
            case GuardState.IDLE:
                cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.grey, 255);
                break;
            case GuardState.PURSUE:
                cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.red, 255);
                break;
            case GuardState.RETURN:
                cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.blue, 255);
                break;
            case GuardState.SEARCH:
                cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.yellow, 255);
                break;
            default: break;
        }
    }

}