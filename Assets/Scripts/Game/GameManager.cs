using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public enum Players
    {
        player1 = 0,
        player2 = 1
    }
    public static string SHEEP_TAG  = "Sheep";
    public static string GHOST_TAG  = "Ghost";
    public static string PLAYER_TAG = "Player";
    public static string SCORE_TAG = "Score";
    public static string CONTROLLER_TAG = "GameController";

    public GameObject[] playerList;
    public int[] scoreList;
    public GameObject[] scoresText;
    // Start is called before the first frame update
    void Start()
    {
        initializePlayerList();
        gameObject.tag = CONTROLLER_TAG;
    }

    public void initializePlayerList()
    {
        playerList = GameObject.FindGameObjectsWithTag(PLAYER_TAG);
        for (int i = 0; i < playerList.Length; ++i)
        {
            playerList[i].GetComponent<MoveWithKeyboardBehavior>().setInputKeyboard((MoveWithKeyboardBehavior.InputKeyboard)i, (Players)i); ;
        }
        scoreList = new int[playerList.Length];
        scoresText = GameObject.FindGameObjectsWithTag(SCORE_TAG);
    }

    public void addScoreToPlayer(GameObject player)
    {
        int playerNumber = player.GetComponent<MoveWithKeyboardBehavior>().getPlayerNumber();
        scoreList[playerNumber]++;
        //scoresText[playerNumber].GetComponent<TextMeshProUGUI>().SetText(scoreList[playerNumber].ToString());
        //to be uncommented when scoreUI available
    }

    public void subScoreToPlayer(GameObject player)
    {
        int playerNumber = player.GetComponent<MoveWithKeyboardBehavior>().getPlayerNumber();
        if (scoreList[playerNumber] > 0)
        {
            scoreList[playerNumber]--;
        }
        //scoresText[playerNumber].GetComponent<TextMeshProUGUI>().SetText(scoreList[playerNumber].ToString());
    }

}
