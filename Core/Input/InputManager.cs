using Microsoft.Xna.Framework;
using PowerPlant.Core.Input.Enums;
using PowerPlant.Core.Input.Handlers;
using PowerPlant.Core.State;

namespace PowerPlant.Core.Input;

public class InputManager(Game game, StateManager stateManager)
{
    private readonly InputHandler inputHandler = new(stateManager);

    public void Update()
    {
        inputHandler.HandleInput(InputActions.StartGame, stateManager.StartGame);
        inputHandler.HandleInput(InputActions.ExitGame, game.Exit);
        inputHandler.HandleInput(InputActions.SetFullScreen, stateManager.ToggleFullScreen);
    }
}
