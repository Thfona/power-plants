using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace JumpBot.Core.Input;

internal class GamePadInputHandler : IInputActionHandler
{
    private GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);
    private readonly Dictionary<InputActions, List<Buttons>> inputActionsMapping = new()
    {
        { InputActions.StartGame, [Buttons.A] },
        { InputActions.ExitGame, [Buttons.Back] },
        { InputActions.SetFullScreen, [Buttons.Start] },
    };

    private List<Buttons> GetPressedButtons()
    {
        List<Buttons> pressedButtons = [];

        foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
        {
            if (gamePadState.IsButtonDown(button))
            {
                pressedButtons.Add(button);
            }
        }

        return pressedButtons;
    }

    public bool IsExecutingAction(InputActions inputAction) {
        List<Buttons> inputActionButtons = inputActionsMapping[inputAction];

        List<Buttons> pressedButtons = GetPressedButtons();

        return pressedButtons.Intersect(inputActionButtons).Any();
    }
}
