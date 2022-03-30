using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class GameManager : MonoBehaviour
{
    public static string SHEEP_TAG = "Sheep";
    public static string GHOST_TAG = "Ghost";
    public static string PLAYER_TAG = "Player";

    public GameObject player1;
    public GameObject player2;
    private int score1 = 0;
    private int score2 = 0;
    // Start is called before the first frame update
    void Start()
    {
        player1.GetComponent<MoveWithKeyboardBehavior>().setInputKeyboard(InputKeyboard.arrows);
        player2.GetComponent<MoveWithKeyboardBehavior>().setInputKeyboard(InputKeyboard.wasd);
        player1.tag = PLAYER_TAG;
        player2.tag = PLAYER_TAG;
    }

    public void addScoreToPlayer(GameObject player) {
        if (player == player1)
        {
            score1++;
        }
        else if(player == player2)
        {
            score2++;
        }
    }
    public void subScoreToPlayer(GameObject player)
    {
        if (player == player1)
        {
            score1--;
        }
        else if (player == player2)
        {
            score2--;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
