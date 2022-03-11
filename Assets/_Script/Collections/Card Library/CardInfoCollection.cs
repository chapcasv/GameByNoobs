using UnityEngine;
using UnityEngine.UI;

namespace PH
{

    public class CardInfoCollection : CardInfoVisual
    {
        [System.Serializable]
        public class InfoCollectionElement
        {
            public Button main;
            public Image selected;
            public System.Action OnSelected;
            public void Init()
            {
                OnSelected += Selected;
                main.onClick.AddListener(() => OnSelected?.Invoke());
                
            }
            
            private void Selected()
            {
                selected.gameObject.SetActive(true);
            }
            public void Deselect()
            {
                selected.gameObject.SetActive(false);
            }
            
        }
        
        [Header("Stat Unit")]
        [SerializeField] InfoCollectionElement B_descriptionDetail;
        [SerializeField] InfoCollectionElement B_UnitDetail;
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
            B_descriptionDetail.Init();
            B_UnitDetail.Init();

            B_descriptionDetail.OnSelected += OnDescriptionCallBack;
            B_UnitDetail.OnSelected += OnUnitStatCallBack;
        }

        private void OnUnitStatCallBack()
        {
            unitStat.SetActive(true);
            cardDescription.SetActive(false);
            B_descriptionDetail.Deselect();
        }

        private void OnDescriptionCallBack()
        {
            unitStat.SetActive(false);
            cardDescription.SetActive(true);
            B_UnitDetail.Deselect();
        }

        protected override void LoadInfo(Card card)
        {
            base.LoadInfo(card);
            //cardDescription.SetActive(true);
            B_descriptionDetail.OnSelected?.Invoke();
            DeInteractive(false);
        }

        protected override void LoadCardInfoUnit(Card card)
        {
            base.LoadCardInfoUnit(card);
            DeInteractive(true);
            B_descriptionDetail.OnSelected?.Invoke();
            OnDescriptionCallBack();

        }
        private void DeInteractive(bool active)
        {
            B_UnitDetail.main.gameObject.SetActive(active);
        }

       
        private void OnDestroy()
        {
            B_descriptionDetail.OnSelected -= OnDescriptionCallBack;
            B_UnitDetail.OnSelected -= OnUnitStatCallBack;
        }
    }

}
