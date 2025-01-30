namespace JumpBot.Core.Input;

internal interface IInputActionHandler
{
    public bool IsExecutingAction(InputActions inputAction);
}
