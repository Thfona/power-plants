using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using PowerPlant.Core.Input.Enums;

namespace PowerPlant.Core.Input;

internal class Input(
    InputActions action,
    InputBehaviors behavior,
    InputContext context,
    List<MouseButtons> mouseButtons,
    Keys[] keys,
    List<Buttons> buttons
)
{
    public InputActions Action
    {
        get => action;
    }

    public InputBehaviors Behavior
    {
        get => behavior;
    }

    public InputContext Context
    {
        get => context;
    }

    public List<MouseButtons> MouseButtons
    {
        get => mouseButtons;
    }

    public Keys[] Keys
    {
        get => keys;
    }

    public List<Buttons> Buttons
    {
        get => buttons;
    }
}
