using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{
    public float speed = 2.0f;
    public void Start(){
    }
    public override Steering GetSteering()
    {
        Vector3 position = this.transform.position;
        GameObject[] dogs = GameObject.FindGameObjectsWithTag("Dog");
        Vector3 direction = new Vector3();
        
        foreach (GameObject dog in dogs) {
            Vector3 dogDistance = position - dog.transform.position;
            // Dog distance should never be 0
            Vector3 inverseProportional = new Vector3(1 / dogDistance.x, 0, 1 / dogDistance.z);
            direction = direction + inverseProportional;
        }

        direction = direction.normalized * speed * agent.maxAccel;

        Steering steering = new Steering();
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(direction, agent.maxAccel));

        return steering;
    }

}
