using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    public abstract class Ability : ScriptableObject
    {   
        [SerializeField] string abilityName;
        [SerializeField] Sprite icon;

        [Range(1, 32)]
        [SerializeField] float range = 1;
        public string GetAbilityName => abilityName;
      
        public Sprite GetIcon => icon;

        public virtual float GetRange()
        {
            return range * 6 + 2.5f;//Cel Size
        }

        public abstract string GetDiscription(CardUnit unit);
        public abstract string GetDiscription(BaseUnit unit);
        protected abstract string GetDiscription(int value);

        public abstract void CastSkill(BaseUnit currentTarget, BaseUnit caster);
    }
}

