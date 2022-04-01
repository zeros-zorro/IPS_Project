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
    public GameManager gameManager;
    public static string HORIZONTAL = "Horizontal";
    public static string VERTICAL = "Vertical";
    public static string HORIZONTAL_WASD = "HorizontalWASD";
    public static string VERTICAL_WASD = "VerticalWASD";
    public InputKeyboard inputKeyboard;
    private string verticalAxis = null;
    private string horizontalAxis = null;
    private GameManager.Players playerNumber;
    private CelluloAgent cellulo;

    public void Start()
    {
        gameManager = this.GetComponentInParent<GameManager>();
        cellulo = gameObject.GetComponent<CelluloAgent>();
        gameObject.tag = GameManager.PLAYER_TAG;
        setColor();
    }

    // This should be set by the game manager on start
    public void setInputKeyboard(InputKeyboard _inputKeyboard, GameManager.Players _playerNumber) {
        this.inputKeyboard = _inputKeyboard;
        this.horizontalAxis = inputKeyboard == InputKeyboard.arrows ? HORIZONTAL : HORIZONTAL_WASD;
        this.verticalAxis = inputKeyboard == InputKeyboard.arrows ? VERTICAL : VERTICAL_WASD;
        this.playerNumber = _playerNumber;
    }

    public override Steering GetSteering()
    {
        float horizontal = Input.GetAxis(horizontalAxis);
        float vertical = Input.GetAxis(verticalAxis);
        /*if (verticalAxis != null && horizontalAxis != null)
        {
            horizontal = Input.GetAxis(horizontalAxis);
            vertical = Input.GetAxis(verticalAxis);
        } else
        {
            horizontalAxis = inputKeyboard == InputKeyboard.arrows ? HORIZONTAL : HORIZONTAL_WASD;
            verticalAxis = inputKeyboard == InputKeyboard.arrows ? VERTICAL : VERTICAL_WASD;
            horizontal = Input.GetAxis(horizontalAxis);
            vertical = Input.GetAxis(verticalAxis);
        }*/

        Steering steering = new Steering();
        steering.linear = new Vector3(horizontal, 0, vertical) * agent.maxAccel;
        steering.linear = this.transform.parent.TransformDirection(Vector3.ClampMagnitude(steering.linear, agent.maxAccel));

        return steering;
    }

    public int getPlayerNumber() {
        return (int)playerNumber;
    }

    private void setColor()
    {
        if (inputKeyboard == InputKeyboard.wasd)
        {
            cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.blue, 255);
        }
        else
        {
            cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.red, 255);
        }
    }

}
