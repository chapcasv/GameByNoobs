using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;
using SO;


namespace PH
{
    [CreateAssetMenu(fileName = "new Card", menuName = "ScriptableObject/Card/Unit")]
    public class CardUnit : Card
    {
        [SerializeField] int str;
        [SerializeField] FloatVariable moveSpeed;
        [SerializeField] BaseUnitID unitID;

        public int Str { get => str;}
        public FloatVariable MoveSpeed { get => moveSpeed;}
        public BaseUnitID UnitID { get => unitID;}

        public override bool CanDropBoard(Node dropNode)
        {   

            if (!dropNode.IsOccupied && GridBoard.NodePlayerTeam.Contains(dropNode))
            {
                return true;
            }
            else return false;
            
        }

        public override void DropBoard(Node node, BoardSystem boardSystem)
        {
            boardSystem.SpawnUnit(this, node);
        }
    }
}

