using System;
using UnityEngine;

namespace PH
{
    public abstract class ManaSystem : MonoBehaviour
    {
        public event Action<float> OnManaValueChange;

        #region Properties
        protected int baseMaxMana;
        protected int orMaxMana;

        protected int baseManaCurrent;
        protected int orManaCurrent;

        protected int baseManaStart;
        protected int orManaStart;

        protected int baseManaRegenOnHit;
        protected int orManaRegenOnHit;

        protected int baseManaRegenOnTakeDmg;
        protected int orManaRegenOnTakeDmg;

        protected bool isFullMana;

        public int BaseMaxMana => baseMaxMana;
        public int ORMaxMana => orMaxMana;

        public int ORManaCurrent 
        {   get => orManaCurrent;
            set 
            { 
                orManaCurrent = value;
                float currentManaPct = (float)orManaCurrent / orMaxMana;
                OnManaValueChange?.Invoke(currentManaPct);

                if (ORManaCurrent == orMaxMana) isFullMana = true;
            } 
        }

        public int BaseManaStart => baseManaStart;
        public int BaseManaRegenOnHit => baseManaRegenOnHit;
        public int BaseManaRegenOnTakeDmg => baseManaRegenOnTakeDmg;

        #endregion

        #region Reuse Methods - Use for PlayerCacheUnitData

        public void ReuseMaxMana(int value)
        {
            baseMaxMana = value;
            orMaxMana = baseMaxMana;
        }

        public void ReuseManaStart(int value)
        {
            baseManaStart = value;
            orManaStart = baseManaStart;
            ORManaCurrent = orManaStart;
        }

        public void ReuseManaRegenOnHit(int value)
        {
            baseManaRegenOnHit = value;
            orManaRegenOnHit = baseManaRegenOnHit;
        }

        public void ReuseManaRegenOnTakeDmg(int value)
        {
            baseManaRegenOnTakeDmg = value;
            orManaRegenOnTakeDmg = baseManaRegenOnTakeDmg;
        }
        #endregion

        #region UpStat Methods - Use for buff

        public void UpOneRoundMaxMana(int value) => orMaxMana -= value;

        public void UpBaseMaxMana(int value)
        {
            baseMaxMana -= value;
            orMaxMana -= value;
        }

        public void UpOneRoundManaStart(int value)
        {
            orManaStart += value;
            ORManaCurrent = orManaStart;
        }

        public void UpBaseManaStart(int value)
        {
            baseManaStart += value;
            orManaStart += value;
            ORManaCurrent = orManaStart;
        }

        public void UpOneRoundManaRegenOnHit(int value) => orManaRegenOnHit += value;

        public void UpBaseManaRegenOnHit(int value)
        {
            baseManaRegenOnHit += value;
            orManaRegenOnHit += value;
        }

        public void UpOneRoundManaRegenOnTakeDame(int value) => orManaRegenOnTakeDmg += value;
        public void UpBaseManaRegenOnTakeDmg(int value)
        {
            baseManaRegenOnTakeDmg += value;
            orManaRegenOnTakeDmg += value;
        }

        #endregion

        public bool IsFullMana => isFullMana;

        public abstract void CastSkill();
        public virtual void IncreaseManaOnHit()
        {
            if (IsFullMana) return;

            ORManaCurrent += orManaRegenOnHit;
            CaculatorManaPCT();
        }
        public virtual void IncreaseManaOnTakeDame()
        {
            if (IsFullMana) return;

            ORManaCurrent += orManaRegenOnTakeDmg;
            CaculatorManaPCT();
        }

        protected void CaculatorManaPCT()
        {
            float currentManaPct = (float)ORManaCurrent / orMaxMana;
            OnManaChange(currentManaPct);

            if (ORManaCurrent == orMaxMana) isFullMana = true;
        }
        
        public abstract void SetMana(int maxSP, int startSP, int spRegen);

        protected void OnManaChange(float pct)
        {
            OnManaValueChange?.Invoke(pct);
        }
    }
}