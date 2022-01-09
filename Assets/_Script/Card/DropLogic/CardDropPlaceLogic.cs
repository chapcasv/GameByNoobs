using PH.GraphSystem;
using UnityEngine;

namespace PH
{
    public abstract class CardDropPlaceLogic : ScriptableObject
    {
        public abstract bool CanDrop(Node dropNode);

    }
}

