using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class GhostSheepBehavior : AgentBehaviour
{
    bool isGhost = false;
    bool wasInCollision = false;
    // Range under which the Sheep escapes
    public float minRange = 30f;
    // Greatest time between sheep/ghost cycle
    public float swithRate = 15f;

    // Range under which the ghost does not run after the dog after collision
    public float safeRange = 6f;
    private CelluloAgent cellulo;
    private Audio audioGS;
    private GameManager game;

    public void Start()
    {
        game = this.GetComponentInParent<GameManager>();
        cellulo = gameObject.GetComponent<CelluloAgent>();
        audioGS = GameObject.FindGameObjectWithTag("Audio").GetComponent<Audio>();
    }

    // Called by the GameManager when the START button was clicked
    public void StartGhostSheep()
    {
        //implements the switch between ghost and sheep for the ghostsheep
        InvokeRepeating("SwitchGhostSheepMode", 5f + Random.Range(0, 8), swithRate);
    }

    private void Update()
    {
        if (!game.GetGameRunningStatus())
        {
            CancelInvoke();
        }
    }

    public override Steering GetSteering()
    {
        Steering steering = new Steering();

        if (game.GetGameRunningStatus())
        {
            Vector3 direction = Vector3.zero;
            if (!isGhost) //if sheep
            {
                direction = GetFleeingDirection();
                steering.linear = direction * agent.maxAccel;
                steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));
            }
            else //if ghost
            {
                GameObject target = FindClosestPlayer();
                Vector3 targetPosition = target.transform.position;
                if (!wasInCollision)
                {
                    direction = targetPosition - transform.position;
                } else
                {
                    Vector3 diff = targetPosition - transform.position;
                    if (diff.sqrMagnitude >= safeRange)
                    {
                        wasInCollision = false;
                    }
                }
                steering.linear = direction * agent.maxAccel;
                steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));
            }
        }

        return steering;
    }

    public void SwitchGhostSheepMode()
    {
        isGhost = !isGhost;
        if (isGhost)
        {
            cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.red, 255);
            audioGS.wolfSound();
            gameObject.tag = GameManager.GHOST_TAG;
        }
        else
        {
            cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.green, 255);
            audioGS.sheepSound();
            gameObject.tag = GameManager.SHEEP_TAG;
        }

    }

    public GameObject FindClosestPlayer()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(GameManager.PLAYER_TAG);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    public Vector3 GetFleeingDirection() {

        Vector3 finalDirection = Vector3.zero;

        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(GameManager.PLAYER_TAG);
        foreach (GameObject go in gos) {
            Vector3 diff = transform.position - go.transform.position;
            if (diff.sqrMagnitude < minRange) {
                finalDirection += diff;
            }
        }
        return finalDirection * agent.maxAccel;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (isGhost)
        {
            wasInCollision = true;
            if (collision.transform.gameObject.CompareTag(GameManager.PLAYER_TAG))
            {
                audioGS.loosePointSound();
                GameObject.FindGameObjectWithTag(GameManager.CONTROLLER_TAG).GetComponent<GameManager>().subScoreToPlayer(collision.transform.gameObject);
                Debug.Log("Collided with a ghost");
            }
        }
    }

}