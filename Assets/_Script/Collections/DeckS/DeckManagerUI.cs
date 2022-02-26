using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace PH
{
    public class DeckManagerUI : MonoBehaviour
    {
        [SerializeField] private Button deckSetup;
        [SerializeField] private GameObject showChildCard;
        public void Start()
        {
            deckSetup.onClick.AddListener(DeckManageCallBack);
        }
        private void DeckManageCallBack()
        {
            showChildCard.SetActive(true);
        }
    }

}
