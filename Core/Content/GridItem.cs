using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PowerPlant.Core.Content;

internal class GridItem(Vector2 position, Texture2D texture)
{
    public Vector2 Position
    {
        get => position;
    }

    public Texture2D Texture
    {
        get => texture;
    }
}
