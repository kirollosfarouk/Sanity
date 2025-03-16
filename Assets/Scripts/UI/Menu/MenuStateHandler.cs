using System.Collections;
using UI.Menu;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityHFSM;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MainMenuView : MonoBehaviour, InputSystem_Actions.IMenuActions
{
    public GameObject MainMenuRoot;
    public GameObject IngameMenuRoot;
    public GameObject LoadingRoot;
    public EventSystem EventSystem;

    public MusicDefinition MenuMusic;

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
        _stateMachine = new StateMachine<MenuStates, MenuTriggers>();

        _stateMachine.AddState(MenuStates.StartUp);
        _stateMachine.AddTransition(MenuStates.StartUp, MenuStates.LoadMainMenu);

        _stateMachine.AddState(MenuStates.LoadMainMenu, new CoState<MenuStates, MenuTriggers>(this, LoadEmptyScene, needsExitTime: true, loop: false));
        _stateMachine.AddTransition(MenuStates.LoadMainMenu, MenuStates.MainMenu);

        _stateMachine.AddState(MenuStates.MainMenu, onEnter: _ => MainMenuRoot?.SetActive(true), onExit: _ => MainMenuRoot?.SetActive(false));
        _stateMachine.AddTriggerTransition(MenuTriggers.StartGame, MenuStates.MainMenu, MenuStates.LoadIngame);
        _stateMachine.AddTriggerTransition(MenuTriggers.Exit, MenuStates.MainMenu, MenuStates.Exit);

        _stateMachine.AddState(MenuStates.LoadIngame, new CoState<MenuStates, MenuTriggers>(this, LoadIngame, needsExitTime: true, loop: false));
        _stateMachine.AddTransition(MenuStates.LoadIngame, MenuStates.Ingame);

        _stateMachine.AddState(MenuStates.Ingame);
        _stateMachine.AddTriggerTransition(MenuTriggers.Pause, MenuStates.Ingame, MenuStates.PauseMenu);
        _stateMachine.AddTriggerTransition(MenuTriggers.GameOver, MenuStates.Ingame, MenuStates.GameOver);

        _stateMachine.AddState(MenuStates.Exit, onEnter: _ => Quit());

        _stateMachine.AddState(MenuStates.PauseMenu, onEnter: PauseTransition, onExit: ResumeTransition);
        _stateMachine.AddTriggerTransition(MenuTriggers.Pause, MenuStates.PauseMenu, MenuStates.Ingame);
        _stateMachine.AddTriggerTransition(MenuTriggers.Exit, MenuStates.PauseMenu, MenuStates.LoadMainMenu);

        _stateMachine.AddState(MenuStates.GameOver);
        _stateMachine.AddTriggerTransition(MenuTriggers.Exit, MenuStates.GameOver, MenuStates.LoadMainMenu);

        _stateMachine.SetStartState(initialState);
        _stateMachine.Init();
    }

    public void Update()
    {
        _stateMachine.OnLogic();
    }

    public void StartGame()
    {
        Debug.Log("StartGame");
        _stateMachine.Trigger(MenuTriggers.StartGame);
    }

    public void Exit()
    {
        _stateMachine.Trigger(MenuTriggers.Exit);
    }

    public void TogglePause()
    {
        _stateMachine.Trigger(MenuTriggers.Pause);
    }

    private void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private IEnumerator LoadEmptyScene()
    {
        LoadingRoot.SetActive(true);
        var loading = SceneManager.LoadSceneAsync(_config.EmptyScene.BuildIndex);
        while (!loading.isDone)
        {
            yield return null;
        }

        LoadingRoot.SetActive(false);
        MusicPlayer.Instance.StartMusic(MenuMusic);
        _stateMachine.StateCanExit();
    }

    private IEnumerator LoadIngame()
    {
        Debug.Log("LoadIngame...");
        LoadingRoot.SetActive(true);
        var loading = SceneManager.LoadSceneAsync(_config.GameScene.BuildIndex);
        while (!loading.isDone)
        {
            yield return null;
        }

        LoadingRoot.SetActive(false);
        Debug.Log("LoadIngame done");
        _stateMachine.StateCanExit();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        if (context.performed)
            TogglePause();
    }

    private void PauseTransition(State<MenuStates, MenuTriggers> state)
    {
        _input.Player.Disable();
        Time.timeScale = 0;
        IngameMenuRoot?.SetActive(true);
    }

    private void ResumeTransition(State<MenuStates, MenuTriggers> state)
    {
        IngameMenuRoot?.SetActive(false);
        Time.timeScale = 1;
        _input.Player.Enable();
    }
}