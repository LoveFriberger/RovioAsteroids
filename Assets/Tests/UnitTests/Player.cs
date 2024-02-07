using UnityEngine;
using Zenject;
using NUnit.Framework;

[TestFixture]
public class Player : ZenjectUnitTestFixture
{
    [SetUp]
    public void Install()
    {
        var rigidbody = new GameObject().AddComponent<Rigidbody2D>();
        Container.Bind<PlayerModel>().AsSingle().WithArguments(rigidbody, rigidbody.transform);

        Container.Bind<PlayerMover.Settings>().AsSingle();

        Container.Bind<InputModel>().AsSingle();
        Container.Bind<InputView>().AsSingle();

        Container.BindInterfacesAndSelfTo<PlayerMover>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerShooter>().AsSingle();
        Container.Bind<PlayerSpawner>().AsSingle();

        Container.Inject(this);
    }

    [Inject]
    PlayerMover playerMover;
    [Inject]
    InputModel inputModel;
    [Inject]
    PlayerModel playerModel;

    [Test]
    public void Turn()
    {
        inputModel.inputType = InputModel.Type.Player;

        playerModel.Rotation = Quaternion.Euler(0, 0, 40);
        inputModel.leftInputHold = true;
        playerMover.FixedTick();
        inputModel.leftInputHold = false;
;
        Assert.True(playerModel.Rotation.eulerAngles.z > 40);

        playerModel.Rotation = Quaternion.Euler(0, 0, 40);
        inputModel.rightInputHold = true;
        playerMover.FixedTick();
        Assert.True(playerModel.Rotation.eulerAngles.z < 40);
        inputModel.rightInputHold = false;
    }

    
}
