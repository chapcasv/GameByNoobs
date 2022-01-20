using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PH
{   
    public abstract class Ability : ScriptableObject
    {   
        [SerializeField] string abilityName;
        [SerializeField] string discription;
        [SerializeField] Sprite icon;

        [Range(1, 4)]
        [SerializeField] float range = 1;
        public string GetAbilityName => abilityName;
      
        public Sprite GetIcon => icon;

        public float GetRange()
        {
            return range * 6 + 2.5f;
        }

        public abstract string GetDiscription(CardUnit unit);
        public abstract string GetDiscription(BaseUnit unit);
        public abstract void CastSkill(BaseUnit currentTarget, UnitAtkSystem atkSystem);
    }
}

