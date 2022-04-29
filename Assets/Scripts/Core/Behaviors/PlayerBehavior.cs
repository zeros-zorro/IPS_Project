using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    //The player states
    public enum playerState
    {
        idleState,
        gemState
    }

    public GameManager.Players playerNumber;
    private playerState state;
    private GameManager game;
    private Audio audioPlayer;

    // Start is called before the first frame update
    void Start()
    {
        audioPlayer = GameObject.FindGameObjectWithTag(GameManager.AUDIO_TAG).GetComponent<Audio>();
        game = this.GetComponentInParent<GameManager>();
        gameObject.tag = GameManager.PLAYER_TAG;
        state = playerState.idleState;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // To retrieve the player number
    public int GetPlayerNumber()
    {
        return (int)playerNumber;
    }

    // To make the player enter the gem state where he can steal the other player 2 points if a touches the other player's robot
    public void EnterGemState()
    {
        state = playerState.gemState;
    }

    // To make the player enter the idle state 
    public void EnterIdleState()
    {
        state = playerState.idleState;
    }

    // To retrieve the current player state
    public PlayerBehavior.playerState GetPlayerState()
    {
        return state;
    }

    // For the gem state
    void OnCollisionEnter(Collision collision)
    {
        if (state == playerState.gemState)
        {
            if (collision.transform.gameObject.CompareTag(GameManager.PLAYER_TAG))
            {
                //audioGS.loosePointSound();

                game.subScoreToPlayer(collision.transform.gameObject, GameParameter.gemPointValue);
                game.addScoreToPlayer(gameObject, GameParameter.gemPointValue);
                audioPlayer.powerOfGemUsedSound();
                state = playerState.idleState;
                Debug.Log("THE POWER OF THE GEM WAS USED");
            }
        }
    }

}
