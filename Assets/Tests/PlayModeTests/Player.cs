using System.Collections;
using UnityEngine;
using Zenject;
using NUnit.Framework;
using UnityEngine.TestTools;
using UnityEngine.AddressableAssets;
using UnityEditor;

public class Player : ZenjectIntegrationTestFixture
{
    void Install()
    {
        PreInstall();

        Container.Bind<GameManagerModel>().AsSingle();
        Container.Bind<GameManagerController>().AsSingle();
        var rigidbody = new GameObject().AddComponent<Rigidbody2D>();
        Container.Bind<PlayerModel>().AsSingle().WithArguments(rigidbody, rigidbody.transform);

        Container.Bind<InputModel>().AsSingle();
        Container.Bind<InputView>().AsSingle();
        Container.BindInterfacesAndSelfTo<PlayerMover>().AsSingle();

        var transform = new GameObject().transform;
        Container.BindInstance(transform);

        Container.Inject(this);

        PostInstall();
    }

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
