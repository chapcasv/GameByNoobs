using UnityEngine;


namespace PH
{
    [CreateAssetMenu(fileName = "Create Random Card by History Item", 
        menuName = "ScriptableObject/Card/Trigger Input/Create Card/Random Card/Create Item By History Item")]
    public class InputCreateItemByHistoryItem : InputCreateRandomCard
    {   
        [Range(1,10)]
        [SerializeField] int requireHistoryItemCount;

        public int RequireHistoryItemCount { get => requireHistoryItemCount;}

        public override Card[] GetCard()
        {
            return base.GetCard();
        }
    }
}

