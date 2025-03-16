namespace UI.Menu
{
    public enum MenuStates
    {
        StartUp,
        MainMenu,
        LoadMainMenu,
        Ingame,
        LoadIngame,
        PauseMenu,
        GameWon,
        GameLost,
        Exit
    }

    public enum MenuTriggers
    {
        Initialized,
        StartGame,
        Pause,
        GameWon,
        GameLost,
        Exit
    }
}