using PH.GraphSystem;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Drop Place/Drop on every zone")]
    public class DropOnEveryZone : CardDropPlaceLogic
    {
        public override bool CanDrop(Node dropNode)
        {
            if (GridBoard.NodePlayerTeam.Contains(dropNode) || GridBoard.NodeEnemyTeam.Contains(dropNode))
            {
                return true;
            }
            else return false;
        }
    }
}

