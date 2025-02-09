﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PowerPlants.Core.Content;
using PowerPlants.Core.Input;
using PowerPlants.Core.Render;
using PowerPlants.Core.State;

namespace PowerPlants;

public class PowerPlantsGame : Game
{
    private readonly GraphicsDeviceManager graphicsDeviceManager;
    private SpriteBatch spriteBatch;

    private RenderManager renderManager;
    private StateManager stateManager;
    private InputManager inputManager;
    private ContentLoader contentLoader;
    private ContentDrawer contentDrawer;

    public PowerPlantsGame()
    {
        graphicsDeviceManager = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
    }

    protected override void Initialize()
    {
        renderManager = new(this, graphicsDeviceManager);
        stateManager = new(this, renderManager);
        inputManager = new(this, stateManager);

        stateManager.Initialize();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        spriteBatch = new SpriteBatch(GraphicsDevice);
        contentLoader = new ContentLoader(Content);
        contentDrawer = new(spriteBatch, stateManager, contentLoader);

        contentLoader.LoadContent();
        stateManager.PrepareContent(contentLoader);
    }

    protected override void Update(GameTime gameTime)
    {
        inputManager.Update();
        stateManager.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        renderManager.Load();

        contentDrawer.Draw();

        renderManager.Draw(spriteBatch);

        base.Draw(gameTime);
    }
}
