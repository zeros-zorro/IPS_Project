using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == GameManager.SHEEP_TAG) {
            FindClosestPlayerAndScore();
        }
    }

    void FindClosestPlayerAndScore() {
        GameObject[] players = GameObject.FindGameObjectsWithTag(GameManager.PLAYER_TAG);
        float minDistance = float.MaxValue;
        GameObject closestPlayer = players[0];
        foreach(GameObject player in players) {
            float distance = (this.transform.position - player.transform.position).magnitude;
            if (distance < minDistance) {
                minDistance = distance;
                closestPlayer = player;
            }
        }
        gameManager.addScoreToPlayer(closestPlayer);
    }
}
