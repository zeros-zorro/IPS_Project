using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static string SHEEP_TAG  = "Sheep";
    public static string GHOST_TAG  = "Dog";
    public static string PLAYER_TAG = "Player";

    public GameObject[] playerList;
    public int[] scoreList;
    public enum players
    {
        player1 = 0,
        player2 = 1
    }
    // Start is called before the first frame update
    void Start()
    {
        //initializePlayerList();
    }

    public void initializePlayerList()
    {
        playerList = GameObject.FindGameObjectsWithTag(GameManager.PLAYER_TAG);
        for (int i = 0; i < playerList.Length; ++i)
        {
            playerList[i].GetComponent<MoveWithKeyboardBehavior>().setInputKeyboard((MoveWithKeyboardBehavior.InputKeyboard)i);
        }
    }

    public void addScoreToPlayer(GameObject player) {
        for (int i = 0; i < playerList.Length; ++i)
        {
            if (player == playerList[i])
            {
                scoreList[i]++;
                break;
            }
        }
    }

    public void subScoreToPlayer(GameObject player)
    {
        for (int i = 0; i < playerList.Length; ++i)
        {
            if (player == playerList[i])
            {
                scoreList[i]--;
                break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
