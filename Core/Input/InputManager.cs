using Microsoft.Xna.Framework;
using PowerPlants.Core.Input.Enums;
using PowerPlants.Core.Input.Handlers;
using PowerPlants.Core.State;

namespace PowerPlants.Core.Input;

public class InputManager(Game game, StateManager stateManager)
{
    private readonly InputHandler inputHandler = new(game, stateManager);

    public void Update()
    {
        inputHandler.HandleInput(InputActions.StartGame, stateManager.StartGame);
        inputHandler.HandleInput(InputActions.ExitGame, game.Exit);
        inputHandler.HandleInput(InputActions.SetFullScreen, stateManager.ToggleFullScreen);
        inputHandler.HandleInput(InputActions.LeftClick, stateManager.HandleLeftMouseClick);
    }
}
