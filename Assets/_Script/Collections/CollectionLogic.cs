using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public abstract class CollectionLogic : ScriptableObject
    {
        public abstract void OnClick(Card card, CardVizCollection cardVizCollection);
    }
}

