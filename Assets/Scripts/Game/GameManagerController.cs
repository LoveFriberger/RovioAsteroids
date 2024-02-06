using System;
using UnityEngine;

public class GameManagerController
{
    readonly GameManagerModel model = null;

    public GameManagerController(GameManagerModel model) 
    {
        this.model = model;
    }

    public void AddOnPlayerKilledAction(Action action)
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
    }

    public void SetPause(bool pause)
    {
        Time.timeScale = pause ? 0 : 1;
    }
}
