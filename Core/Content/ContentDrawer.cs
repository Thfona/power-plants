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
    private readonly List<GridItem> grid = [];

    private void DrawMenu()
    {
        string title = "Power Plant";
        Vector2 titleTextSize = contentLoader.BigFont.MeasureString(title);
        spriteBatch.DrawString(contentLoader.BigFont, title, new Vector2((gameWidth / 2) - (titleTextSize.X / 2), 50), Color.White);

        string startGameMessage = "Press [Enter] to begin";
        Vector2 startGameTextSize = contentLoader.SmallFont.MeasureString(startGameMessage);
        spriteBatch.DrawString(contentLoader.SmallFont, startGameMessage, new Vector2((gameWidth / 2) - (startGameTextSize.X / 2), 400), Color.White);

        string exitGameMessage = "Press [Esc] to quit";
        Vector2 exitGameTextSize = contentLoader.SmallFont.MeasureString(exitGameMessage);
        spriteBatch.DrawString(contentLoader.SmallFont, exitGameMessage, new Vector2((gameWidth / 2) - (exitGameTextSize.X / 2), 450), Color.White);

        string fullScreenMessage = "Press [F] to toggle fullscreen";
        Vector2 fullScreenTextSize = contentLoader.SmallFont.MeasureString(fullScreenMessage);
        spriteBatch.DrawString(contentLoader.SmallFont, fullScreenMessage, new Vector2((gameWidth / 2) - (fullScreenTextSize.X / 2), 500), Color.White);
    }

    private void BuildGrid()
    {
        int tileSize = 32;

        int gridWidth = 15;
        int gridHeight = 15;

        int gridSizeWidth = gridWidth * tileSize;
        int gridSizeHeight = gridHeight * tileSize;

        float offsetX = (gameWidth - gridSizeWidth) / 2f;
        float offsetY = gameHeight - gridSizeHeight - tileSize;

        for (int row = 0; row < gridHeight; row++)
        {
            for (int col = 0; col < gridWidth; col++)
            {
                Vector2 position = new(col * tileSize + offsetX, row * tileSize + offsetY);

                List<Texture2D> textureOptions = contentLoader.Grasses;
                Texture2D texture = textureOptions[random.Next(textureOptions.Count)];

                GridItem gridItem = new(position, texture);

                grid.Add(gridItem);
            }
        }
    }

    private void DrawGrid()
    {
        grid.ForEach((item) => spriteBatch.Draw(item.Texture, item.Position, Color.White));
    }

    private void DrawGame()
    {
        DrawGrid();
    }

    public void Load()
    {
        BuildGrid();
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
