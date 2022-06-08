using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class GuardBehavior : AgentBehaviour
{

    public enum WatcherState
    {
        IDLE,
        PURSUE,
        SEARCH,
        RETURN
    };

    private CelluloAgent cellulo;
    private Audio audioGuard;
    private GameManager game;
    private WatcherState state;

    public void Start()
    {
        state = WatcherState.IDLE;
        game = this.GetComponentInParent<GameManager>();
        cellulo = gameObject.GetComponent<CelluloAgent>();
        audioGuard = GameObject.FindGameObjectWithTag(GameManager.AUDIO_TAG).GetComponent<Audio>();
        gameObject.tag = GameManager.GUARD_TAG;
    }


    private void Update()
    {

    }

    public GuardBehavior.WatcherState GetGuardState()
    {
        return state;
    }

    public void SetGuardState(GuardBehavior.WatcherState state)
    {
        this.state = state;
        UpdateColor();
    }

    public void ResetGuardState()
    {
        state = WatcherState.IDLE;
        UpdateColor();
    }

    private void UpdateColor()
    {
        switch (state)
        {
            case WatcherState.IDLE:
                cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.grey, 255);
                break;
            case WatcherState.PURSUE:
                cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.red, 255);
                break;
            case WatcherState.RETURN:
                cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.blue, 255);
                break;
            case WatcherState.SEARCH:
                cellulo.SetVisualEffect(VisualEffect.VisualEffectConstAll, Color.yellow, 255);
                break;
            default: break;
        }
    }


    public GameObject FindClosestPlayer()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag(GameManager.PLAYER_TAG);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }


    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.gameObject.CompareTag(GameManager.PLAYER_TAG))
        {
            game.GuardCaughtPlayer();
        }
    }

}