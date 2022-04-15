using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinnerText : MonoBehaviour
{
    private TextMeshProUGUI GUI;
    private GameManager game;
    private bool winnerWasDisplayed = false;
    // Start is called before the first frame update
    void Start()
    {
        game = this.GetComponentInParent<GameManager>();
        GUI = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (game.HasGameEnded() && !winnerWasDisplayed)
        {
            List<GameManager.Players> winner = game.getWinner();
            if (winner.Count == 1)
            {
                Color color = GameParameter.colors[(int)winner[0]];
                GUI.text = "TEAM " + GameParameter.getColorName(color);
                GUI.color = color;

            } else if (winner.Count == 2)
            {
                GUI.text = "DRAW";
                GUI.color = Color.black;
            }
            winnerWasDisplayed = true;
        }
    }
}
