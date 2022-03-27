using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{
    public float speed = 2.0f;
    public void Start(){
    }
    public override Steering GetSteering()
    {
        Vector3 direction = VectorAwayFromTag("Dog");

        Steering steering = new Steering();
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(direction, agent.maxAccel));

        return steering;
    }

    private Vector3 VectorAwayFromTag(string tag)
    {
        Vector3 position = this.transform.position;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        Vector3 direction = new Vector3();

        foreach (GameObject enemy in enemies)
        {
            Vector3 dogDistance = position - enemy.transform.position;
            // Dog distance should never be 0
            Vector3 inverseProportional = new Vector3(1 / dogDistance.x, 0, 1 / dogDistance.z);
            direction = direction + inverseProportional;
        }
        direction = direction.normalized * speed * agent.maxAccel;
        return direction;
    }
}
