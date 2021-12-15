using System;

namespace PH
{
    public interface IMana
    {
        event Action<float> OnManaValueChange;
        bool IsFullMana();

        void CastSkill();
        void IncreaseMana();
        void SetMana(int maxSP, int startSP, int spRegen);
    }
}