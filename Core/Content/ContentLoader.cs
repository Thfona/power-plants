using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PowerPlant.Core.Content;

public class ContentLoader(ContentManager contentManager)
{
    private SpriteFont _bigFont;
    private SpriteFont _smallFont;
    private Texture2D grass1;
    private Texture2D grass2;
    private Texture2D grass3;
    private Texture2D grass4;
    private Texture2D grass5;
    private Texture2D grass6;
    private Texture2D grass7;
    private Texture2D grass8;
    private Texture2D grass9;
    private Texture2D grass10;
    private List<Texture2D> _grasses;

    public SpriteFont BigFont
    {
        get => _bigFont;
    }

    public SpriteFont SmallFont
    {
        get => _smallFont;
    }

    public List<Texture2D> Grasses
    {
        get => _grasses;
    }

    public void LoadContent()
    {
        _bigFont = contentManager.Load<SpriteFont>("bigFont");
        _smallFont = contentManager.Load<SpriteFont>("smallFont");
        grass1 = contentManager.Load<Texture2D>("grass1");
        grass2 = contentManager.Load<Texture2D>("grass2");
        grass3 = contentManager.Load<Texture2D>("grass3");
        grass4 = contentManager.Load<Texture2D>("grass4");
        grass5 = contentManager.Load<Texture2D>("grass5");
        grass6 = contentManager.Load<Texture2D>("grass6");
        grass7 = contentManager.Load<Texture2D>("grass7");
        grass8 = contentManager.Load<Texture2D>("grass8");
        grass9 = contentManager.Load<Texture2D>("grass9");
        grass10 = contentManager.Load<Texture2D>("grass10");
        _grasses = [
            grass1,
            grass2,
            grass3,
            grass4,
            grass5,
            grass6,
            grass7,
            grass8,
            grass9,
            grass10,
        ];
    }
}
