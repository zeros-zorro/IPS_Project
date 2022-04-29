using System.Collections;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject obj;
    public Vector3 center;
    public Vector3 mapSize;
    public float invokeTimeDelay = 5.0f;

    private GameManager game;
    private bool isGemDisplayed = false;  


    // Start is called before the first frame update
    void Start()
    {
        game = this.GetComponentInParent<GameManager>();
        gameObject.tag = GameManager.GEM_TAG;
        gameObject.SetActive(false);
        isGemDisplayed = false;

    }

    // Update is called once per frame
    void Update()
    {
        //Invoke("GemBehavior", invokeTimeDelay);
        if (game.GetGameRunningStatus() && !isGemDisplayed)
        {
            Invoke("DisplayGem", invokeTimeDelay);
            isGemDisplayed = true;
        }
    }

    // To display the gem in the game
    private void DisplayGem()
    {
        Vector3 randomPos = GetRandomPosition();
        gameObject.transform.position = randomPos;
        gameObject.SetActive(true);
    }

    // To get a random position 
    private Vector3 GetRandomPosition()
    {
        Vector3 pos = center + new Vector3(Random.Range(-mapSize.x / 2, mapSize.x / 2), Random.Range(-mapSize.y / 2, mapSize.y / 2), Random.Range(-mapSize.z / 2, mapSize.z / 2));

        return pos;
    }
}