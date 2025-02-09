using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PowerPlants.Core.Render;
using PowerPlants.Core.State;

namespace PowerPlants.Core.Content;

public class ContentDrawer(SpriteBatch spriteBatch, StateManager stateManager, ContentLoader contentLoader)
{
    private void BeginSpriteBatch()
    {
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);
    }

    private void DrawStringWithOutline(SpriteFont spriteFont, string text, Vector2 position, Color color)
    {
        int firstOffset = 2;
        int secondOffset = 4;

        // First offset, outline
        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X - firstOffset, position.Y - firstOffset), Color.Black);
        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X + firstOffset, position.Y + firstOffset), Color.Black);
        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X + firstOffset, position.Y - firstOffset), Color.Black);
        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X - firstOffset, position.Y + firstOffset), Color.Black);

        // Second offset, shadow
        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X - secondOffset, position.Y + secondOffset), Color.Black);
        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X - secondOffset, position.Y + secondOffset), Color.Black);
        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X - secondOffset, position.Y + secondOffset), Color.Black);
        spriteBatch.DrawString(spriteFont, text, new Vector2(position.X - secondOffset, position.Y + secondOffset), Color.Black);

        spriteBatch.DrawString(spriteFont, text, position, color);
    }

    private void DrawGrid(List<GridItem> grid, Color color)
    {
        grid.ForEach((item) => spriteBatch.Draw(item.PowerPlant?.Texture ?? item.Texture, item.Position, color));
    }

    private void DrawMenu()
    {
        // Title
        string title = "Power Plants";
        Vector2 titleTextSize = contentLoader.BigFont.MeasureString(title);
        DrawStringWithOutline(contentLoader.BigFont, title, new Vector2((RenderManager.GameWidth / 2) - (titleTextSize.X / 2), 150), Color.White);

        // Play
        string startGameMessage = "Press [Enter] to play";
        Vector2 startGameTextSize = contentLoader.SmallFont.MeasureString(startGameMessage);
        DrawStringWithOutline(contentLoader.SmallFont, startGameMessage, new Vector2((RenderManager.GameWidth / 2) - (startGameTextSize.X / 2), 350), Color.White);

        // Fullscreen
        string fullScreenMessage = "Press [F] to toggle fullscreen";
        Vector2 fullScreenTextSize = contentLoader.SmallFont.MeasureString(fullScreenMessage);
        DrawStringWithOutline(contentLoader.SmallFont, fullScreenMessage, new Vector2((RenderManager.GameWidth / 2) - (fullScreenTextSize.X / 2), 400), Color.White);

        // Quit
        string exitGameMessage = "Press [Esc] to quit";
        Vector2 exitGameTextSize = contentLoader.SmallFont.MeasureString(exitGameMessage);
        DrawStringWithOutline(contentLoader.SmallFont, exitGameMessage, new Vector2((RenderManager.GameWidth / 2) - (exitGameTextSize.X / 2), 450), Color.White);
    }

    private void DrawGame()
    {
        int gridTileSize = StateManager.GridTileSize;

        // Panels
        Vector2 topPanelPosition = new(gridTileSize, 10);
        Vector2 sidePanelPosition = new(gridTileSize, RenderManager.GameHeight - StateManager.GameGridSizeHeight - gridTileSize);

        spriteBatch.Draw(contentLoader.TopPanel, topPanelPosition, Color.White);
        spriteBatch.Draw(contentLoader.SidePanel, sidePanelPosition, Color.White);

        // Top panel content
        string moneyText = "Money: " + stateManager.Money;
        spriteBatch.DrawString(contentLoader.SmallFont, moneyText, new Vector2(50, 28), Color.White);

        string energyOutputText = "Energy: " + stateManager.EnergyOutput;
        Vector2 energyOutputTextSize = contentLoader.SmallFont.MeasureString(energyOutputText);
        spriteBatch.DrawString(contentLoader.SmallFont, energyOutputText, new Vector2(RenderManager.GameWidth - energyOutputTextSize.X - 48, 28), Color.White);

        // Side panel content
        stateManager.PowerPlantList.ForEach((plant) => {
            spriteBatch.DrawString(contentLoader.SmallFont, plant.Name, new Vector2(50, plant.Id * 115), Color.White);
            spriteBatch.DrawString(contentLoader.SmallFont, "$" + plant.Cost, new Vector2(50, (plant.Id * 115) + 50), Color.White);
        });

        // Grid
        DrawGrid(stateManager.GameGrid, Color.White);

        // Cursor
        if (stateManager.SelectedPowerPlant != null)
        {
            Vector2 mousePosition = stateManager.GetMousePosition();

            spriteBatch.Draw(
                stateManager.SelectedPowerPlant.Texture,
                new Vector2(mousePosition.X - (gridTileSize / 2),
                mousePosition.Y - (gridTileSize / 2)),
                Color.White
            );
        }
    }

    public void Draw()
    {
        BeginSpriteBatch();

        // Background Grid
        DrawGrid(stateManager.BackgroundGrid, new Color(Color.Green, 0.3f));

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
