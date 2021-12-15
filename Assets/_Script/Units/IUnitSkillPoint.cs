using System;

namespace PH
{
    public interface IUnitSkillPoint
    {
        event Action<float> OnSPIncrease;
        event Action OnCastSkill;

        void CastSkill();
        void IncreaseSP();
        void SetSP(int maxSP, int startSP, int spRegen);
    }
}