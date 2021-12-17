using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;

namespace PH
{
    [CreateAssetMenu(fileName = "new Card", menuName = "ScriptableObject/Card/Spell")]
    public class CardSpell : Card
    {
        public override bool CanDropBoard(Node node)
        {
            return false;
        }

        public override bool TryDropBoard(Node node , BoardSystem boardSystem)
        {
            return false;
        }
    }
}

