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
        [SerializeField] int hp;
        [SerializeField] int spMax;
        [SerializeField] int spStart;
        [SerializeField] int spRegen = 5;
        [SerializeField] int str;
        [Range(1,4)]
        [SerializeField] int range;
        [SerializeField] float atkSpeed;
        [SerializeField] float moveSpeed;
        [SerializeField] BaseUnitID unitID;
        #region Properties
        public int Range { get => range; }
        public int Str { get => str;}
        public float MoveSpeed { get => moveSpeed;}
        public BaseUnitID UnitID { get => unitID;}
        public int Hp { get => hp;}
        public int SpMax { get => spMax;}
        public int SpStart { get => spStart;}
        public int SpRegen { get => spRegen;}
        public float AtkSpeed { get => atkSpeed;}
        #endregion

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

