using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using JumpBot.Core.State;

namespace JumpBot.Core.Content;

public class ContentDrawer(
    SpriteBatch spriteBatch,
    StateManager stateManager,
    ContentLoader contentLoader,
    int gameWidth
)
{
    private void DrawMenu()
    {
        string startGameMessage = "Press [Enter] or (A) to begin.";
        Vector2 startGameTextSize = contentLoader.BigFont.MeasureString(startGameMessage);
        spriteBatch.DrawString(contentLoader.BigFont, startGameMessage, new Vector2((gameWidth / 2) - (startGameTextSize.X / 2), 200), Color.White);

        string exitGameMessage = "Press [Esc] or (Back) to quit.";
        Vector2 exitGameTextSize = contentLoader.SmallFont.MeasureString(exitGameMessage);
        spriteBatch.DrawString(contentLoader.SmallFont, exitGameMessage, new Vector2((gameWidth / 2) - (exitGameTextSize.X / 2), 300), Color.White);

        string fullScreenMessage = "Press [F] or (Start) to toggle fullscreen.";
        Vector2 fullScreenTextSize = contentLoader.SmallFont.MeasureString(fullScreenMessage);
        spriteBatch.DrawString(contentLoader.SmallFont, fullScreenMessage, new Vector2((gameWidth / 2) - (fullScreenTextSize.X / 2), 400), Color.White);
    }

    public void Draw()
    {
        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        if (stateManager.IsInMenu)
        {
            DrawMenu();
        }

        spriteBatch.End();
    }
}
