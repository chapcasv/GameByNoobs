using PH.GraphSystem;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Drop Place/Drop on Node in my zone")]
    public  class DropOnNodeInMyZone : CardDropPlaceLogic
    {
        public override bool CanDrop(Node dropNode)
        {
            if (dropNode.IsOccupied && GridBoard.NodePlayerTeam.Contains(dropNode))
            {
                return true;
            }
            else return false;
        }
    }
}

