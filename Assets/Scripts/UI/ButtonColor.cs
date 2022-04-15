using UnityEngine;

public class ButtonColor : MonoBehaviour
{
    private GameManager.Players p1 = GameManager.Players.player1;
    private GameManager.Players p2 = GameManager.Players.player2;

    // To change the color for the player 1
    public void NextColorPlayer1()
    {
        GameParameter.PlayerNextColor(p1);
        GameObject.FindGameObjectsWithTag(GameManager.PLAYER_TAG)[(int) p1].GetComponent<CelluloAgent>()
            .SetVisualEffect(VisualEffect.VisualEffectConstAll,
            GameParameter.colors[(int) p1], 255);
    }

    public void PreviousColorPlayer1()
    {
        GameParameter.PlayerPreviousColor(p1);
        GameObject.FindGameObjectsWithTag(GameManager.PLAYER_TAG)[(int) p1].GetComponent<CelluloAgent>()
            .SetVisualEffect(VisualEffect.VisualEffectConstAll,
            GameParameter.colors[(int) p1], 255);
    }

    // To change the color for the player 2
    public void NextColorPlayer2()
    {
        GameParameter.PlayerNextColor(p2);
        GameObject.FindGameObjectsWithTag(GameManager.PLAYER_TAG)[(int)p2].GetComponent<CelluloAgent>()
            .SetVisualEffect(VisualEffect.VisualEffectConstAll,
            GameParameter.colors[(int)p2], 255);
    }

    public void PreviousColorPlayer2()
    {
        GameParameter.PlayerPreviousColor(p2);
        GameObject.FindGameObjectsWithTag(GameManager.PLAYER_TAG)[(int)p2].GetComponent<CelluloAgent>()
            .SetVisualEffect(VisualEffect.VisualEffectConstAll,
            GameParameter.colors[(int)p2], 255);
    }
}
