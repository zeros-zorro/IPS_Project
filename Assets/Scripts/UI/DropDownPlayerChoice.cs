using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropDownPlayerChoice : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void HandleInputData(int input)
    {
        if (input == 0)
        {         
            GameParameter.SetPlayerInput(GameManager.Players.player1, MoveWithKeyboardBehavior.InputKeyboard.wasd);
            GameParameter.SetPlayerInput(GameManager.Players.player2, MoveWithKeyboardBehavior.InputKeyboard.arrows);
        } else if (input == 1)
        {
            GameParameter.SetPlayerInput(GameManager.Players.player1, MoveWithKeyboardBehavior.InputKeyboard.arrows);
            GameParameter.SetPlayerInput(GameManager.Players.player2, MoveWithKeyboardBehavior.InputKeyboard.wasd);
        }
        
    }
}
