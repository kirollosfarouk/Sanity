using System;
using System.Collections.Generic;
using Player;
using Slurs;
using UnityEngine;

public class RewardSelectionView : MonoBehaviour
{
    public static RewardSelectionView Instance => _instance;
    private static RewardSelectionView _instance;

    public RewardButton RewardButtonPrefab;
    public Transform RewardButtonRoot;
    public AudioClip RewardAudioClip;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this)
            Destroy(gameObject);

        gameObject.SetActive(false);
    }

    public void Show(IEnumerable<Slur> rewardOptions)
    {
        foreach (Transform child in RewardButtonRoot)
        {
            Destroy(child.gameObject);
        }

        foreach (var option in rewardOptions)
        {
            var button = Instantiate(RewardButtonPrefab, RewardButtonRoot);
            button.Reward = option;
            button.RewardSelectionView = this;
            button.gameObject.name = option.SlurText;
        }

        gameObject.SetActive(true);
        SFXPlayer.Instance.PlaySFX(RewardAudioClip);
    }

    public void Select(Slur reward)
    {
        PlayerManager.Instance.UnlockSlur(reward);
        gameObject.SetActive(false);
    }
}