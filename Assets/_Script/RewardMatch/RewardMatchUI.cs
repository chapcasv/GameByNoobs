using PH.Loader;
using PH.Save;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;

namespace PH
{
    public class RewardMatchUI : MonoBehaviour
    {
        [SerializeField] Button b_back;
        [SerializeField] Button b_againt;

        [Header("Label Reward")]
        [SerializeField] Image labelReward;
        [SerializeField] Image rankIcon;
        [SerializeField] float delay;
        [SerializeField] Ease easeMode;
        [SerializeField] TextMeshProUGUI coinCurrent;
        [SerializeField] TextMeshProUGUI diamondCurrent;
        [SerializeField] TextMeshProUGUI coinRewardText;
        [SerializeField] TextMeshProUGUI diamondRewardText;
        [SerializeField] TextMeshProUGUI rankReward;
        [SerializeField] TextMeshProUGUI rankCurrent;
        [SerializeField] TextMeshProUGUI rankPointCurrent;

        private int oldCoin;
        private int oldDiamond;
        private int oldRankPoint;

        private RankSystem _rankSystem;
        private Vector2 topCenter;
        private Vector2 botCenter;

        void Start()
        {
            float offsetTop = Screen.height / 5;
            topCenter = new Vector2(0, offsetTop);
            
            float offsetBot = Screen.height / 6;
            botCenter = new Vector2(0, -offsetBot);

            AddListerner();
            DisplayCurrentRankIcon();
            DisplayLabel();

        }

        public void Constructor(RankSystem rs, PlayerLocalSO playerLocalSO, int coinCurrent, 
            int diamondCurrent, string rankTier, string rankLevel,int rankPoint)
        {
            _rankSystem = rs;
            _playerLocalSO = playerLocalSO;

            oldRankPoint = rankPoint;
            rankCurrent.text = rankTier + " " + rankLevel;
            rankPointCurrent.text = rankPoint.ToString();

            oldCoin = coinCurrent;
            this.coinCurrent.text = coinCurrent.ToString();

            oldDiamond = diamondCurrent;
            this.diamondCurrent.text = diamondCurrent.ToString();


            rankReward.gameObject.SetActive(false);
            coinRewardText.gameObject.SetActive(false);
            diamondRewardText.gameObject.SetActive(false);
        }

        public void DisplayCoinReward(int coinRewardValue)
        {
            var currentCoint = oldCoin + coinRewardValue;
            coinRewardText.gameObject.SetActive(true);
            coinRewardText.text = "+"+ coinRewardValue;

            DOVirtual.Int(oldCoin, currentCoint, 0.8f,
                (v) => coinCurrent.text = v.ToString());
        }

        public void DisplayDiamonReward(int diamondRewardValue)
        {
            var currentDiamond = oldDiamond + diamondRewardValue;
            diamondRewardText.gameObject.SetActive(true);
            diamondRewardText.text = "+" + diamondRewardValue;

            DOVirtual.Int(oldDiamond, currentDiamond, 0.6f,
               (v) => diamondCurrent.text = v.ToString());
        }

        public void DisplayRank(int bonus)
        {
            var currentPoint = oldRankPoint + bonus;
            rankReward.gameObject.SetActive(true);
            rankReward.text = $"+ {bonus}đ";

            DOVirtual.Int(oldRankPoint, currentPoint, 0.6f,
              (v) => rankPointCurrent.text = v.ToString());
        }


        private void DisplayCurrentRankIcon()
        {
            Rank currentRank = ConvertRank.Form(SaveSystem.LoadRank());
            RankInstance instance = _rankSystem.GetRank(currentRank.GetRankTier);
            rankIcon.sprite = instance.Icon;

            rankIcon.rectTransform.DOScale(Vector2.one, 0.4f).SetDelay(delay);

            rankIcon.rectTransform.DOAnchorPos(topCenter, 0.6f)
                .SetEase(easeMode)
                .SetDelay(delay);
        }

        private void DisplayLabel()
        {
            labelReward.rectTransform.DOAnchorPos(botCenter, 0.6f)
                .SetEase(easeMode)
                .SetDelay(delay);
        }

        private void AddListerner()
        {
            b_back.onClick.AddListener(() => LoadSystem.Load(SceneSelect.MainMenu));
            b_againt.onClick.AddListener(() => LoadSystem.Load(SceneSelect.FindMatch));
        }

        private void OnDisable()
        {
            b_back.onClick.RemoveAllListeners();
            b_againt.onClick.RemoveAllListeners();
        }
    }
}


