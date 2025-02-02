using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PowerPlant.Core.Content;
using PowerPlant.Core.Render;
using PowerPlant.Core.State.Enums;

namespace PowerPlant.Core.State;

public class StateManager(Game game, RenderManager renderManager)
{
    private ContentLoader contentLoader;
    private StateContext stateContext = StateContext.Menu;
    private bool _isFullScreen = false;

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

    public void PrepareContent(ContentLoader contentLoader)
    {
        this.contentLoader = contentLoader;
        AudioPlayer.PlayMusic(this.contentLoader.PowerPlantThemeSong, true);
    }

    public void ToggleFullScreen()
    {
        renderManager.SetFullScreen(!_isFullScreen);
        _isFullScreen = !_isFullScreen;
    }

    public void StartGame()
    {
        stateContext = StateContext.Game;
        AudioPlayer.PlaySoundEffect(contentLoader.StartSfx);
    }

    public void EndGame()
    {
        stateContext = StateContext.Menu;
    }

    public static Vector2 GetMousePosition()
    {
        MouseState mouseState = Mouse.GetState();

        return mouseState.Position.ToVector2();
    }
}
