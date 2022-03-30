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

    public void Start()
    {
        gameObject.tag = GameManager.PLAYER_TAG;
    }
    public static string HORIZONTAL = "Horizontal";
    public static string VERTICAL = "Vertical";
    public static string HORIZONTAL_WASD = "HorizontalWASD";
    public static string VERTICAL_WASD = "VerticalWASD";

    public InputKeyboard inputKeyboard;
    private string verticalAxis;
    private string horizontalAxis;

    // This should be set by the game manager on start
    public void setInputKeyboard(InputKeyboard _inputKeyboard) {
        this.inputKeyboard = _inputKeyboard;
        this.horizontalAxis = inputKeyboard == InputKeyboard.arrows ? HORIZONTAL : HORIZONTAL_WASD;
        this.verticalAxis = inputKeyboard == InputKeyboard.arrows ? VERTICAL : VERTICAL_WASD;
    }

    public override Steering GetSteering()
    {
        float horizontal; // = Input.GetAxis(horizontalAxis);
        float vertical; // = Input.GetAxis(verticalAxis);

        if (inputKeyboard == InputKeyboard.wasd)
        {
            horizontal = Input.GetAxis(HORIZONTAL_WASD);
            vertical = Input.GetAxis(VERTICAL_WASD);
        }
        else
        {
            horizontal = Input.GetAxis(HORIZONTAL);
            vertical = Input.GetAxis(VERTICAL);
        }

        Steering steering = new Steering();
        steering.linear = new Vector3(horizontal, 0, vertical) * agent.maxAccel;
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));

        return steering;
    }

}
