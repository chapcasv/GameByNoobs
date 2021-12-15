using System;

namespace PH
{
    public interface IUnitHealth
    {
        event Action<BaseUnit> OnDie;
        event Action<float> OnTakeDamage;
        event Action<float> OnHealthUp;

        void HealthUp(int amount);
        void SetHP(int value, UnitTeam team);
        void TakeDamage(int amount, BaseUnit unit);
        void Die(BaseUnit unit);
    }
}