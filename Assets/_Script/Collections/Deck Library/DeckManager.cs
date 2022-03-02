using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PH
{
    public class DeckManager : MonoBehaviour
    {
        [SerializeField] Image avatar;
        [SerializeField] TextMeshProUGUI deckName;

        public void SetCurrentDeck(DeckVisual deckVisual)
        {
            avatar.sprite = deckVisual.GetAvatar;
            deckName.text = deckVisual.GetDeckName;
        }
    }
}

