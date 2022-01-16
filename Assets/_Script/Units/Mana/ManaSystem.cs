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

        public bool IsFullMana => isFullMana;

        public abstract void CastSkill();
        public virtual void IncreaseManaOnHit()
        {
            manaCurrent += manaRegenOnHit;
            CaculatorManaPCT();
        }
        public virtual void IncreaseManaOnTakeDame()
        {
            manaCurrent += manaRegenOnTakeDmg;

            CaculatorManaPCT();
        }

        protected void CaculatorManaPCT()
        {
            float currentManaPct = (float)manaCurrent / maxMana;
            OnManaChange(currentManaPct);

            if (manaCurrent == maxMana) isFullMana = true;
        }
        
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