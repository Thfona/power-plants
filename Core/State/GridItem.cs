using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PowerPlants.Core.State;

public class GridItem(Vector2 position, Texture2D texture)
{
    private PowerPlant _powerPlant;

    public Vector2 Position
    {
        get => position;
    }

    public Texture2D Texture
    {
        get => texture;
    }

    public Rectangle Rectangle
    {
        get => new(new Point((int)position.X, (int)position.Y), new Point(StateManager.GridTileSize));
    }

    public PowerPlant PowerPlant
    {
        get => _powerPlant;
        set => _powerPlant = value;
    }
}
