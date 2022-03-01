using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Collection/Deck Library/Logic")]
    public class DeckLibraryLogic : CollectionLogic
    {
        public override void OnClick(Card card, CardVizCollection cardVizCollection)
        {
            Debug.Log("Deck Library");
        }
    }
}

