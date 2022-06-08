using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPointTrigger : MonoBehaviour
{
    private Audio audioEndPoint;
    void Start()
    {
        gameObject.tag = GameManager.END_TAG;
        audioEndPoint = GameObject.FindGameObjectWithTag(GameManager.AUDIO_TAG).GetComponent<Audio>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.CompareTag(GameManager.PLAYER_TAG))
        {
            //audioEndPoint.endPointSound(); //TODO to be defined
            Debug.Log("A player cleared the stage.");
            GameObject.FindGameObjectWithTag(GameManager.CONTROLLER_TAG).GetComponent<GameManager>().StageClearedAction();
        }
    }
}
