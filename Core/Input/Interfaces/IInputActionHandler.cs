using JumpBot.Core.Input.Enums;

namespace JumpBot.Core.Input.Interfaces;

internal interface IInputActionHandler
{
    public bool IsExecutingAction(InputActions inputAction);
}
