
using System.Collections.Generic;

namespace PH
{   
    //This class use for SaveSystem in namespace PH.Save
    [System.Serializable]
    public class PlayerData
    {
        public string PlayerName;
        public int Coin;
        public List<PlayerCard> Cards;
        public List<PlayerDeck> Decks;
    }
}


