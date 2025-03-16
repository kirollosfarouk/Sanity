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
        GameOver,
        Exit
    }

    public enum MenuTriggers
    {
        Initialized,
        StartGame,
        Pause,
        GameOver,
        Exit
    }
}