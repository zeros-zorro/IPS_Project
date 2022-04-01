using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingTrigger : MonoBehaviour
{
    private AudioRing audioRing;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Ring";
        audioRing = gameObject.GetComponent<AudioRing>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.CompareTag(GameManager.SHEEP_TAG)) {
            
            audioRing.winPointSound();
            print("A sheep entered the ring");
            FindClosestPlayerAndScore();
        }
    }

    void FindClosestPlayerAndScore() {
        GameObject sheep = GameObject.FindGameObjectWithTag(GameManager.SHEEP_TAG);
        GameObject closestPlayer = sheep.GetComponent<GhostSheepBehavior>().FindClosestPlayer();
        print("Found closest player with tag " + closestPlayer.tag);
        GameObject.FindGameObjectWithTag(GameManager.CONTROLLER_TAG).GetComponent<GameManager>().addScoreToPlayer(closestPlayer);
    }
}
