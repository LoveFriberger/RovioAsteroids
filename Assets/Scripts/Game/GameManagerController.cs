using System;
using UnityEngine;

public class GameManagerController
{
    readonly GameManagerModel model;

    public GameManagerController(GameManagerModel model) 
    {
        this.model = model;
    }

    public void AddPlayerKilledAction(Action action)
    {
        model.onPlayerKilled += action;
    }

    public void RemovePlayerKilledAction(Action action)
    {
        model.onPlayerKilled -= action;
    }

    public void InvokePlayerKilledAction()
    {
        model.onPlayerKilled?.Invoke();
        Debug.Log("Player killed action invoked");
    }

    public void AddResetGameAction(Action action)
    {
        model.onResetGame += action;
    }

    public void RemoveResetGameAction(Action action)
    {
        model.onResetGame -= action;
    }

    public void InvokeResetGameAction()
    {
        model.onResetGame?.Invoke();
        Debug.Log("Reset game action invoked");
    }

    public void SetPause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
        Debug.Log(string.Format("Game {0}paused", pause? "":"un"));
    }
}
