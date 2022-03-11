using PH.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using PH.Loader;

namespace PH
{
    public class MainMenuSystem : MonoBehaviour
    {
        [SerializeField] PlayerLocalSO player;
        [SerializeField] RankSystem rankSystem;

        [SerializeField] Button B_Play;
        [SerializeField] Button B_Collection;
        [SerializeField] Button B_Deck;
        [Header("UI")]
        [SerializeField] TextMeshProUGUI coin;
        [SerializeField] TextMeshProUGUI diamond;
        [SerializeField] TextMeshProUGUI playerName;
        [SerializeField] GameObject selectPlayMode;
        [Header("Rank")]
        [SerializeField] Image rankIcon;
        [SerializeField] TextMeshProUGUI rankTierLevel;

        private void Awake()
        {
            B_Play.onClick.AddListener(() => ShowPlayMode());
            B_Collection.onClick.AddListener(() => GoToCardLibrary());
            B_Deck.onClick.AddListener(GoToDeck);
        }

        void Start()
        {
            LoadPlayer();
        }

        private void LoadPlayer()
        {
            coin.text = SaveSystem.LoadCoin().ToString();
            playerName.text = SaveSystem.LoadName();
            diamond.text = SaveSystem.LoadDiamond().ToString();

            LoadRank();
        }

        private void LoadRank()
        {
            Rank rank = ConvertRank.Form(SaveSystem.LoadRank());

            RankInstance rankInstance = rankSystem.GetRank(rank.GetRankTier);

            string tier = rankInstance.RankName;
            string level = rank.GetRankLevelString();

            rankTierLevel.text = tier + " " + level;
            rankIcon.sprite = rankInstance.Icon;
        }

        public void ShowPlayMode()
        {
            Debug.Log("Show");
            selectPlayMode.SetActive(true);
        }

        public void GoToCardLibrary()
        {
            LoadSystem.Load(SceneSelect.CardLibrary);
        }

        private void GoToDeck()
        {
            LoadSystem.Load(SceneSelect.DeckLibrary);
        }

        private void OnDisable()
        {
            B_Collection.onClick.RemoveAllListeners();
            B_Play.onClick.RemoveAllListeners();
        }
    }
}

