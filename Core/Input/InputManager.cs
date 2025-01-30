using Microsoft.Xna.Framework;
using JumpBot.Core.Input.Enums;
using JumpBot.Core.Input.Handlers;
using JumpBot.Core.State;

namespace JumpBot.Core.Input;

public class InputManager(Game game, StateManager stateManager)
{
    private readonly InputHandler inputHandler = new(stateManager);

    public void Update()
    {
        inputHandler.HandleInput(InputActions.StartGame, stateManager.SetToInGameContext);
        inputHandler.HandleInput(InputActions.ExitGame, game.Exit);
        inputHandler.HandleInput(InputActions.SetFullScreen, stateManager.ToggleFullScreen);
    }
}
