using UnityEngine;
using UnityEngine.UI;

namespace PH
{

    public class CardInfoCollection : CardInfoVisual
    {
        
        [Header("Stat Unit")]
        [SerializeField] Button B_descriptionDetail;
        [SerializeField] Button B_UnitDetail;
        [SerializeField] private ALLCard _allCards;
   
        private void Start()
        {
            AddListenerDetail();
        }

        public void LoadCardInformation(Card card, CardVizCollection cardUI)
        {
            LoadCard(card);
         
        }
       
        private void AddListenerDetail()
        {
            B_descriptionDetail.onClick.AddListener(() => OnDescriptionDetailCallBack());
            B_UnitDetail.onClick.AddListener(() => OnUnitDetailCallBack());
        }

        private void OnUnitDetailCallBack()
        {
            unitStat.SetActive(true);
            cardDescription.SetActive(false);
        }

        private void OnDescriptionDetailCallBack()
        {
            unitStat.SetActive(false);
            cardDescription.SetActive(true);
        }

        protected override void LoadInfo(Card card)
        {
            base.LoadInfo(card);
            cardDescription.SetActive(true);
            DeInteractive(false);
        }

        protected override void LoadCardInfoUnit(Card card)
        {
            base.LoadCardInfoUnit(card);
            DeInteractive(true);
            B_descriptionDetail.Select();
            OnDescriptionDetailCallBack();

        }
        private void DeInteractive(bool active)
        {
            B_descriptionDetail.interactable = active;
            B_UnitDetail.interactable = active;
        }

       
        private void OnDestroy()
        {
            B_descriptionDetail.onClick.RemoveAllListeners();
            B_UnitDetail.onClick.RemoveAllListeners();
        }
    }

}
