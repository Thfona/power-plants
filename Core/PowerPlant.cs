using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PowerPlants.Core;

public class PowerPlant(int id, string name, double energyOutput, double cost, Texture2D texture, Rectangle rectangle)
{
    public int Id
    {
        get => id;
    }
    
    public string Name
    {
        get => name;
    }
    
    public double EnergyOutput
    {
        get => energyOutput;
    }

    public double Cost
    {
        get => cost;
    }

    public Texture2D Texture
    {
        get => texture;
    }

    public Rectangle Rectangle
    {
        get => rectangle;
    }
}
