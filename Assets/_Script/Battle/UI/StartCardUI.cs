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
        [SerializeField] TimeBar timeBar;
        [SerializeField] List<Transform> startCardSlot;
        private float _time;
        private StartCardSystem _startCardSystem;

        private void Awake()
        {
            _startCardSystem = GetComponent<StartCardSystem>();
            _time = _startCardSystem.Time;
            _startCardSystem.OnStartCard += LoadStartCard;
            _startCardSystem.OnStartCard += RunTimeBar;
            _startCardSystem.OnReplace += LoadStartCard;
        }


        private void RunTimeBar() => StartCoroutine(timeBar.RunTimeBarStartCard(_time));

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

