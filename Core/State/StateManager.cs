using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using PowerPlants.Core.Content;
using PowerPlants.Core.Render;
using PowerPlants.Core.State.Enums;

namespace PowerPlants.Core.State;

public class StateManager(Game game, RenderManager renderManager)
{
    private static readonly Random random = new();
    private ContentLoader contentLoader;
    private StateContext stateContext = StateContext.Menu;
    private bool _isFullScreen = false;
    private static readonly int energyGenerationInterval = 5;
    private double energyGenerationTimer = energyGenerationInterval;
    private double _energyOutput = 0;
    private static readonly int moneyMultiplier = 5;
    private double _money = 100;
    private static readonly int _gridTileSize = 32;
    private static readonly int menuGridWidth = 25;
    private static readonly int menuGridHeight = 19;
    private static readonly int gameGridWidth = 15;
    private static readonly int gameGridHeight = 15;
    private static readonly int gameGridSizeWidth = gameGridWidth * _gridTileSize;
    private static readonly int _gameGridSizeHeight = gameGridHeight * _gridTileSize;
    private List<GridItem> _menuGrid;
    private List<GridItem> _gameGrid;

    public static int GridTileSize
    {
        get => _gridTileSize;
    }

    public static int GameGridSizeHeight
    {
        get => _gameGridSizeHeight;
    }

    public List<GridItem> MenuGrid
    {
        get => _menuGrid;
    }

    public List<GridItem> GameGrid
    {
        get => _gameGrid;
        set => _gameGrid = value;
    }

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

    private static List<GridItem> BuildGrid(int gridWidth, int gridHeight, Vector2 offset, List<Texture2D> textureOptions)
    {
        List<GridItem> grid = [];

        for (int row = 0; row < gridHeight; row++)
        {
            for (int col = 0; col < gridWidth; col++)
            {
                Vector2 position = new(col * _gridTileSize + offset.X, row * _gridTileSize + offset.Y);

                Texture2D texture = textureOptions[random.Next(textureOptions.Count)];

                GridItem gridItem = new(position, texture);

                grid.Add(gridItem);
            }
        }

        return grid;
    }

    private void BuildMenuGrid()
    {
        Vector2 offset = new(0, 0);

        List<Texture2D> textureOptions = [
            contentLoader.SolarPlant,
            contentLoader.NaturalGasPlant,
            contentLoader.NuclearPlant,
        ];

        _menuGrid = BuildGrid(menuGridWidth, menuGridHeight, offset, textureOptions);
    }

    private void BuildGameGrid()
    {
        Vector2 offset = new(RenderManager.GameWidth - gameGridSizeWidth - _gridTileSize, RenderManager.GameHeight - _gameGridSizeHeight - _gridTileSize);

        _gameGrid = BuildGrid(gameGridWidth, gameGridHeight, offset, contentLoader.Grasses);
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

        BuildMenuGrid();
        BuildGameGrid();

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
