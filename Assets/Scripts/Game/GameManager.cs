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
    public static int DEFAULT_NUMBER_OF_PLAYERS = 2;
    public static string SHEEP_TAG = "Sheep";
    public static string GHOST_TAG = "Ghost";
    public static string PLAYER_TAG = "Player";
    public static string SCORE_TAG = "Score";
    public static string CONTROLLER_TAG = "GameController";

    private GameObject[] playerList;
    private int[] scoreList;
    private bool isGameOn = false;
    private bool hasGameEnded = false;
    private Timer timer;
    private Canvas[] CanvasPlayerGUIs;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = CONTROLLER_TAG;
        isGameOn = false;
        hasGameEnded = false;
        timer = this.GetComponentInChildren<Timer>();
        CanvasPlayerGUIs = FindObjectsOfType<Canvas>();
        DisplayGameUI();
    }

    void Update()
    {
        if (timer.verifyIfTimeIsUp())
        {
            hasGameEnded = true;
            DisplayEndScreen();
            QuitGame();
        }
    }

    public bool HasGameEnded()
    {
        return hasGameEnded;
    }

    // To display the game UI (the scores, the back button, the start button)
    private void DisplayGameUI()
    {
        foreach (Canvas CanvasPlayerGUI in CanvasPlayerGUIs)
        {
            switch (CanvasPlayerGUI.name)
            {
                case "Canvas EndGame":
                    CanvasPlayerGUI.enabled = false;
                    break;
                case "Canvas UI":
                    CanvasPlayerGUI.enabled = true;
                    break;
                default: break;
            }
        }
    }

    // To display the end screen with the winner(s) name
    private void DisplayEndScreen()
    {
        foreach (Canvas CanvasPlayerGUI in CanvasPlayerGUIs)
        {
            switch (CanvasPlayerGUI.name)
            {
                case "Canvas EndGame":
                    CanvasPlayerGUI.enabled = true;
                    break;
                case "Canvas UI":
                    CanvasPlayerGUI.enabled = false;
                    break;
                default: break;
            }
        }
    }

    public void StartGame()
    {
        isGameOn = true;
        this.GetComponentInChildren<GhostSheepBehavior>().StartGhostSheep();
        playerList = GameObject.FindGameObjectsWithTag(PLAYER_TAG);
        for (int i = 0; i < playerList.Length; ++i)
        {
            playerList[i].GetComponent<MoveWithKeyboardBehavior>()
                .SetInputKeyboard((MoveWithKeyboardBehavior.InputKeyboard)playerList.Length - i - 1);
        }
        scoreList = new int[playerList.Length];
    }

    // To pause the game
    public void PauseGame()
    {
        isGameOn = false;
    }

    // To pause the game
    public void QuitGame()
    {
        isGameOn = false;
        timer.resetTimer();
    }

    // To resume the game
    public void ResumeGame()
    {
        this.GetComponentInChildren<GhostSheepBehavior>().StartGhostSheep();
        isGameOn = true;
    }

    // To get if the game is running of not
    public bool GetGameRunningStatus()
    {
        return isGameOn;
    }

    public void addScoreToPlayer(GameObject player)
    {
        int playerNumber = player.GetComponent<MoveWithKeyboardBehavior>().getPlayerNumber();
        scoreList[playerNumber]++;
    }

    public void subScoreToPlayer(GameObject player)
    {
        int playerNumber = player.GetComponent<MoveWithKeyboardBehavior>().getPlayerNumber();
        scoreList[playerNumber]--;
    }

    public int getScorePlayer(Players p)
    {
        return scoreList[(int)p];
    }

    public List<Players> getWinner()
    {
        List<Players> list = new List<Players>();
        if (scoreList[0] < scoreList[1])
        {
            list.Add(Players.player2);
        }
        else if (scoreList[0] > scoreList[1])
        {
            list.Add(Players.player1);
        }
        else
        {
            list.Add(Players.player1);
            list.Add(Players.player2);
        }

        return list;
    }

}
