using System;
using UnityEngine;

namespace PH
{
    public abstract class ManaSystem : MonoBehaviour
    {
        public event Action<float> OnManaValueChange;

        protected int maxMana;
        protected int manaCurrent;
        protected int manaRegenOnHit;
        protected int manaStart;
        protected int manaRegenOnTakeDmg;
        protected bool isFullMana;

        public abstract bool IsFullMana();

        public abstract void CastSkill();
        public abstract void IncreaseMana();
        public abstract void SetMana(int maxSP, int startSP, int spRegen);

        public void UpManaStart(int value) => manaStart += value;

        public void UpManaRegenOnHit(int value) => manaRegenOnHit += value;

        public void UpManaRegenOnTakeDmg(int value) => manaRegenOnTakeDmg += value;

        protected void OnManaChange(float pct)
        {
            OnManaValueChange?.Invoke(pct);
        }
    }
}