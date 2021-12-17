using PH.GraphSystem;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Equipment System")]
    public class EquipmentSystem : ScriptableObject
    {
        public bool DropItem(CardItem item, Node nodeDrop, UnitTeam team)
        {
            var allTeamMate = DictionaryTeamBattle.GetAllUnits(team);

            foreach (var unit in allTeamMate)
            {
                if (unit.CurrentNode == nodeDrop)
                {
                    bool canDrop = unit.Equip(item);
                    return canDrop;
                }
            }
            return false;
        }
    }
}

