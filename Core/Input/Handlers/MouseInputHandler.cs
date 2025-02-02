using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PowerPlants.Core.Input.Enums;
using PowerPlants.Core.Input.Interfaces;

namespace PowerPlants.Core.Input.Handlers;

internal class MouseInputHandler(Game game) : IInputActionHandler
{
    private static bool IsPressed(ButtonState buttonState) {
        return buttonState == ButtonState.Pressed;
    }

    public bool IsPressingInput(Input input)
    {
        MouseState mouseState = Mouse.GetState();

        List<MouseButtons> pressedMouseButtons = [];

        if (IsPressed(mouseState.LeftButton))
        {
            pressedMouseButtons.Add(MouseButtons.LeftButton);
        }

        if (IsPressed(mouseState.MiddleButton))
        {
            pressedMouseButtons.Add(MouseButtons.MiddleButton);
        }

        if (IsPressed(mouseState.RightButton))
        {
            pressedMouseButtons.Add(MouseButtons.RightButton);
        }

        return pressedMouseButtons.Intersect(input.MouseButtons).Any() && game.IsActive;
    }
}
