
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(fileName = "new Input Draw", menuName = "ScriptableObject/Card/Trigger Input/Draw")]
    public class TriggerInputDrawCard : TriggerInput
    {   
        [Range(1,4)]
        [SerializeField] int numberCardDraw = 1;

        [Header("Type Input")]
        [SerializeField] bool useDrawByType;
        [SerializeField] TypeMode typeWantDraw;

        [Header("Cost Input")]
        [SerializeField] bool useDrawByCost;
        [Range(0, 9)]
        [SerializeField] int cardCost = 0;
        [SerializeField] CostMode costModeDraw;

        [Header("Faction Input")]
        [SerializeField] bool useDrawByFaction;
        [SerializeField] FactionMode factionModeDraw;

 
        public int NumbderCardDraw => numberCardDraw;
        public TypeMode TypeWantDraw => typeWantDraw;
        public bool UseDrawByType { get => useDrawByType;}

        public bool UseDrawByCost { get => useDrawByCost;}
        public int ValueCardCost => cardCost;
        public CostMode GetCostMode => costModeDraw;
    }
}

