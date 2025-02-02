using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PowerPlant.Core.Content;

public class ContentLoader(ContentManager contentManager)
{
    private SpriteFont _bigFont;
    private SpriteFont _smallFont;

    public SpriteFont BigFont
    {
        get => _bigFont;
    }

    public SpriteFont SmallFont
    {
        get => _smallFont;
    }

    public void LoadContent()
    {
        _bigFont = contentManager.Load<SpriteFont>("bigFont");
        _smallFont = contentManager.Load<SpriteFont>("smallFont");
    }
}
