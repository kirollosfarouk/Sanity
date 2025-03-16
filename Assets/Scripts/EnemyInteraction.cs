using System.Linq;
using Data;
using Fight;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyInteraction : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    public FighterData FighterData;
    public GameObject Prompt;
    public TMP_Text PromptText;
    public bool IsBoss = false;

    private InputSystem_Actions _input;

    [CanBeNull]
    private PlayerMovement _player;

    private void Awake()
    {
        Prompt.SetActive(false);
        _input = new InputSystem_Actions();
        _input.Player.AddCallbacks(this);
    }

    private void OnDestroy()
    {
        if (IsBoss)
            MenuStateHandler.Instance.Won();
        DisableInteraction();
    }

    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player == null)
            return;

        _input.Player.Enable();
        _player = player;
        Prompt.SetActive(true);
        var binding = _input.Player.Interact.bindings.First(b => b.groups == "Keyboard&Mouse");
        PromptText.text = binding.ToDisplayString();
    }

    private void OnTriggerExit(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();
        if (player == null)
            return;
        DisableInteraction();
    }

    private void DisableInteraction()
    {
        _input.Player.Disable();
        _player = null;
        Prompt.SetActive(false);
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (_player != null)
            FightManager.Instance.StartFight(FighterData, _player.FighterData, this);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
    }
}