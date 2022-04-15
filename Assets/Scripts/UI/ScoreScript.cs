using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{

    private int[] scoreList;
    public TextMeshProUGUI[] GUI;
    private GameManager game;

    // Start is called before the first frame update
    void Start()
    {
        game = this.GetComponentInParent<GameManager>();
        gameObject.tag = GameManager.SCORE_TAG;
        scoreList = new int[GameManager.DEFAULT_NUMBER_OF_PLAYERS];
        for (int i = 0; i < GameManager.DEFAULT_NUMBER_OF_PLAYERS; ++i)
        {
            GUI[i].text = "0" + scoreList[i].ToString();
            GUI[GameManager.DEFAULT_NUMBER_OF_PLAYERS - i - 1].color =
                GameParameter.colors[GameManager.DEFAULT_NUMBER_OF_PLAYERS - i - 1];
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (game.GetGameRunningStatus())
        {
            for (int i = 0; i < GameManager.DEFAULT_NUMBER_OF_PLAYERS; ++i)
            {
                scoreList[i] = game.getScorePlayer((GameManager.Players)i);
                int score = Mathf.Abs(scoreList[i]);
                if (score < 10)
                {
                    GUI[i].text = Mathf.Sign(scoreList[i]) == -1
                        ? "-0" + score
                        : "0" + score;
                }
                else
                {
                    GUI[i].text = scoreList[i].ToString();
                }
            }
        }
    }

}
