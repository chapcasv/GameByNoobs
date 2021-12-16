using PH.GraphSystem;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Equipment System")]
    public class EquipmentSystem : ScriptableObject
    {
        public void DropItem(CardItem item, Node nodeDrop, UnitTeam team)
        {
            var allTeamMate = DictionaryTeamBattle.GetAllUnits(team);

            foreach (var unit in allTeamMate)
            {
                if (unit.CurrentNode == nodeDrop)
                {   
                    //Unit Equip
                    break;
                }
            }
        }
    }
}

