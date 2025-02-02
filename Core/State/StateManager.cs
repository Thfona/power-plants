using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PowerPlant.Core.State.Enums;
using PowerPlant.Core.Render;

namespace PowerPlant.Core.State;

public class StateManager(Game game, RenderManager renderManager)
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
        game.IsMouseVisible = true;
        renderManager.SetFullScreen(_isFullScreen);
    }

    public void Update(GameTime gameTime)
    {
        // TODO: Add update logic
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

    public static Vector2 GetMousePosition()
    {
        MouseState mouseState = Mouse.GetState();

        return mouseState.Position.ToVector2();
    }
}
