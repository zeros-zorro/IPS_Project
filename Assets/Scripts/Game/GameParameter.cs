using System.Collections;
using UnityEngine;

public static class GameParameter
{
    public static float gameTimer = 2f; // Default timer
    public static MoveWithKeyboardBehavior.InputKeyboard player1_input = MoveWithKeyboardBehavior.InputKeyboard.wasd;
    public static MoveWithKeyboardBehavior.InputKeyboard player2_input = MoveWithKeyboardBehavior.InputKeyboard.arrows;

    public static void SetGameTimer(float timer)
    {
        gameTimer = timer;
    }

    public static void SetPlayer1Input(MoveWithKeyboardBehavior.InputKeyboard input)
    {
        player1_input = input;
    }

    public static void SetPlayer2Input(MoveWithKeyboardBehavior.InputKeyboard input)
    {
        player2_input = input;
    }
}
