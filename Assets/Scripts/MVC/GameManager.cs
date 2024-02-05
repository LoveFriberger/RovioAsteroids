using System.Collections;
using System;
using UnityEngine;

public class GameModel
{
    public Action onPlayerKilled = null;
    public Action onResetGame = null;
}

public class GameViewer
{
    protected GameModel model = new();

    public GameViewer(){}
}

public class GameController : GameViewer
{
    public GameController() { }

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
