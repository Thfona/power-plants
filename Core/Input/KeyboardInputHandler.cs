using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using JumpBot.Core.Input.Enums;
using JumpBot.Core.Input.Interfaces;

namespace JumpBot.Core.Input;

internal class KeyboardInputHandler : IInputActionHandler
{
    private readonly Dictionary<InputActions, Keys[]> inputActionsMapping = new()
    {
        { InputActions.StartGame, [Keys.Enter] },
        { InputActions.ExitGame, [Keys.Escape] },
        { InputActions.SetFullScreen, [Keys.F] },
    };

    public bool IsExecutingAction(InputActions inputAction)
    {
        KeyboardState keyboardState = Keyboard.GetState();

        Keys[] inputActionKeys = inputActionsMapping[inputAction];

        Keys[] pressedKeys = keyboardState.GetPressedKeys();

        return pressedKeys.Intersect(inputActionKeys).Any();
    }
}
