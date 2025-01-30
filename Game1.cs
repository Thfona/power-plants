using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using JumpBot.Core.Input;
using JumpBot.Core.Input.Enums;
using JumpBot.Core.Managers;

namespace JumpBot;

public class Game1 : Game
{
    private readonly GraphicsDeviceManager graphicsDeviceManager;
    private SpriteBatch spriteBatch;

    private RenderTargetManager renderTargetManager;
    private StateManager stateManager;
    private InputHandler inputHandler;

    private static readonly int gameWidth = 1920;
    private static readonly int gameHeight = 1080;

    private SpriteFont bigFont;
    private SpriteFont smallFont;

    public Game1()
    {
        graphicsDeviceManager = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        renderTargetManager = new(this, graphicsDeviceManager, gameWidth, gameHeight);
        stateManager = new(renderTargetManager);
        inputHandler = new(stateManager);

        stateManager.Initialize();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);

        bigFont = Content.Load<SpriteFont>("bigFont");
        smallFont = Content.Load<SpriteFont>("smallFont");
    }

    protected override void Update(GameTime gameTime)
    {
        inputHandler.HandleInput(InputActions.StartGame, stateManager.StartGame);
        inputHandler.HandleInput(InputActions.ExitGame, Exit);
        inputHandler.HandleInput(InputActions.SetFullScreen, stateManager.SwapFullScreen);

        stateManager.Update(gameTime);

        if (stateManager.IsInGame)
        {
            // TODO: Add your update logic here
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        renderTargetManager.Activate();

        spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp);

        if (!stateManager.IsInGame)
        {
            string startGameMessage = "Press [Enter] or (A) to begin.";
            Vector2 startGameTextSize = bigFont.MeasureString(startGameMessage);
            spriteBatch.DrawString(bigFont, startGameMessage, new Vector2((gameWidth / 2) - (startGameTextSize.X / 2), 200), Color.White);

            string exitGameMessage = "Press [Esc] or (Back) to quit.";
            Vector2 exitGameTextSize = smallFont.MeasureString(exitGameMessage);
            spriteBatch.DrawString(smallFont, exitGameMessage, new Vector2((gameWidth / 2) - (exitGameTextSize.X / 2), 300), Color.White);

            string fullScreenMessage = "Press [F] or (Start) to enable/disable fullscreen.";
            Vector2 fullScreenTextSize = smallFont.MeasureString(fullScreenMessage);
            spriteBatch.DrawString(smallFont, fullScreenMessage, new Vector2((gameWidth / 2) - (fullScreenTextSize.X / 2), 400), Color.White);
        }

        spriteBatch.End();

        renderTargetManager.Draw(spriteBatch);

        base.Draw(gameTime);
    }
}
