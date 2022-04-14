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
            GameParameter.SetPlayer1Input(MoveWithKeyboardBehavior.InputKeyboard.wasd);
            GameParameter.SetPlayer2Input(MoveWithKeyboardBehavior.InputKeyboard.arrows);
        } else if (input == 1)
        {
            GameParameter.SetPlayer1Input(MoveWithKeyboardBehavior.InputKeyboard.arrows);
            GameParameter.SetPlayer2Input(MoveWithKeyboardBehavior.InputKeyboard.wasd);
        }
        
    }
}
