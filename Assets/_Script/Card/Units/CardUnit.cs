
using UnityEngine;
using PH.GraphSystem;


namespace PH
{
    [CreateAssetMenu(fileName = "new Card", menuName = "ScriptableObject/Card/Unit")]
    public class CardUnit : Card
    {
        [SerializeField] BaseUnitID unitID;
        [Header("Ability")]
        [SerializeField] Ability abitity;

        [Header("Stat")]
        [SerializeField] int hp;
        [SerializeField] int manaMax;
        [SerializeField] int manaStart;

        [Range(0, 10)]
        [SerializeField] int manaRegen = 5;

        [SerializeField] int magicResist;
        [SerializeField] int damage;
        [SerializeField] int armor;
        [Range(0.50f,1f)]
        [SerializeField] float atkSpeed;

        [Range(4, 8)]
        [SerializeField] float moveSpeed = 4;

        [Range(1, 4)]
        [SerializeField] int range;
        private int critRate = 25;
        [Range(1, 5)]
        [SerializeField] int dmgLife = 1;

        #region Properties
        public int Range { get => range; }
        public int Damage { get => damage;}
        public float MoveSpeed { get => moveSpeed;}
        public BaseUnitID UnitID { get => unitID;}
        public int Hp { get => hp;}
        public int ManaMax { get => manaMax;}
        public int ManaStart { get => manaStart;}
        public int ManaRegen { get => manaRegen;}
        public float AtkSpeed { get => atkSpeed;}
        public int Armor { get => armor;}
        public int MagicResist { get => magicResist;}
        public int CritRate { get => critRate;}
        public int DmgLife { get => dmgLife;}
        public Ability Abitity { get => abitity;}
        #endregion

        public override bool CanDropBoard(Node dropNode)
        {   
            if (!dropNode.IsOccupied && GridBoard.NodePlayerTeam.Contains(dropNode))
            {
                return true;
            }
            else return false;
        }

        public override bool TryDropBoard(Node node, BoardSystem boardSystem)
        {   
            bool spawnResult = boardSystem.SpawnUnit(this, node);

            return spawnResult;
        }
    }
}

