using Microsoft.Xna.Framework;
using JumpBot.Core.State.Enums;
using JumpBot.Core.Render;

namespace JumpBot.Core.State;

public class StateManager(RenderManager renderManager)
{
    private bool _isFullScreen = false;
    private StateContext stateContext = StateContext.Menu;

    public bool IsFullScreen
    {
        get => _isFullScreen;
    }

    public bool IsInMenu
    {
        get => stateContext == StateContext.Menu;
    }

    public bool IsInGame
    {
        get => stateContext == StateContext.Game;
    }

    public void Initialize()
    {
        renderManager.SetFullScreen(_isFullScreen);
    }

    public void ToggleFullScreen()
    {
        renderManager.SetFullScreen(!_isFullScreen);
        _isFullScreen = !_isFullScreen;
    }

    public void SetToInGameContext()
    {
        stateContext = StateContext.Game;
    }

    public void SetToInMenuContext()
    {
        stateContext = StateContext.Menu;
    }

    public void Update(GameTime gameTime)
    {
        // TODO: Add update logic
    }
}
