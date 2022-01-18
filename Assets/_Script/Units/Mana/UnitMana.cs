
namespace PH
{
    public class UnitMana :  ManaSystem
    {
        public override void SetMana(int maxMana, int startMana, int manaRegen)
        {
            baseMaxMana = maxMana;
            orMaxMana = baseMaxMana;

            baseManaRegenOnHit = manaRegen;
            orManaRegenOnHit = baseManaRegenOnHit;
            baseManaRegenOnTakeDmg = baseManaRegenOnHit;
            orManaRegenOnTakeDmg = baseManaRegenOnTakeDmg;

            baseManaStart = startMana;
            orManaStart = baseManaStart;

            baseManaCurrent = baseManaStart;
            orManaCurrent = baseManaCurrent;

            isFullMana = false;

            float currentManaPct = (float)orManaCurrent / orMaxMana;
            OnManaChange(currentManaPct);
        }

        public override void CastSkill()
        {
            ORManaCurrent = 0;
            isFullMana = false;
        }

    }
}

