using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemBehavior : MonoBehaviour
{
    public Vector3 center;
    public Vector3 mapSize;
    public float invokeTimeDelay = 2.0f;

    private GameManager game;
    private bool isGemDisplayed = false;
    private Audio audioGem;


    void Start()
    {
        gameObject.tag = GameManager.GEM_TAG;
        audioGem = GameObject.FindGameObjectWithTag(GameManager.AUDIO_TAG).GetComponent<Audio>();
        game = this.GetComponentInParent<GameManager>();
        isGemDisplayed = false;
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (game.GetGameRunningStatus() && !isGemDisplayed && !IsAPlayerInGemState())
        {
            Invoke("DisplayGem", invokeTimeDelay);
            isGemDisplayed = true;
        }
    }

    // To get if the gem is displayed
    public bool GetGemStatus()
    {
        return isGemDisplayed;
    }

    private bool IsAPlayerInGemState() {

        bool isGemStateActive = false;
        GameObject[] playerList = GameObject.FindGameObjectsWithTag(GameManager.PLAYER_TAG);
        foreach (GameObject player in playerList)
        {
            if (player.GetComponent<PlayerBehavior>().GetPlayerState() == PlayerBehavior.playerState.gemState)
            {
                isGemStateActive = true;
                break;
            }
        }

        return isGemStateActive;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent.CompareTag(GameManager.PLAYER_TAG))
        {
            audioGem.gemEarnedSound();
            Debug.Log("Someone got the gem !");
            ActivePlayerBonus();
            HideGem();
        }
    }

    private void ActivePlayerBonus()
    {
        GameObject closestPlayer = FindClosestPlayer();
        Debug.Log("The player who earned the gem is player " + (closestPlayer.GetComponent<PlayerBehavior>().GetPlayerNumber()+1).ToString());
        closestPlayer.GetComponent<PlayerBehavior>().EnterGemState();
    }


    // To display the gem in the game
    private void DisplayGem()
    {
        audioGem.gemAppearedSound();
        gameObject.transform.position = GetRandomPosition();
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    // To hide the gem once the player got it
    private void HideGem()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        isGemDisplayed = false;
    }

    public Vector3 GetRandomPosition()
    {
        Vector3 pos = center + new Vector3(Random.Range(-mapSize.x / 2, mapSize.x / 2), Random.Range(-mapSize.y / 2, mapSize.y / 2), Random.Range(-mapSize.z / 2, mapSize.z / 2));

        return pos;
        //GameObject newObj = Instantiate(obj, pos, Quaternion.identity);
        //StartCoroutine(ObjectAppearDisappear(newObj, delay));

    }

    // To find who earned the gem
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
}
