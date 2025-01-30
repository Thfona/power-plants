using Microsoft.Xna.Framework;
using JumpBot.Core.State.Enums;
using JumpBot.Core.Render;

namespace JumpBot.Core.State;

public class StateManager(RenderTargetManager renderTargetManager)
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
        renderTargetManager.SetFullScreen(_isFullScreen);
    }

    public void SwapFullScreen()
    {
        renderTargetManager.SetFullScreen(!_isFullScreen);
        _isFullScreen = !_isFullScreen;
    }

    public void StartGame()
    {
        stateContext = StateContext.Game;
    }

    public void EndGame()
    {
        stateContext = StateContext.Menu;
    }

    public void Update(GameTime gameTime)
    {
        if (IsInGame)
        {
            // TODO: Add in game logic
        }
    }
}
