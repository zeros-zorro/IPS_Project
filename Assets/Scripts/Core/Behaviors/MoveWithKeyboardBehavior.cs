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
    public InputKeyboard inputKeyboard;

    // Start is called before the first frame update
    void Start()
    {
        gameObject.tag = "Player";
    }

    public override Steering GetSteering()
    {
        float horizontal;
        float vertical;
        if (inputKeyboard == InputKeyboard.wasd)
        {
            horizontal = Input.GetAxis("PlayerHorizontal1");
            vertical = Input.GetAxis("PlayerVertical1");
        } else {
            horizontal = Input.GetAxis("PlayerHorizontal2");
            vertical = Input.GetAxis("PlayerVertical2");
        }
        
        Steering steering = new Steering();
        //implement your code here
        steering.linear = new Vector3(horizontal, 0, vertical) * agent.maxAccel; steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.
        linear, agent.maxAccel));
        return steering;
    }

}
