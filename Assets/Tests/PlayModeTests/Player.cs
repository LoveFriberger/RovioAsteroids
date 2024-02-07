using System.Collections;
using UnityEngine;
using Zenject;
using NUnit.Framework;
using UnityEngine.TestTools;

public class Player : ZenjectIntegrationTestFixture
{
    void Install()
    {
        PreInstall();

        var rigidbody = new GameObject().AddComponent<Rigidbody2D>();
        Container.Bind<PlayerModel>().AsSingle().WithArguments(rigidbody, rigidbody.transform);
        Container.Bind<PlayerMover.Settings>().AsSingle();

        Container.Bind<InputModel>().AsSingle();
        Container.Bind<InputView>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerMover>().AsSingle();

        Container.Inject(this);

        PostInstall();
    }

    [Inject]
    PlayerMover playerMover;
    [Inject]
    InputModel inputModel;
    [Inject]
    PlayerModel playerModel;

    [UnityTest]
    public IEnumerator Accelerate()
    {
        Install();
        Assert.True(playerModel.Velocity.sqrMagnitude == 0);
        inputModel.inputType = InputModel.Type.Player;
        inputModel.upInputHold = true;
        yield return null;

        Assert.True(playerModel.Velocity.sqrMagnitude > 0);
    }
}
