using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Extension Methods/Get Base Properties")]
    public class GetBaseProperties : ScriptableObject
    {
        [SerializeField] ElementInt cost;
        [SerializeField] ElementImage art;
        [SerializeField] ElementInt price;
        [SerializeField] ElementText cardName;

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

        public string GetName(Card card)
        {
            var BaseProperties = card.baseProperties;
            for (int i = 0; i < BaseProperties.Length; i++)
            {
                if (BaseProperties[i].element == cardName)
                {
                    return BaseProperties[i].stringValue;
                }
            }
            return null;
        }

        public Sprite GetArt(Card card)
        {
            var BaseProperties = card.baseProperties;
            for (int i = 0; i < BaseProperties.Length; i++)
            {
                if (BaseProperties[i].element == art)
                {
                    return BaseProperties[i].sprite;
                }
            }
            return null;
        }

        
    }
}

