using System.Linq;
using Microsoft.Xna.Framework.Input;
using JumpBot.Core.Input.Interfaces;

namespace JumpBot.Core.Input.Handlers;

internal class KeyboardInputHandler : IInputActionHandler
{
    public bool IsPressingInput(Input input)
    {
        KeyboardState keyboardState = Keyboard.GetState();

        Keys[] pressedKeys = keyboardState.GetPressedKeys();

        return pressedKeys.Intersect(input.Keys).Any();
    }
}
