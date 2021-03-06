using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Collection/Card Library/Logic")]
    public class CardLibraryLogic : CollectionLogic
    {
        protected CardLibraryUI _cardLibraryUI;
        protected CardInfoCollection _cardInfoCollection;
        
        public void Constructor(CardLibraryUI cardLibraryUI, CardInfoCollection cardInfoCollection)
        {
            _cardLibraryUI = cardLibraryUI;
            _cardInfoCollection = cardInfoCollection;
        }


        public override void OnClick(Card card, CardVizCollection cardVizCollection)
        {
            _cardInfoCollection.LoadCardInformation(card, cardVizCollection);
            _cardLibraryUI.SelectCardCollectionUI(cardVizCollection);

        }
    }
}

