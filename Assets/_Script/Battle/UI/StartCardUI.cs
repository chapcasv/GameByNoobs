using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using UnityEngine.UI;
using System;

namespace PH
{
    [RequireComponent(typeof(StartCardSystem))]
    public class StartCardUI : MonoBehaviour
    {
        [SerializeField] List<Transform> startCardSlot;
        private LocalPlayer _playerTeam;

        private void Awake()
        {
            var StartCardSystem = GetComponent<StartCardSystem>();
            _playerTeam = StartCardSystem.playerTeam;
            StartCardSystem.OnStartCard += LoadStartCard;
            StartCardSystem.OnReplace += LoadStartCard;
        }


        private void LoadStartCard()
        {
            List<Card> startCard = GetStartCard();

            if (startCard.Count == startCardSlot.Count)
            {
                for (int i = 0; i < startCard.Count; i++)
                {
                    //Child(0) is Start Card Temp
                    CardVisual cardViz = startCardSlot[i].GetChild(0).GetComponent<CardVisual>();
                    cardViz.SetCard(startCard[i]);
                }
            }
        }

        private List<Card> GetStartCard()
        {
            List<Card> startCard = _playerTeam.CardsInHand;
            return startCard;
        }
  
    }
}

