public class PlayerDamageTaker : IHitable
{
    readonly GameManagerController gameController;

    public PlayerDamageTaker(GameManagerController gameController)
    {
        this.gameController = gameController;
    }
        
    public void TakeDamage()
    {
        gameController.InvokePlayerKilledAction();
    }
}
