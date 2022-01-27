using UnityEngine;
using PH.GraphSystem;
using System.Collections.Generic;

namespace PH
{
    [CreateAssetMenu(fileName = "new Card", menuName = "ScriptableObject/Card/Unit/New Unit")]
    public class CardUnit : Card
    {
        [SerializeField] BaseUnitID unitID;

        [Header("Ability")]
        [SerializeField] Ability abitity;

        [SerializeField] DataTriggerOnBoard[] dataTriggerOnBoards;

        [Header("Survival Stat")]
        [SerializeField] int hp;
        [SerializeField] int magicResist;
        [SerializeField] int armor;

        [Header("Mana Stat")]
        [SerializeField] int manaMax;
        [SerializeField] int manaStart;
        [Range(0, 10)]
        [SerializeField] int manaRegen = 5;

        [Header("Basic Atk")]
        [SerializeField] int damage;
        [Range(1, 4)]
        [SerializeField] int range = 1;
        [Range(0.20f, 1f)]
        [SerializeField] float atkSpeed = 0.20f;

        [Header("Other")]
        [Range(4, 8)]
        [SerializeField] float moveSpeed = 4;
        [Range(1, 5)]
        [SerializeField] int dmgLife = 1;

        [SerializeField] private bool negativeBonusDamage;


        #region Properties
        public int Range { get => range; }
        public int Damage { get => damage; }
        public float MoveSpeed { get => moveSpeed; }
        public BaseUnitID UnitID { get => unitID; }
        public int Hp { get => hp; }
        public int ManaMax { get => manaMax; }
        public int ManaStart { get => manaStart; }
        public int ManaRegen { get => manaRegen; }
        public float AtkSpeed { get => atkSpeed; }
        public int Armor { get => armor; }
        public int MagicResist { get => magicResist; }
        public int DmgLife { get => dmgLife; }

        public bool NegativeBonusDamage { get => negativeBonusDamage; }
        public Ability Abitity { get => abitity; }

        public override TypeMode GetCardType() => TypeMode.UNIT;

        public DataTriggerOnBoard[] GetDataTriggerOnBoards => dataTriggerOnBoards;
        #endregion


    }
}

