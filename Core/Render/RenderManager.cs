using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PowerPlant.Core.Render;

public class RenderManager(Game game, GraphicsDeviceManager graphicsDeviceManager, int targetWidth, int targetHeight)
{
    private readonly RenderTarget2D renderTarget = new(graphicsDeviceManager.GraphicsDevice, targetWidth, targetHeight);
    private Rectangle destinationRectangle;

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
            graphicsDeviceManager.PreferredBackBufferWidth = targetWidth;
            graphicsDeviceManager.PreferredBackBufferHeight = targetHeight;
            game.Window.IsBorderless = false;
        }

        graphicsDeviceManager.ApplyChanges();
        SetDestinationRectangle();
    }
}
