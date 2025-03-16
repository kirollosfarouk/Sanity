using System.Collections.Generic;
using Slurs;
using UI;
using UnityEngine;
using Data;

namespace Fight
{
    public class FightManager : MonoBehaviour
    {
        public static FightManager Instance { get; private set; }

        public MusicDefinition FightMusic;
        public MusicDefinition DefaultMusic;


        [SerializeField] private PlayerView PlayerView;
        [SerializeField] private OpponentView OpponentView;
        [SerializeField] private Transform BubblesParent;
        [SerializeField] private SlurBubble slurBubble;

        private Fight _currentFight;


        [SerializeField] private FighterData _tempOpponentFighterData;
        [SerializeField] private FighterData _tempPlayerFighterData;

        void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
            }
            else
            {
                Instance = this;
            }

            SlursGenerator.LoadSlurs();
        }

        private void Start()
        {
            //StartFight(_tempOpponentFighterData, _tempPlayerFighterData);
        }

        public void StartDebugFight()
        {
            StartFight(_tempOpponentFighterData, _tempPlayerFighterData);
        }

        public void StartFight(FighterData opponentFighterData, FighterData playerData)
        {
            _currentFight = new Fight();

            Opponent opponent = new Opponent();
            opponent.Inialize(
                opponentFighterData.Slurs,
                opponentFighterData.WeakPoints,
                opponentFighterData.Health);

            PlayerFighter playerFighter = new PlayerFighter();
            playerFighter.Inialize(
                SlursGenerator.UnlockedSlurs,
                playerData.WeakPoints,
                playerData.Health);

            OpponentView.Setup(opponentFighterData);
            PlayerView.InitializeView(SlursGenerator.UnlockedSlurs);

            _currentFight.StartFight(opponent, playerFighter);
            gameObject.SetActive(true);
            MusicPlayer.Instance.StartMusic(FightMusic);
        }


        void Update()
        {
            if (_currentFight != null && !_currentFight.FightEnded)
            {
                _currentFight.Tick();

                PlayerView.UpdateHealth(_currentFight.PlayerFighter.CurrentHealth,
                    _currentFight.PlayerFighter.StartingHealth);

                OpponentView.UpdateHealth(_currentFight.Opponent.CurrentHealth,
                    _currentFight.Opponent.StartingHealth);
            }
            else
            {
                gameObject.SetActive(false);
                MusicPlayer.Instance.StartMusic(DefaultMusic);
                _currentFight = null;
                foreach (Transform child in BubblesParent.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        public void WordSelected(Slur word)
        {
            _currentFight.PlayerFighter.AddSlur(word);
        }


        public void ShowSlurs(List<Slur> slurs)
        {
            if (BubblesParent.childCount != 0)
            {
                foreach (Transform child in BubblesParent.transform)
                {
                    Destroy(child.gameObject);
                }
            }

            foreach (var slur in slurs)
            {
                var obj = Instantiate(slurBubble, BubblesParent);
                obj.Initialize(slur);
            }
        }
    }
}