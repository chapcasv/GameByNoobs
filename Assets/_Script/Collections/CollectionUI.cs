using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
namespace PH
{
    public class CollectionUI : MonoBehaviour
    {
        [System.Serializable]
        public class ButtonCollectionState
        {
            public enum StateOnCollection
            {
                DEFAULT, ONSELECTED
            }

            public Button main;
            public System.Action<bool> SwitchState;
            [SerializeField] private StateOnCollection currentState;
            public void Init()
            {
                main.onClick.AddListener(OnShowState);
                currentState = StateOnCollection.DEFAULT;
            }
            private void OnShowState()
            {
                if (currentState == StateOnCollection.DEFAULT)
                {
                    SwitchState?.Invoke(true);
                    currentState = StateOnCollection.ONSELECTED;
                }
                else
                {
                    SwitchState?.Invoke(false);
                    currentState = StateOnCollection.DEFAULT;
                }
            }

        }
       
        [SerializeField] private CardCollectionUI cardPrefab;
        [SerializeField] private ALLCard cardCollections;
        [SerializeField] private Button backButton;

        [SerializeField] private ButtonCollectionState OnUnlockedButton;

        [SerializeField] private List<CardCollectionUI> listCard;
        [SerializeField] private GetBaseProperties getBaseProperties;
        [Header("CheckBox")]
        [SerializeField] private ButtonCollectionState CkeckBoxButton;
        [SerializeField] private GameObject checkBoxScreen;

        [SerializeField] private Toggle cardunit;
        public void ShowUnit()
        {
            Debug.LogError(cardunit.isOn);
        }
        [Header("Check Type Card")]
        [SerializeField] private ButtonCollectionState unitType;
        [SerializeField] private ButtonCollectionState itemType;
        [SerializeField] private ButtonCollectionState spellType;

        [Header("Check Rate Card")]
        [SerializeField] private ButtonCollectionState summonRank;
        [SerializeField] private ButtonCollectionState rateRank;
        [SerializeField] private ButtonCollectionState epicRank;
        [SerializeField] private ButtonCollectionState legendRank;
        
        [Header("Check Energy Card")]
        [SerializeField] private ButtonCollectionState oneCost;
        [SerializeField] private ButtonCollectionState twoCost;
        [SerializeField] private ButtonCollectionState threeCost;
        [SerializeField] private ButtonCollectionState fourCost;
        [SerializeField] private ButtonCollectionState fiveCost;
        [SerializeField] private ButtonCollectionState sixCost;
        [SerializeField] private ButtonCollectionState sevenCost;
        [SerializeField] private ButtonCollectionState plusEightCost;

        [Header("Check Region Card")]
        [SerializeField] private ButtonCollectionState rockMountainRegion;

        [SerializeField] private ButtonCollectionState clearAllSelection;



        private void Start()
        {
            backButton.onClick.AddListener(OnBackMainMenuCallBack);

            CkeckBoxButton.Init();
            OnUnlockedButton.Init();


            unitType.Init();
            itemType.Init();
            spellType.Init();



            CkeckBoxButton.SwitchState = OnShowCheckBoxCallBack;
            OnUnlockedButton.SwitchState = ShowLockCardCallBack;

            unitType.SwitchState = OnShowUnitTypeCallBack;
            itemType.SwitchState = OnShowItemTypeCallBack;
            spellType.SwitchState = OnShowSpellTypeCallBack;




            PopUpCardCollection();
            ShowUnlockedCard();
            ArrangeListCard();
        }

        private void OnShowSpellTypeCallBack(bool show)
        {
            if(show == true)
            {
                for (int i = 0; i < listCard.Count; i++)
                {
                    if (listCard[i].GetTypeCardOnCollection() != TypeMode.SPELL)
                    {
                        listCard[i].OnRefreshCard(!show);
                    }
                    else
                    {
                        listCard[i].OnRefreshCard(show);
                    }
                }

            }
            else
            {
                //xoa lua chon
            }


        }

        private void OnShowItemTypeCallBack(bool show)
        {
            if (show == true)
            {
                for (int i = 0; i < listCard.Count; i++)
                {
                    if (listCard[i].GetTypeCardOnCollection() != TypeMode.ITEM)
                    {
                        listCard[i].OnRefreshCard(!show);
                    }
                    else
                    {
                        listCard[i].OnRefreshCard(show);
                    }
                }
            }
            else
            {
                //xoa lua chon
            }
           
        }

        private void OnShowUnitTypeCallBack(bool show)
        {
            if(show == true)
            {
                for (int i = 0; i < listCard.Count; i++)
                {
                    if (listCard[i].GetTypeCardOnCollection() == TypeMode.UNIT)
                    {
                        listCard[i].OnRefreshCard(show);
                    }
                    else
                    {
                        listCard[i].OnRefreshCard(!show);
                    }

                }
            }
            else
            {
                //xoa lua chon
            }
          
        }

        private void OnShowCheckBoxCallBack(bool show)
        {
            checkBoxScreen.SetActive(show);
        }

        private void ShowLockCardCallBack(bool show)
        {
            for (int i = 0; i < listCard.Count; i++)
            {
                if(listCard[i].IsUnlocked == false)
                {
                    listCard[i].OnRefreshCard(show);
                }
            }
            LoadInformation();
        }

        private void ShowUnlockedCard()
        {
            for (int i = 0; i < listCard.Count; i++)
            {
                if (listCard[i].IsUnlocked == true)
                {
                    listCard[i].OnRefreshCard(true);
                }
                else
                {
                    listCard[i].OnRefreshCard(false);
                }
            }
            LoadInformation();
        }
        private void LoadInformation()
        {
            foreach (var item in listCard)
            {
                if (item.gameObject.activeInHierarchy)
                {
                    item.OnClick?.Invoke();
                    break;
                }
            }
        }
        private void ArrangeListCard()
        {
            CardCollectionUI temp = null;
            for (int i = 0; i < listCard.Count; i++)
            {
                for (int j = i+1; j < listCard.Count; j++)
                {
                    if(listCard[j].Cost < listCard[i].Cost)
                    {
                        temp = listCard[i];
                        listCard[i] = listCard[j];
                        listCard[j] = temp;
                    }
                }
                
            }
            foreach (var item in listCard)
            {
                item.gameObject.transform.SetParent(cardPrefab.transform.parent);
            }
        }
      
        private void PopUpCardCollection()
        {
            var firstCard = cardCollections.allCard[0];
            cardPrefab.SetCard(firstCard);
            cardPrefab.Init(firstCard, getBaseProperties);
            //cardPrefab.OnClick?.Invoke();
            listCard.Add(cardPrefab);
            for (int i = 1; i < cardCollections.allCard.Count; i++)
            {
                var card = cardCollections.allCard[i];
                var _cardCollection = Instantiate(cardPrefab);
                _cardCollection.SetCard(card);
                _cardCollection.Init(card, getBaseProperties);
                listCard.Add(_cardCollection);

            }
        }
        private void OnBackMainMenuCallBack()
        {
            SceneManager.LoadScene(SceneSelect.MainMenu.ToString());
        }
    }
}

