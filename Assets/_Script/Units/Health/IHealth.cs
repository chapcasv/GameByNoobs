using System;

namespace PH
{
    public interface IHealth
    {
        event Action OnDie;
        event Action<float> OnTakeDamage;
        event Action<float> OnHealthUp;

        bool IsLive { get; set; }

        void HealthUp(int amount);
        void SetUp(int maxHP, int armor, int mr, UnitTeam team);
        void TakeDamage(int amount);
        void Die();
    }
}