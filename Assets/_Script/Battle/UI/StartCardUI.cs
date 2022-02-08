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
        [SerializeField] List<Transform> startCardSlot;
        [SerializeField] StartCard startCardPhase;

        private void Awake()
        {
            startCardPhase.OnStartCard += LoadStartCard;
            startCardPhase.OnReplace += LoadStartCard;
            startCardPhase.OnComplete += Complete;
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

