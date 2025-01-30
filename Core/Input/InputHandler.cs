using System;

namespace JumpBot.Core.Input;

public class InputHandler
{
    private readonly KeyboardInputHandler keyboardInputHandler = new();
    private readonly GamePadInputHandler gamePadInputHandler = new();

    public void HandleInput(InputActions inputAction, Action handleInputLogic)
    {
        if (keyboardInputHandler.IsExecutingAction(inputAction) || gamePadInputHandler.IsExecutingAction(inputAction))
        {
            handleInputLogic();
        }
    }
}
