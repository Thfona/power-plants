using System;
using System.Collections.Generic;
using JumpBot.Core.Input.Enums;
using JumpBot.Core.Managers;

namespace JumpBot.Core.Input;

public class InputHandler(StateManager stateManager)
{
    private readonly KeyboardInputHandler keyboardInputHandler = new();
    private readonly GamePadInputHandler gamePadInputHandler = new();
    private readonly Dictionary<InputActions, InputBehaviors> inputActionsBehaviorMapping = new()
    {
        { InputActions.StartGame, InputBehaviors.Press },
        { InputActions.ExitGame, InputBehaviors.Press },
        { InputActions.SetFullScreen, InputBehaviors.Press },
    };

    private void ReleaseAction(InputActions inputAction)
    {
        bool isActionUnreleased = stateManager.UnreleasedActions.Contains(inputAction);

        if (isActionUnreleased)
        {
            stateManager.UnreleasedActions.Remove(inputAction);
        }
    }

    public void HandleInput(InputActions inputAction, Action handleInputLogic)
    {
        if (keyboardInputHandler.IsExecutingAction(inputAction) || gamePadInputHandler.IsExecutingAction(inputAction))
        {
            InputBehaviors inputBehavior = inputActionsBehaviorMapping[inputAction];
            
            if (inputBehavior == InputBehaviors.Press)
            {
                bool isActionUnreleased = stateManager.UnreleasedActions.Contains(inputAction);

                if (!isActionUnreleased)
                {
                    stateManager.UnreleasedActions.Add(inputAction);
                    handleInputLogic();
                }
            }
            else {
                handleInputLogic();
            }
        }
        else
        {
            ReleaseAction(inputAction);
        }
    }
}
