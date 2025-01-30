using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using JumpBot.Core.Content;
using JumpBot.Core.Input;
using JumpBot.Core.Render;
using JumpBot.Core.State;

namespace JumpBot;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager graphicsDeviceManager;
    private SpriteBatch spriteBatch;

    private RenderManager renderManager;
    private StateManager stateManager;
    private InputManager inputManager;
    private ContentLoader contentLoader;

    private static readonly int gameWidth = 1920;
    private static readonly int gameHeight = 1080;

    public Game1()
    {
        graphicsDeviceManager = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        renderManager = new(this, graphicsDeviceManager, gameWidth, gameHeight);
        stateManager = new(this, renderManager);
        inputManager = new(this, stateManager);

        stateManager.Initialize();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        contentLoader = new ContentLoader(Content);

        contentLoader.LoadContent();
    }

    protected override void Update(GameTime gameTime)
    {
        inputManager.Update();
        stateManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        renderManager.Activate();

        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        if (stateManager.IsInMenu)
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

        spriteBatch.End();

        renderManager.Draw(spriteBatch);

        base.Draw(gameTime);
    }
}
