using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;

namespace PH
{
    [CreateAssetMenu(fileName = "new Card", menuName = "ScriptableObject/Card/Item")]
    public class CardItem : Card
    {
        public override bool CanDropBoard(Node dropNode)
        {
            if (dropNode.IsOccupied && GridBoard.NodePlayerTeam.Contains(dropNode))
            {
                return true;
            }
            else return false;
        }

        public override void DropBoard(Node node, BoardSystem boardSystem)
        {
            boardSystem.DropItem(this, node, UnitTeam.Player);
        }
    }
}

