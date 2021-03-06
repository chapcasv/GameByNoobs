
namespace PH
{
    public class UnitNormalSurvivalStat : UnitSurvivalStat
    {
        public override void SetUp(int maxHP, int armor, int mr,bool negativeBonusDamage,UnitTeam team)
        {
            baseMaxHP = maxHP;
            orMaxHP = baseMaxHP;
            ORCurrentHP = orMaxHP;

            baseArmor = armor;
            orArmor = baseArmor;

            baseMagicResist = mr;
            orMagicResist = baseMagicResist;
            IsLive = true;
            CanRegen = true;

            negatesBonusCritDmg = negativeBonusDamage;
            if (team == UnitTeam.Enemy) HpBar.color = COLOR_ENEMY;
            else HpBar.color = COLOR_PLAYER_TEAM;
        }


        public override void Die() => TriggerOnDie();
    }

}

