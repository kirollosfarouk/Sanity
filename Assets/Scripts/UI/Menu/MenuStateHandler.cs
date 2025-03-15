using System.Threading.Tasks;
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
    public GameObject MainMenuRoot;
    public GameObject IngameMenuRoot;
    public GameObject LoadingRoot;
    public EventSystem EventSystem;

    private StateMachine<MenuStates, MenuTriggers> _stateMachine;
    private InputSystem_Actions _input;
    private SceneConfig _config;


    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void Initialize()
    {
        if (SceneManager.GetActiveScene().name != SceneConfig.Instance.MenuScene.Name)
        {
            SceneManager.LoadScene(SceneConfig.Instance.MenuScene.BuildIndex, LoadSceneMode.Additive);
        }
    }

    public void Awake()
    {
        _input = new InputSystem_Actions();
        _input.Menu.AddCallbacks(this);
        _input.Menu.Enable();
        _config = SceneConfig.Instance;

        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(EventSystem.gameObject);
        DontDestroyOnLoad(MainMenuRoot);
        DontDestroyOnLoad(IngameMenuRoot);
        DontDestroyOnLoad(LoadingRoot);

        MainMenuRoot.SetActive(false);
        IngameMenuRoot.SetActive(false);
        LoadingRoot.SetActive(false);

        var initialState = MenuStates.Ingame;
        if (SceneManager.GetActiveScene().name == SceneConfig.Instance.MenuScene.Name)
            initialState = MenuStates.StartUp;
        _stateMachine = new StateMachine<MenuStates, MenuTriggers>(initialState);

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
        await SceneManager.LoadSceneAsync(_config.GameScene.BuildIndex);
        LoadingRoot.SetActive(false);
    }

    private async Task LoadEmptyScene()
    {
        LoadingRoot.SetActive(true);
        await SceneManager.LoadSceneAsync(_config.EmptyScene.BuildIndex);
        LoadingRoot.SetActive(false);
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