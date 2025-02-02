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
    private static readonly int energyGenerationInterval = 3;
    private double energyGenerationTimer = energyGenerationInterval;
    private double _energyOutput = 0;
    private static readonly int moneyMultiplier = 5;
    private double _money = 100;
    private static readonly int sidePanelStartX = 36;
    private static readonly int sidePanelSizeX = 216;
    private static readonly int sidePanelSizeY = 110;
    private static readonly int _gridTileSize = 32;
    private static readonly int menuGridWidth = 25;
    private static readonly int menuGridHeight = 19;
    private static readonly int gameGridWidth = 15;
    private static readonly int gameGridHeight = 15;
    private static readonly int gameGridSizeWidth = gameGridWidth * _gridTileSize;
    private static readonly int _gameGridSizeHeight = gameGridHeight * _gridTileSize;
    private List<GridItem> _menuGrid;
    private List<GridItem> _gameGrid;
    private List<PowerPlant> _powerPlantList;
    private PowerPlant _selectedPowerPlant;

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

    public List<PowerPlant> PowerPlantList
    {
        get => _powerPlantList;
    }

    public PowerPlant SelectedPowerPlant
    {
        get => _selectedPowerPlant;
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
            contentLoader.WindPlant,
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

        _powerPlantList = [
            new(1, "Solar", 5, 100, contentLoader.SolarPlant, new Rectangle(new Point(sidePanelStartX, 90), new Point(sidePanelSizeX, sidePanelSizeY))),
            new(2, "Wind", 10, 1000, contentLoader.WindPlant, new Rectangle(new Point(sidePanelStartX, 212), new Point(sidePanelSizeX, sidePanelSizeY))),
            new(3, "Nat. Gas", 15, 3000, contentLoader.NaturalGasPlant, new Rectangle(new Point(sidePanelStartX, 332), new Point(sidePanelSizeX, sidePanelSizeY))),
            new(4, "Nuclear", 30, 5000, contentLoader.NuclearPlant, new Rectangle(new Point(sidePanelStartX, 452), new Point(sidePanelSizeX, sidePanelSizeY))),
        ];

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

    public void HandleLeftMouseClick()
    {
        Vector2 mousePosition = GetMousePosition();
        
        // Side panel
        _powerPlantList.ForEach((powerPlant) => {
            bool hasClickedSidePanelItem = powerPlant.Rectangle.Contains(mousePosition);

            if (hasClickedSidePanelItem)
            {
                if (powerPlant.Cost <= _money && _selectedPowerPlant == null)
                {
                    _selectedPowerPlant = powerPlant;
                    _money -= powerPlant.Cost;

                    AudioPlayer.PlaySoundEffect(contentLoader.PickSfx);
                } else {
                    AudioPlayer.PlaySoundEffect(contentLoader.FailSfx);
                }
            }
        });

        // Game grid
        _gameGrid.ForEach((item) => {
            bool hasClickedGameGridItem = item.Rectangle.Contains(mousePosition);

            if (hasClickedGameGridItem)
            {
                if (_selectedPowerPlant != null)
                {
                    if (item.PowerPlant != null)
                    {
                        _energyOutput -= item.PowerPlant.EnergyOutput;
                    }
                    
                    item.PowerPlant = _selectedPowerPlant;
                    _energyOutput += _selectedPowerPlant.EnergyOutput;

                    _selectedPowerPlant = null;

                    AudioPlayer.PlaySoundEffect(contentLoader.PickSfx);
                } else {
                    AudioPlayer.PlaySoundEffect(contentLoader.FailSfx);
                }
            }
        });
    }
}
