using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{
    bool isGhost = false;
    public float minRange = 30f;

    public void Start()
    {
        //Set the tag of this GameObject to Player
        gameObject.tag = "GhostSheep";
        InvokeRepeating("changeGhostSheepMode", 0f + Random.Range(0, 8), 10f);

    }

    public void Update()
    {
        
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

    public void changeGhostSheepMode()
    {
        isGhost = !isGhost;
    }

    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Player");
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
