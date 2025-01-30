using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;

namespace JumpBot.Core.Input;

internal class KeyboardInputHandler : IInputActionHandler
{
    private KeyboardState keyboardState = Keyboard.GetState();
    private readonly Dictionary<InputActions, Keys[]> inputActionsMapping = new()
    {
        { InputActions.StartGame, [Keys.Enter] },
        { InputActions.ExitGame, [Keys.Escape] },
        { InputActions.SetFullScreen, [Keys.F] },
    };

    public bool IsExecutingAction(InputActions inputAction) {
        Keys[] inputActionKeys = inputActionsMapping[inputAction];

        Keys[] pressedKeys = keyboardState.GetPressedKeys();

        return pressedKeys.Intersect(inputActionKeys).Any();
    }
}
