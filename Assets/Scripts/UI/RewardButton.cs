using Slurs;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardButton : MonoBehaviour
{
    public Slur Reward;
    public RewardSelectionView RewardSelectionView;
    public TMP_Text Label;

    public void Start()
    {
        var button = GetComponent<Button>();
        button.onClick.AddListener(OnClick);
        Label.text = Reward.SlurText;
    }

    private void OnClick()
    {
        RewardSelectionView.Select(Reward);
    }
}