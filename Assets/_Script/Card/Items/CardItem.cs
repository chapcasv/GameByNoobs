using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;

namespace PH
{
    [CreateAssetMenu(fileName = "new Card", menuName = "ScriptableObject/Card/Item/Item")]
    public class CardItem : Card
    {
        
        public override TypeMode GetCardType() => TypeMode.ITEM;
    }
}

