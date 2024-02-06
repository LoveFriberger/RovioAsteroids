using Zenject;
using NUnit.Framework;

[TestFixture]
public class Input : ZenjectUnitTestFixture
{
    [SetUp]
    public void Install()
    {
        Container.Bind<InputModel>().AsSingle();
        Container.Bind<InputView>().AsSingle();
        Container.Bind<InputController>().AsSingle();

        Container.Inject(this);
    }

    [Inject]
    InputModel inputModel;
    [Inject]
    InputView inputView;

    [Test]
    public void PlayerMovement()
    {
        Assert.IsFalse(inputModel.upInputHold);
        inputModel.upInputHold = true;
        Assert.IsTrue(inputModel.upInputHold);

        Assert.IsFalse(inputModel.leftInputHold);
        inputModel.leftInputHold = true;
        Assert.IsTrue(inputModel.leftInputHold);

        Assert.IsFalse(inputModel.rightInputHold);
        inputModel.rightInputHold = true;
        Assert.IsTrue(inputModel.rightInputHold);
    }

    [Test]
    public void MenuMovement()
    {
        Assert.IsFalse(inputModel.upInputDown);
        inputModel.upInputDown = true;
        Assert.IsTrue(inputModel.upInputDown);

        Assert.IsFalse(inputModel.downInputDown);
        inputModel.downInputDown = true;
        Assert.IsTrue(inputModel.downInputDown);
    }

    [Test]
    public void Action()
    {
        Assert.IsFalse(inputModel.actionInputDown);
        inputModel.actionInputDown = true;
        Assert.IsTrue(inputModel.actionInputDown);
    }
}
