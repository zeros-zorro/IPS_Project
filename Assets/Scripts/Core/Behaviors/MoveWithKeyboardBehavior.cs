using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Input Keys
public enum InputKeyboard{
    arrows = 0, 
    wasd = 1
}

public class MoveWithKeyboardBehavior : AgentBehaviour
{
    public static string HORIZONTAL = "Horizontal";
    public static string VERTICAL = "Vertical";
    public static string HORIZONTAL_AWSD = "HorizontalAWSD";
    public static string VERTICAL_AWSD = "VerticalAWSD";

    private InputKeyboard inputKeyboard;
    private string verticalAxis;
    private string horizontalAxis;

    // This should be set by the game manager on start
    public void setInputKeyboard(InputKeyboard _inputKeyboard) {
        this.inputKeyboard = _inputKeyboard;
        this.horizontalAxis = _inputKeyboard == InputKeyboard.arrows ? HORIZONTAL : HORIZONTAL_AWSD;
        this.verticalAxis = _inputKeyboard == InputKeyboard.arrows ? VERTICAL : VERTICAL_AWSD;
    }

    public override Steering GetSteering()
    {
        float horizontal = Input.GetAxis(horizontalAxis);
        float vertical = Input.GetAxis(verticalAxis);

        Steering steering = new Steering();
        steering.linear = new Vector3(horizontal, 0, vertical) * agent.maxAccel;
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));

        return steering;
    }

}
