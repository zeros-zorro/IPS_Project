using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinnerText : MonoBehaviour
{
    private TextMeshProUGUI GUI;
    private GameManager game;
    // Start is called before the first frame update
    void Start()
    {
        game = this.GetComponentInParent<GameManager>();
        GUI = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (game.HasGameEnded())
        {
            List<GameManager.Players> winner = game.getWinner();
            if (winner.Count == 1)
            {
                GUI.text = winner[0] == GameManager.Players.player1
                    ? "TEAM BLUE"
                    : "TEAM PURPLE";
                GUI.color = winner[0] == GameManager.Players.player1
                    ? Color.blue
                    : Color.magenta;

            } else if (winner.Count == 2)
            {
                GUI.text = "DRAW";
                GUI.color = Color.cyan;
            }
        }
    }
}
