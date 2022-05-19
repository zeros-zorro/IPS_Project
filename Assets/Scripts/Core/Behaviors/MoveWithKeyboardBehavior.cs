using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveWithKeyboardBehavior : AgentBehaviour
{
    //Input Keys
    public enum InputKeyboard
    {
        arrows = 0,
        wasd = 1
    }


    public static string HORIZONTAL = "Horizontal";
    public static string VERTICAL = "Vertical";
    public static string HORIZONTAL_WASD = "HorizontalWASD";
    public static string VERTICAL_WASD = "VerticalWASD";

    private GameManager game;
    private InputKeyboard inputKeyboard;
    private string verticalAxis = null;
    private string horizontalAxis = null;

    public void Start()
    {
        game = this.GetComponentInParent<GameManager>();
    }

    // This should be set by the game manager on start
    public void SetInputKeyboard(InputKeyboard _inputKeyboard) {
        this.inputKeyboard = _inputKeyboard;
        this.horizontalAxis = (inputKeyboard == InputKeyboard.arrows) ? HORIZONTAL : HORIZONTAL_WASD;
        this.verticalAxis = (inputKeyboard == InputKeyboard.arrows) ? VERTICAL : VERTICAL_WASD;
    }

    public override Steering GetSteering()
    {
        Steering steering = new Steering();

        if (game.GetGameRunningStatus())
        {
            float horizontal = Input.GetAxis(horizontalAxis);
            float vertical = Input.GetAxis(verticalAxis);


            steering.linear = new Vector3(horizontal, 0, vertical) * agent.maxAccel;
            steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));
        }
     
        return steering;
    }

    //implement the cellulo long touch
    public override void OnCelluloLongTouch(int key) {
        if (!game.GetGameRunningStatus())
        {
            game.StartGame();
        }
    }
}
