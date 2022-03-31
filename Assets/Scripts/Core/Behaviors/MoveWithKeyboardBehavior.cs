using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Input Keys
public enum InputKeyboard
{
    arrows = 0,
    wasd = 1
}

public class MoveWithKeyboardBehavior : AgentBehaviour
{
    public GameManager gameManager;
    public void Start()
    {
        gameManager = this.GetComponentInParent<GameManager>();
        gameObject.tag = GameManager.PLAYER_TAG;
    }
    public static string HORIZONTAL = "Horizontal";
    public static string VERTICAL = "Vertical";
    public static string HORIZONTAL_WASD = "HorizontalWASD";
    public static string VERTICAL_WASD = "VerticalWASD";

    public InputKeyboard inputKeyboard;
    private string verticalAxis;
    private string horizontalAxis;
    private Players playerNumber;

    // This should be set by the game manager on start
    public void setInputKeyboard(InputKeyboard _inputKeyboard, Players _playerNumber) {
        this.inputKeyboard = _inputKeyboard;
        this.horizontalAxis = inputKeyboard == InputKeyboard.arrows ? HORIZONTAL : HORIZONTAL_WASD;
        this.verticalAxis = inputKeyboard == InputKeyboard.arrows ? VERTICAL : VERTICAL_WASD;
        this.playerNumber = _playerNumber;
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
    public int getPlayerNumber() {
        return (int)playerNumber;
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject colliderGO = collision.transform.gameObject;
        if (colliderGO.tag == GameManager.GHOST_TAG) {
            gameManager.subScoreToPlayer(this.gameObject);
            print("Collided with a ghost");
        }
    }
}
