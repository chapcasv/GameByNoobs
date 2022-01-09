using PH.GraphSystem;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Card/Drop Place/Drop on my zone")]
    public class DropOnMyZone : CardDropPlaceLogic
    {
        public override bool CanDrop(Node place)
        {
            if (GridBoard.NodePlayerTeam.Contains(place))
            {
                return true;
            }
            else return false; ;
        }
    }
}

