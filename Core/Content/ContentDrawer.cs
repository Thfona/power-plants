using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PowerPlant.Core.State;

namespace PowerPlant.Core.Content;

public class ContentDrawer(
    SpriteBatch spriteBatch,
    StateManager stateManager,
    ContentLoader contentLoader,
    int gameWidth,
    int gameHeight
)
{
    private static readonly Random random = new();
    private static readonly int gridTileSize = 32;
    private List<GridItem> menuGrid;
    private List<GridItem> gameGrid;

    private static List<GridItem> BuildGrid(int gridWidth, int gridHeight, Vector2 offset, List<Texture2D> textureOptions)
    {
        List<GridItem> grid = [];

        for (int row = 0; row < gridHeight; row++)
        {
            for (int col = 0; col < gridWidth; col++)
            {
                Vector2 position = new(col * gridTileSize + offset.X, row * gridTileSize + offset.Y);

                Texture2D texture = textureOptions[random.Next(textureOptions.Count)];

                GridItem gridItem = new(position, texture);

                grid.Add(gridItem);
            }
        }

        return grid;
    }

    private void DrawGrid(List<GridItem> grid)
    {
        grid.ForEach((item) => spriteBatch.Draw(item.Texture, item.Position, Color.White));
    }

    private void BuildMenuGrid()
    {
        int gridWidth = 25;
        int gridHeight = 19;

        Vector2 offset = new(0, 0);

        menuGrid = BuildGrid(gridWidth, gridHeight, offset, contentLoader.Grasses);
    }

    private void DrawMenu()
    {
        DrawGrid(menuGrid);

        string title = "Power Plant";
        Vector2 titleTextSize = contentLoader.BigFont.MeasureString(title);
        spriteBatch.DrawString(contentLoader.BigFont, title, new Vector2((gameWidth / 2) - (titleTextSize.X / 2), 150), Color.White);

        string startGameMessage = "Press [Enter] to play";
        Vector2 startGameTextSize = contentLoader.SmallFont.MeasureString(startGameMessage);
        spriteBatch.DrawString(contentLoader.SmallFont, startGameMessage, new Vector2((gameWidth / 2) - (startGameTextSize.X / 2), 350), Color.White);

        string fullScreenMessage = "Press [F] to toggle fullscreen";
        Vector2 fullScreenTextSize = contentLoader.SmallFont.MeasureString(fullScreenMessage);
        spriteBatch.DrawString(contentLoader.SmallFont, fullScreenMessage, new Vector2((gameWidth / 2) - (fullScreenTextSize.X / 2), 400), Color.White);

        string exitGameMessage = "Press [Esc] to quit";
        Vector2 exitGameTextSize = contentLoader.SmallFont.MeasureString(exitGameMessage);
        spriteBatch.DrawString(contentLoader.SmallFont, exitGameMessage, new Vector2((gameWidth / 2) - (exitGameTextSize.X / 2), 450), Color.White);
    }

    private void BuildGameGrid()
    {
        int gridWidth = 15;
        int gridHeight = 15;

        int gridSizeWidth = gridWidth * gridTileSize;
        int gridSizeHeight = gridHeight * gridTileSize;

        Vector2 offset = new(gameWidth - gridSizeWidth - gridTileSize, gameHeight - gridSizeHeight - gridTileSize);

        gameGrid = BuildGrid(gridWidth, gridHeight, offset, contentLoader.Grasses);
    }

    private void DrawGame()
    {
        DrawGrid(gameGrid);
    }

    public void Load()
    {
        BuildMenuGrid();
        BuildGameGrid();
    }

    public void Draw()
    {
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        if (stateManager.IsInMenu)
        {
            DrawMenu();
        }

        if (stateManager.IsInGame)
        {
            DrawGame();
        }

        spriteBatch.End();
    }
}
