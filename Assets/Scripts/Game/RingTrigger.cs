using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    public GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.CompareTag(GameManager.SHEEP_TAG)) {
            FindClosestPlayerAndScore();
            print("A sheep entered the ring");
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
        print("Found closest player with tag " + closestPlayer.tag);
        gameManager.addScoreToPlayer(closestPlayer);
    }
}
