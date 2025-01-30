using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using JumpBot.Core.Input.Enums;
using JumpBot.Core.State;

namespace JumpBot.Core.Input.Handlers;

internal class InputHandler(StateManager stateManager)
{
    private readonly KeyboardInputHandler keyboardInputHandler = new();
    private readonly GamePadInputHandler gamePadInputHandler = new();
    private readonly List<InputActions> unreleasedActions = [];
    private readonly List<Input> inputs = [
        new Input(InputActions.StartGame, InputBehaviors.Press, InputContext.Menu, [Keys.Enter], [Buttons.A]),
        new Input(InputActions.ExitGame, InputBehaviors.Press, InputContext.Global, [Keys.Escape], [Buttons.Back]),
        new Input(InputActions.SetFullScreen, InputBehaviors.Press, InputContext.Menu, [Keys.F], [Buttons.Start]),
    ];

    private void ReleaseAction(InputActions inputAction)
    {
        bool isActionUnreleased = unreleasedActions.Contains(inputAction);

        if (isActionUnreleased)
        {
            unreleasedActions.Remove(inputAction);
        }
    }

    public void HandleInput(InputActions inputAction, Action handleInputLogic)
    {
        Input input = inputs.Find((i) => i.Action == inputAction);

        if (input.Context == InputContext.Menu && !stateManager.IsInMenu)
        {
            return;
        }

        if (input.Context == InputContext.Game && !stateManager.IsInGame)
        {
            return;
        }

        if (keyboardInputHandler.IsPressingInput(input) || gamePadInputHandler.IsPressingInput(input))
        {
            if (input.Behavior == InputBehaviors.Press)
            {
                bool isActionUnreleased = unreleasedActions.Contains(inputAction);

                if (!isActionUnreleased)
                {
                    unreleasedActions.Add(inputAction);
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
