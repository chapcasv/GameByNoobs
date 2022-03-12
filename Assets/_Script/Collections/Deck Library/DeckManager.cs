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
        [SerializeField] Image rankLabel;
        [SerializeField] Image costLabel;
        [SerializeField] Image faction;
        [SerializeField] Image coin;
        [SerializeField] TextMeshProUGUI cardName;
        [SerializeField] TextMeshProUGUI cost;
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI deckName;

        public void SetCurrentDeck(DeckVisual deckVisual)
        {
            avatar.sprite = deckVisual.GetAvatar;
            faction.sprite = deckVisual.GetFaction;
            cardName.text = deckVisual.GetName;
            title.text = deckVisual.GetTitle;
            rankLabel.color = deckVisual.GetColor;
            costLabel.color = deckVisual.GetColor;
            deckName.text = deckVisual.GetDeckName;


        }
    }
}

