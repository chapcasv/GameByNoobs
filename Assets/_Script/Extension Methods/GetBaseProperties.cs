using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Extension Methods/Get Base Properties")]
    public class GetBaseProperties : ScriptableObject
    {
        [SerializeField] ElementInt cost;
        [SerializeField] ElementImage art;
        [SerializeField] ElementInt price;

        public int GetCost(Card card)
        {
            var BaseProperties = card.baseProperties;
            for (int i = 0; i < BaseProperties.Length; i++)
            {
                if (BaseProperties[i].element == cost)
                {
                    return BaseProperties[i].intValue;
                }
            }
            return int.MaxValue; //Cant find card cost
        }

        
    }
}

