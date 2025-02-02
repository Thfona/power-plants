using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PowerPlants.Core.Render;

public class RenderManager(Game game, GraphicsDeviceManager graphicsDeviceManager)
{
    private static readonly int _gameWidth = 800;
    private static readonly int _gameHeight = 600;
    private readonly RenderTarget2D renderTarget = new(graphicsDeviceManager.GraphicsDevice, _gameWidth, _gameHeight);
    private Rectangle destinationRectangle;

    public static int GameWidth
    {
        get => _gameWidth;
    }

    public static int GameHeight
    {
        get => _gameHeight;
    }

    private void SetDestinationRectangle()
    {
        Rectangle screenSize = graphicsDeviceManager.GraphicsDevice.PresentationParameters.Bounds;

        float scaleX = (float)screenSize.Width / renderTarget.Width;
        float scaleY = (float)screenSize.Height / renderTarget.Height;
        float scale = Math.Min(scaleX, scaleY);

        int newWidth = (int)(renderTarget.Width * scale);
        int newHeight = (int)(renderTarget.Height * scale);

        int posX = (screenSize.Width - newWidth) / 2;
        int posY = (screenSize.Height - newHeight) / 2;

        destinationRectangle = new(posX, posY, newWidth, newHeight);
    }

    public void Load()
    {
        graphicsDeviceManager.GraphicsDevice.SetRenderTarget(renderTarget);
        graphicsDeviceManager.GraphicsDevice.Clear(Color.CornflowerBlue);
    }

    public void Draw(SpriteBatch spriteBatch)
    {
        graphicsDeviceManager.GraphicsDevice.SetRenderTarget(null);
        graphicsDeviceManager.GraphicsDevice.Clear(Color.Black);

        spriteBatch.Begin();
        spriteBatch.Draw(renderTarget, destinationRectangle, Color.White);
        spriteBatch.End();
    }

    public void SetFullScreen(bool enable)
    {
        if (enable)
        {
            graphicsDeviceManager.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphicsDeviceManager.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            game.Window.IsBorderless = true;
        }
        else
        {
            graphicsDeviceManager.PreferredBackBufferWidth = _gameWidth;
            graphicsDeviceManager.PreferredBackBufferHeight = _gameHeight;
            game.Window.IsBorderless = false;
        }

        graphicsDeviceManager.ApplyChanges();
        SetDestinationRectangle();
    }
}
