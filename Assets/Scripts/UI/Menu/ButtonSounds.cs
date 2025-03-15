using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class ButtonSounds : MonoBehaviour
{
    public AudioClip Click;
    public AudioClip Hover;

    private EventTrigger _trigger;

    private void Start()
    {
        _trigger = GetComponent<EventTrigger>();

        AddSoundHandler(EventTriggerType.PointerEnter, OnHover);
        AddSoundHandler(EventTriggerType.Select, OnHover);
        AddSoundHandler(EventTriggerType.PointerDown, OnClick);
        AddSoundHandler(EventTriggerType.Submit, OnClick);
    }

    private void AddSoundHandler(EventTriggerType triggerType, UnityAction<BaseEventData> callback)
    {
        var triggerEntry = new EventTrigger.Entry { eventID = triggerType };
        triggerEntry.callback.AddListener(callback);
        _trigger.triggers.Add(triggerEntry);
    }

    private void OnHover(BaseEventData data)
    {
        SFXPlayer.Instance.PlaySFX(Hover);
    }

    private void OnClick(BaseEventData data)
    {
        SFXPlayer.Instance.PlaySFX(Click);
    }
}