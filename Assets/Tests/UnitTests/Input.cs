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
    [Inject]
    InputController inputController;

    [Test]
    public void PlayerMovement()
    {
        Assert.IsFalse(inputView.UpInputHold);
        inputModel.upInputHold = true;
        Assert.IsTrue(inputView.UpInputHold);

        Assert.IsFalse(inputView.LeftInputHold);
        inputModel.leftInputHold = true;
        Assert.IsTrue(inputView.LeftInputHold);

        Assert.IsFalse(inputView.RightInputHold);
        inputModel.rightInputHold = true;
        Assert.IsTrue(inputView.RightInputHold);
    }

    [Test]
    public void MenuMovement()
    {
        Assert.IsFalse(inputView.UpInputDown);
        inputModel.upInputDown = true;
        Assert.IsTrue(inputView.UpInputDown);

        Assert.IsFalse(inputView.DownInputDown);
        inputModel.downInputDown = true;
        Assert.IsTrue(inputView.DownInputDown);
    }

    [Test]
    public void Action()
    {
        Assert.IsFalse(inputView.ActionInputDown);
        inputModel.actionInputDown = true;
        Assert.IsTrue(inputView.ActionInputDown);
    }

    [Test]
    public void InputType()
    {
        inputController.SetInputType(InputModel.Type.Menu);
        Assert.IsTrue(inputView.InputType == InputModel.Type.Menu);

        inputController.SetInputType(InputModel.Type.Player);
        Assert.IsTrue(inputView.InputType == InputModel.Type.Player);
    }

    [Test]
    public void ToggleMenuButton()
    {
        Assert.IsFalse(inputView.ToggleMenuInputDown);
        inputModel.toggleMenuInputDown = true;
        Assert.IsTrue(inputView.ToggleMenuInputDown);
    }
}
