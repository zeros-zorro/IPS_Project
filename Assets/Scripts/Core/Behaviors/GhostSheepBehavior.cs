using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{    
    public void Start(){
        //Set the tag of this GameObject to Player
        gameObject.tag = "GhostSheep";
    }

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        //implement your code here.
        GameObject target = FindClosestEnemy();
        Vector3 targetPosition = target.transform.position;
        Vector3 diff = targetPosition - transform.position;
        steering.linear = diff * agent.maxAccel; steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
        linear, agent.maxAccel));
        return steering;
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
