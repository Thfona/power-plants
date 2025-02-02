using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PowerPlant.Core.State;

namespace PowerPlant.Core.Content;

public class ContentDrawer(
    SpriteBatch spriteBatch,
    StateManager stateManager,
    ContentLoader contentLoader,
    int gameWidth
)
{
    private void DrawMenu()
    {
        string title = "Power Plant";
        Vector2 titleTextSize = contentLoader.BigFont.MeasureString(title);
        spriteBatch.DrawString(contentLoader.BigFont, title, new Vector2((gameWidth / 2) - (titleTextSize.X / 2), 100), Color.White);

        string startGameMessage = "Press [Enter] to begin";
        Vector2 startGameTextSize = contentLoader.SmallFont.MeasureString(startGameMessage);
        spriteBatch.DrawString(contentLoader.SmallFont, startGameMessage, new Vector2((gameWidth / 2) - (startGameTextSize.X / 2), 700), Color.White);

        string exitGameMessage = "Press [Esc] to quit";
        Vector2 exitGameTextSize = contentLoader.SmallFont.MeasureString(exitGameMessage);
        spriteBatch.DrawString(contentLoader.SmallFont, exitGameMessage, new Vector2((gameWidth / 2) - (exitGameTextSize.X / 2), 750), Color.White);

        string fullScreenMessage = "Press [F] to toggle fullscreen";
        Vector2 fullScreenTextSize = contentLoader.SmallFont.MeasureString(fullScreenMessage);
        spriteBatch.DrawString(contentLoader.SmallFont, fullScreenMessage, new Vector2((gameWidth / 2) - (fullScreenTextSize.X / 2), 800), Color.White);
    }

    private void DrawGame()
    {
        // TODO: Game content
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
