using Microsoft.Xna.Framework;

namespace JumpBot.Core.Managers;

public class StateManager(RenderTargetManager renderTargetManager)
{
    private bool _isFullScreen = false;
    private bool _isInGame = false;

    public bool IsFullScreen
    {
        get => _isFullScreen;
    }

    public bool IsInGame
    {
        get => _isInGame;
    }

    public void Initialize()
    {
        renderTargetManager.SetFullScreen(_isFullScreen);
    }

    public void SwapFullScreen()
    {
        renderTargetManager.SetFullScreen(!_isFullScreen);
        _isFullScreen = !_isFullScreen;
    }

    public void StartGame()
    {
        _isInGame = true;
    }

    public void EndGame()
    {
        _isInGame = false;
    }

    public void Update(GameTime gameTime)
    {
        if (_isInGame)
        {
            // TODO: Add in game logic
        }
    }
}
