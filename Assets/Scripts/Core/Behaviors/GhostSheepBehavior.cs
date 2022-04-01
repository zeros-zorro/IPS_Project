using System.Linq;
using UnityEngine;

public class GhostSheepBehavior : AgentBehaviour
{
    bool isGhost = false;
    // Range under which the Sheep escapes
    public float minRange = 30f;
    public float swithRate = 15f;
    private CelluloAgent cellulo;
    private Audio audioGS;

    public void Start()
    {
        cellulo = gameObject.GetComponent<CelluloAgent>();
        audioGS = gameObject.GetComponent<Audio>();
        //implements the switch between ghost and sheep for the ghostsheep
        InvokeRepeating("switchGhostSheepMode", 5f + Random.Range(0, 8), swithRate);
        cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.green, 255);
    }

    public override Steering GetSteering()
    {
        Steering steering = new Steering();
        //implement your code here.
        GameObject target = FindClosestPlayer();
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
        if (isGhost)
        {
            cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.yellow, 255);
            audioGS.wolfSound();
            gameObject.tag = GameManager.GHOST_TAG;
        }
        else
        {
            cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.green, 255);
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

    void OnCollisionEnter(Collision collision)
    {
        if (isGhost)
        {
            if (collision.transform.gameObject.CompareTag(GameManager.PLAYER_TAG))
            {
                audioGS.loosePointSound();
                GameObject.FindGameObjectWithTag(GameManager.CONTROLLER_TAG).GetComponent<GameManager>().subScoreToPlayer(collision.transform.gameObject);
                print("Collided with a ghost");
            }
        }
    }
}
