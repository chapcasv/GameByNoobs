using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using UnityEngine.UI;
using System;

namespace PH
{

    public class StartCardUI : MonoBehaviour
    {
        [System.Serializable]
        public class RerollCardUI
        {
            public Transform slot;
            public Button main;

            private StartCard startCard;
            private bool isReplaced;
            private bool freeRoll;
            
            public Action<Transform> OnClick;

            public void Init(StartCard _startCard,bool infinity)
            {
                freeRoll = infinity;
                isReplaced = false;
                startCard = _startCard;
                OnClick += RePlace;
                main.onClick.AddListener(() => OnClick?.Invoke(slot));
            }
            private void RePlace(Transform slot)
            {
                if (isReplaced) return;
                startCard.Replace(slot);
                isReplaced = !freeRoll;
                main.interactable = !isReplaced;
            }
            
        }
        [SerializeField] List<Transform> startCardSlot;
        [SerializeField] StartCard startCardPhase;
        [SerializeField] RerollCardUI[] B_rerolls;
        [SerializeField] private bool freeRoll;

        private void Awake()
        {
            startCardPhase.OnStartCard += LoadStartCard;
            startCardPhase.OnReplace += LoadStartCard;
            startCardPhase.OnComplete += Complete;
            AddListen();
           
        }
        private void AddListen()
        {
            for (int i = 0; i < B_rerolls.Length; i++)
            {
                B_rerolls[i].Init(startCardPhase, freeRoll);
            }
        }
        public void Complete()
        {
            gameObject.SetActive(false);
        }

        private void LoadStartCard()
        {
            Card[] startCard = GetStartCard();

            if (startCard.Length == startCardSlot.Count)
            {
                for (int i = 0; i < startCard.Length; i++)
                {
                    //Child(0) is Start Card Temp
                    CardVisual cardViz = startCardSlot[i].GetChild(0).GetComponent<CardVisual>();
                    cardViz.SetCard(startCard[i]);
                }
            }
        }

        private Card[] GetStartCard()
        {
            Card[] startCard = startCardPhase.GetStartCard();
            return startCard;
        }

        private void OnDisable()
        {
            startCardPhase.OnStartCard -= LoadStartCard;
            startCardPhase.OnReplace -= LoadStartCard;
            startCardPhase.OnComplete -= Complete;
        }

    }
}

