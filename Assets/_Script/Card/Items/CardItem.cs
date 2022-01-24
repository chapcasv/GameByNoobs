using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;

namespace PH
{
    [CreateAssetMenu(fileName = "new Card", menuName = "ScriptableObject/Card/Item/Item")]
    public class CardItem : Card
    {
        [SerializeField] private bool isOnRound;
        public bool IsOnRound { get => isOnRound;}
        public override TypeMode GetCardType() => TypeMode.ITEM;
    }
}

