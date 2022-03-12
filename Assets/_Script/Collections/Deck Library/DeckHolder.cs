using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace PH
{
    public class DeckHolder : MonoBehaviour
    {
        public event Action OnReloadAllDeck;

        private void OnEnable()
        {
            OnReloadAllDeck?.Invoke();
        }
    }

}
