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
    private static readonly int menuGridWidth = 25;
    private static readonly int menuGridHeight = 19;
    private static readonly int gameGridWidth = 15;
    private static readonly int gameGridHeight = 15;
    private static readonly int gameGridSizeWidth = gameGridWidth * gridTileSize;
    private static readonly int gameGridSizeHeight = gameGridHeight * gridTileSize;
    private List<GridItem> menuGrid;
    private List<GridItem> gameGrid;

    private void DrawStringWithOutline(SpriteFont spriteFont, string text, Vector2 position, Color color)
    {
        int firstOffset = 2;
        int secondOffset = 4;

        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X - firstOffset, position.Y - firstOffset), Color.Black);
        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X + firstOffset, position.Y + firstOffset), Color.Black);
        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X + firstOffset, position.Y - firstOffset), Color.Black);
        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X - firstOffset, position.Y + firstOffset), Color.Black);

        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X - secondOffset, position.Y + secondOffset), Color.Black);
        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X - secondOffset, position.Y + secondOffset), Color.Black);
        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X - secondOffset, position.Y + secondOffset), Color.Black);
        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X - secondOffset, position.Y + secondOffset), Color.Black);

        spriteBatch.DrawString(spriteFont, text, position, color);
    }

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
        Vector2 offset = new(0, 0);

        List<Texture2D> textureOptions = [
            contentLoader.SolarPlant,
            contentLoader.NaturalGasPlant,
            contentLoader.NuclearPlant,
        ];

        menuGrid = BuildGrid(menuGridWidth, menuGridHeight, offset, textureOptions);
    }

    private void DrawMenu()
    {
        DrawGrid(menuGrid);

        string title = "Power Plant";
        Vector2 titleTextSize = contentLoader.BigFont.MeasureString(title);
        DrawStringWithOutline(contentLoader.BigFont, title, new Vector2((gameWidth / 2) - (titleTextSize.X / 2), 150), Color.White);

        string startGameMessage = "Press [Enter] to play";
        Vector2 startGameTextSize = contentLoader.SmallFont.MeasureString(startGameMessage);
        DrawStringWithOutline(contentLoader.SmallFont, startGameMessage, new Vector2((gameWidth / 2) - (startGameTextSize.X / 2), 350), Color.White);

        string fullScreenMessage = "Press [F] to toggle fullscreen";
        Vector2 fullScreenTextSize = contentLoader.SmallFont.MeasureString(fullScreenMessage);
        DrawStringWithOutline(contentLoader.SmallFont, fullScreenMessage, new Vector2((gameWidth / 2) - (fullScreenTextSize.X / 2), 400), Color.White);

        string exitGameMessage = "Press [Esc] to quit";
        Vector2 exitGameTextSize = contentLoader.SmallFont.MeasureString(exitGameMessage);
        DrawStringWithOutline(contentLoader.SmallFont, exitGameMessage, new Vector2((gameWidth / 2) - (exitGameTextSize.X / 2), 450), Color.White);
    }

    private void BuildGameGrid()
    {
        Vector2 offset = new(gameWidth - gameGridSizeWidth - gridTileSize, gameHeight - gameGridSizeHeight - gridTileSize);

        gameGrid = BuildGrid(gameGridWidth, gameGridHeight, offset, contentLoader.Grasses);
    }

    private void DrawGame()
    {
        spriteBatch.Draw(contentLoader.TopPanel, new Vector2(gridTileSize, 10), Color.White);
        spriteBatch.Draw(contentLoader.SidePanel, new Vector2(gridTileSize, gameHeight - gameGridSizeHeight - gridTileSize), Color.White);

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
