using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PowerPlants.Core.Content;
using PowerPlants.Core.Render;
using PowerPlants.Core.State.Enums;

namespace PowerPlants.Core.State;

public class StateManager(Game game, RenderManager renderManager)
{
    private ContentLoader contentLoader;
    private StateContext stateContext = StateContext.Menu;
    private bool _isFullScreen = false;
    private static readonly int energyGenerationInterval = 5;
    private double energyGenerationTimer = energyGenerationInterval;
    private double _energyOutput = 0;
    private static readonly int moneyMultiplier = 5;
    private double _money = 100;

    public bool IsInMenu
    {
        get => stateContext == StateContext.Menu;
    }

    public bool IsInGame
    {
        get => stateContext == StateContext.Game;
    }

    public bool IsFullScreen
    {
        get => _isFullScreen;
    }

    public double EnergyOutput
    {
        get => _energyOutput;
    }

    public double Money
    {
        get => _money;
    }

    public void Initialize()
    {
        game.IsMouseVisible = true;
        renderManager.SetFullScreen(_isFullScreen);
    }

    public void Update(GameTime gameTime)
    {
        if (IsInGame)
        {
            energyGenerationTimer -= gameTime.ElapsedGameTime.TotalSeconds;

            if (energyGenerationTimer <= 0)
            {
                _money += _energyOutput * moneyMultiplier;

                energyGenerationTimer = energyGenerationInterval;
            }
        }
    }

    public void PrepareContent(ContentLoader contentLoader)
    {
        this.contentLoader = contentLoader;
        AudioPlayer.PlayMusic(this.contentLoader.PowerPlantsThemeSong, true);
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
