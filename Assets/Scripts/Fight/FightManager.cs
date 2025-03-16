using System.Collections.Generic;
using System.Linq;
using Slurs;
using UI;
using UnityEngine;
using Data;
using Player;

namespace Fight
{
    public class FightManager : MonoBehaviour
    {
        public static FightManager Instance { get; private set; }
        public BossBarricade BossBarricade { get; set; }

        public MusicDefinition FightMusic;
        public MusicDefinition DefaultMusic;


        [SerializeField] private PlayerView PlayerView;
        [SerializeField] private OpponentView OpponentView;
        [SerializeField] private Transform BubblesParent;
        [SerializeField] private SlurBubble slurBubble;

        private Fight _currentFight;


        [SerializeField] private FighterData _tempOpponentFighterData;
        [SerializeField] private FighterData _tempPlayerFighterData;
        private EnemyInteraction _currentEnemyInteraction;

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
        }

        public void StartDebugFight()
        {
            StartFight(_tempOpponentFighterData, _tempPlayerFighterData);
        }

        public void StartFight(FighterData opponentFighterData, FighterData playerData,
            EnemyInteraction enemyInteraction = null)
        {
            _currentFight = new Fight();
            _currentEnemyInteraction = enemyInteraction;

            Opponent opponent = new Opponent();
            opponent.Inialize(
                opponentFighterData.Slurs,
                opponentFighterData.WeakPoints,
                opponentFighterData.Health);

            PlayerFighter playerFighter = new PlayerFighter();
            playerFighter.Inialize(
                PlayerManager.Instance.UnlockedSlurs,
                playerData.WeakPoints,
                playerData.Health);

            OpponentView.Setup(opponentFighterData);
            PlayerView.InitializeView(PlayerManager.Instance.UnlockedSlurs);

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

                if (_currentFight is { FightWinner: FightTurn.Player })
                {
                    ShowUnlockReward();
                }

                MusicPlayer.Instance.StartMusic(DefaultMusic);
                if (_currentFight != null)
                {
                    if (BossBarricade != null)
                        BossBarricade.EnemyDefeated();
                    if (_currentFight.FightWinner != FightTurn.Player)
                        MenuStateHandler.Instance.Lost();
                    _currentFight = null;
                    if (_currentEnemyInteraction != null)
                        Destroy(_currentEnemyInteraction.gameObject);
                    _currentEnemyInteraction = null;
                }

                foreach (Transform child in BubblesParent.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }

        private void ShowUnlockReward()
        {
            var newSlurs = PlayerManager.Instance.GetNewRandomSlurs(3);
            RewardSelectionView.Instance.Show(newSlurs);
        }

        public void WordSelected(Slur word)
        {
            if (_currentFight.PlayerFighter.selectedList.Count == 0)
            {
                clearBubbles();
            }

            _currentFight.PlayerFighter.AddSlur(word);
            Instantiate(slurBubble, BubblesParent).Initialize(word);
        }


        public void ShowSlurs(List<Slur> slurs)
        {
            clearBubbles();

            foreach (var slur in slurs)
            {
                var obj = Instantiate(slurBubble, BubblesParent);
                obj.Initialize(slur);
            }
        }

        private void clearBubbles()
        {
            if (BubblesParent.childCount != 0)
            {
                foreach (Transform child in BubblesParent.transform)
                {
                    Destroy(child.gameObject);
                }
            }
        }
    }
}