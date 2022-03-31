using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{
    bool isGhost = false;
    // Range under which the Sheep escapes
    public float minRange = 30f;
    public float swithRate = 10f;

    public void Start()
    {
        //implements the switch between ghost and sheep for the ghostsheep
        InvokeRepeating("switchGhostSheepMode", 5f + Random.Range(0, 8), swithRate);

    }

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        //implement your code here.
        GameObject target = FindClosestEnemy();
        Vector3 targetPosition = target.transform.position;
        Vector3 diff = transform.position - targetPosition;
        if (isGhost)
        {
            diff = -diff;
        }

        if (!isGhost)
        {
            if (diff.sqrMagnitude < minRange) {
                steering.linear = diff * agent.maxAccel; steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
                linear, agent.maxAccel));
            } else
            {
                steering.linear = Vector3.zero * agent.maxAccel; steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
                linear, agent.maxAccel));
            }
        } else
        {
            steering.linear = diff * agent.maxAccel; steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
            linear, agent.maxAccel));
        }
        return steering;
    }

    public void switchGhostSheepMode()
    {
        isGhost = !isGhost;
        gameObject.tag = isGhost ? GameManager.GHOST_TAG : GameManager.SHEEP_TAG;
    }

    public GameObject FindClosestEnemy()
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

}
