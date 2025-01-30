using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using JumpBot.Core.Input.Interfaces;

namespace JumpBot.Core.Input.Handlers;

internal class GamePadInputHandler : IInputActionHandler
{
    private static List<Buttons> GetPressedButtons()
    {
        GamePadState gamePadState = GamePad.GetState(PlayerIndex.One);

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

    public bool IsPressingInput(Input input)
    {
        List<Buttons> pressedButtons = GetPressedButtons();

        return pressedButtons.Intersect(input.Buttons).Any();
    }
}
