using System.Collections;
using System;
using UnityEngine;

public class GameModel
{
    public Action onPlayerKilled = null;
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
}
