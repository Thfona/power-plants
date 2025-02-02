using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PowerPlant.Core.Content;

public class ContentLoader(ContentManager contentManager)
{
    private SpriteFont _bigFont;
    private SpriteFont _smallFont;
    private SoundEffect _startSfx;
    private SoundEffect _pickSfx;
    private SoundEffect _failSfx;
    private SoundEffect _powerPlantThemeSong;
    private Texture2D _topPanel;
    private Texture2D _sidePanel;
    private Texture2D _solarPlant;
    private Texture2D _naturalGasPlant;
    private Texture2D _nuclearPlant;
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

    public SoundEffect StartSfx
    {
        get => _startSfx;
    }

    public SoundEffect PickSfx
    {
        get => _pickSfx;
    }

    public SoundEffect FailSfx
    {
        get => _failSfx;
    }

    public SoundEffect PowerPlantThemeSong
    {
        get => _powerPlantThemeSong;
    }

    public Texture2D TopPanel
    {
        get => _topPanel;
    }

    public Texture2D SidePanel
    {
        get => _sidePanel;
    }

    public Texture2D SolarPlant
    {
        get => _solarPlant;
    }

    public Texture2D NaturalGasPlant
    {
        get => _naturalGasPlant;
    }

    public Texture2D NuclearPlant
    {
        get => _nuclearPlant;
    }

    public List<Texture2D> Grasses
    {
        get => _grasses;
    }

    public void LoadContent()
    {
        _bigFont = contentManager.Load<SpriteFont>("fonts/bigFont");
        _smallFont = contentManager.Load<SpriteFont>("fonts/smallFont");

        _startSfx = contentManager.Load<SoundEffect>("audio/start");
        _pickSfx = contentManager.Load<SoundEffect>("audio/pick");
        _failSfx = contentManager.Load<SoundEffect>("audio/fail");

        _powerPlantThemeSong = contentManager.Load<SoundEffect>("audio/powerplant");

        _topPanel = contentManager.Load<Texture2D>("textures/topPanel");
        _sidePanel = contentManager.Load<Texture2D>("textures/sidePanel");
        _solarPlant = contentManager.Load<Texture2D>("textures/solarPlant");
        _naturalGasPlant = contentManager.Load<Texture2D>("textures/naturalGasPlant");
        _nuclearPlant = contentManager.Load<Texture2D>("textures/nuclearPlant");
        grass1 = contentManager.Load<Texture2D>("textures/grass1");
        grass2 = contentManager.Load<Texture2D>("textures/grass2");
        grass3 = contentManager.Load<Texture2D>("textures/grass3");
        grass4 = contentManager.Load<Texture2D>("textures/grass4");
        grass5 = contentManager.Load<Texture2D>("textures/grass5");
        grass6 = contentManager.Load<Texture2D>("textures/grass6");
        grass7 = contentManager.Load<Texture2D>("textures/grass7");
        grass8 = contentManager.Load<Texture2D>("textures/grass8");
        grass9 = contentManager.Load<Texture2D>("textures/grass9");
        grass10 = contentManager.Load<Texture2D>("textures/grass10");
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
