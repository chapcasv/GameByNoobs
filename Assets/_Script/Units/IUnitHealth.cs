using System;

namespace PH
{
    public interface IUnitHealth
    {
        event Action OnDie;
        event Action<float> OnTakeDamage;
        event Action<float> OnHealthUp;

        bool IsLive { get; set; }

        void HealthUp(int amount);
        void SetHP(int value, UnitTeam team);
        void TakeDamage(int amount);
        void Die();
    }
}