using System.Collections;
using UnityEngine;

// Static class to store the game parameters
public static class GameParameter
{
    public static float gameTimer = 2f; // Default timer
    // Default player inputs
    public static MoveWithKeyboardBehavior.InputKeyboard[] inputs = { MoveWithKeyboardBehavior.InputKeyboard.wasd,
                                                                        MoveWithKeyboardBehavior.InputKeyboard.arrows };
    public static Color[] colors = { Color.blue, Color.magenta }; // Default colors

    public static void SetGameTimer(float timer)
    {
        gameTimer = timer;
    }

    public static void SetPlayer1Input(MoveWithKeyboardBehavior.InputKeyboard input)
    {
        inputs[(int) GameManager.Players.player1] = input;
    }

    public static void SetPlayer2Input(MoveWithKeyboardBehavior.InputKeyboard input)
    {
        inputs[(int)GameManager.Players.player2] = input;
    }

    public static void SetPlayer1Color(Color color)
    {
        colors[(int)GameManager.Players.player1] = color;
    }

    public static void SetPlayer2Color(Color color)
    {
        colors[(int)GameManager.Players.player2] = color;
    }
}
