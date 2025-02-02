using Microsoft.Xna.Framework.Graphics;

namespace PowerPlants.Core;

public class PowerPlant(double energyOutput, double cost, Texture2D texture)
{
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
}
