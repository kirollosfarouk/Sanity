using System.Threading.Tasks;
using Eflatun.SceneReference;
using Stateless;
using UI.Menu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuView : MonoBehaviour, InputSystem_Actions.IMenuActions
{
    public SceneReference GameScene;
    public SceneReference EmptyScene;


    public GameObject MainMenuRoot;
    public GameObject IngameMenuRoot;
    public GameObject LoadingRoot;
    public EventSystem EventSystem;

    private readonly StateMachine<MenuStates, MenuTriggers> _stateMachine = new(MenuStates.StartUp);
    private InputSystem_Actions _input;

    public MainMenuView()
    {
        _stateMachine.Configure(MenuStates.StartUp)
            .Permit(MenuTriggers.Initialized, MenuStates.MainMenu);

        _stateMachine.Configure(MenuStates.MainMenu)
            .Permit(MenuTriggers.StartGame, MenuStates.Ingame)
            .Permit(MenuTriggers.Exit, MenuStates.Exit)
            .OnEntry(() => MainMenuRoot?.SetActive(true))
            .OnExit(() => MainMenuRoot?.SetActive(false))
            .OnEntryAsync(LoadEmptyScene);

        _stateMachine.Configure(MenuStates.Exit)
            .OnEntry(Quit);

        _stateMachine.Configure(MenuStates.Ingame)
            .Permit(MenuTriggers.Pause, MenuStates.PauseMenu)
            .Permit(MenuTriggers.GameOver, MenuStates.GameOver)
            .OnEntryFromAsync(MenuTriggers.StartGame, LoadIngame);

        _stateMachine.Configure(MenuStates.PauseMenu)
            .Permit(MenuTriggers.Pause, MenuStates.Ingame)
            .Permit(MenuTriggers.Exit, MenuStates.MainMenu)
            .OnEntry(PauseTransition)
            .OnExit(ResumeTransition);

        _stateMachine.Configure(MenuStates.GameOver)
            .Permit(MenuTriggers.Exit, MenuStates.MainMenu);
    }

    public void Awake()
    {
        _input = new InputSystem_Actions();
        _input.Menu.AddCallbacks(this);
        _input.Menu.Enable();

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(EventSystem.gameObject);
        DontDestroyOnLoad(MainMenuRoot);
        DontDestroyOnLoad(IngameMenuRoot);
        DontDestroyOnLoad(LoadingRoot);

        MainMenuRoot.SetActive(false);
        IngameMenuRoot.SetActive(false);
        LoadingRoot.SetActive(false);

        _stateMachine.FireAsync(MenuTriggers.Initialized);
    }

    public void StartGame()
    {
        _stateMachine.FireAsync(MenuTriggers.StartGame);
    }

    public void Exit()
    {
        _stateMachine.FireAsync(MenuTriggers.Exit);
    }

    private void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private async Task LoadIngame()
    {
        LoadingRoot.SetActive(true);
        await SceneManager.LoadSceneAsync(GameScene.BuildIndex);
        LoadingRoot.SetActive(false);
    }

    private async Task LoadEmptyScene()
    {
        await SceneManager.LoadSceneAsync(EmptyScene.BuildIndex);
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
            TogglePause();
    }

    public void TogglePause()
    {
        _stateMachine.FireAsync(MenuTriggers.Pause);
    }

    private void PauseTransition()
    {
        _input.Player.Disable();
        Time.timeScale = 0;
        IngameMenuRoot?.SetActive(true);
    }

    private void ResumeTransition()
    {
        IngameMenuRoot?.SetActive(false);
        Time.timeScale = 1;
        _input.Player.Enable();
    }
}