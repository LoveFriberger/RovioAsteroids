using System;
using System.Threading.Tasks;
using UnityEngine;

public class RockDamageTaker
{
    readonly PointsController pointsController;
    readonly Settings settings;
    readonly RockSplitter rockSplitter;

    public RockDamageTaker(PointsController pointsController, Settings settings, RockSplitter rockSplitter)
    {
        this.pointsController = pointsController;
        this.settings = settings;
        this.rockSplitter = rockSplitter;
    }

    public async void TakeDamage()
    {
        Debug.Log("A rock is taking damage");
        GivePoints();

        await rockSplitter.Split();
    }


    void GivePoints()
    {
        pointsController.AddPoints(settings.points);
    }

    [Serializable]
    public class Settings
    {
        public int points = 1;
    }
}
