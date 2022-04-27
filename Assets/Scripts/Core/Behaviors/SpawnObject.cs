using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public GameObject obj;
    public Vector3 center;
    public Vector3 mapSize;


    // Start is called before the first frame update
    void Start()
    {
        SpawnObjectRandomly();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnObjectRandomly()
    {
        Vector3 pos = center + new Vector3(Random.Range(-mapSize.x / 2, mapSize.x / 2), Random.Range(-mapSize.y / 2, mapSize.y / 2), Random.Range(-mapSize.z / 2, mapSize.z / 2));

        Instantiate(obj, pos, Quaternion.identity);

    }

}
