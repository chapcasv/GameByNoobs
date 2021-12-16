using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SO;
using UnityEngine.UI;
using System;

namespace PH
{
    [RequireComponent(typeof(StartCardPhase))]
    public class StartCardUI : MonoBehaviour
    {
        [SerializeField] TimeBar timeBar;
        [SerializeField] List<Transform> startCardSlot;
        private float _time;
        private StartCardPhase _startCardSystem;

        private void Awake()
        {
            _startCardSystem = GetComponent<StartCardPhase>();
            _time = _startCardSystem.Time;
            _startCardSystem.OnStartCard += LoadStartCard;
            _startCardSystem.OnStartCard += RunTimeBar;
            _startCardSystem.OnReplace += LoadStartCard;
        }

        private void RunTimeBar()
        {
            StartCoroutine(timeBar.TimeBarStartCard(_time,this));
        }

        public void Complete()
        {
            _startCardSystem.Complete();
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
            Card[] startCard = _startCardSystem.GetStartCard();
            return startCard;
        }
  
    }
}

