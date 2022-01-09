using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;

namespace PH
{
    [CreateAssetMenu(fileName = "new Card", menuName = "ScriptableObject/Card/Spell/New Spell")]
    public class CardSpell : Card
    {
        public override TypeMode GetCardType() => TypeMode.SPELL;
    }
}

