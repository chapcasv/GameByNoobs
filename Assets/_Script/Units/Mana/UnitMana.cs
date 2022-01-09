
namespace PH
{
    public class UnitMana :  ManaSystem
    {
        public override void SetMana(int maxMana, int startMana, int manaRegen)
        {
            this.maxMana = maxMana;
            this.manaRegenOnHit = manaRegen;
            manaCurrent = startMana;
            manaStart = startMana;
            isFullMana = false;

            float currentManaPct = (float)manaCurrent / this.maxMana;
            OnManaChange(currentManaPct);
        }

        public override void CastSkill()
        {
            manaCurrent = 0;
            isFullMana = false;
            OnManaChange(0);
        }

        public override void IncreaseMana()
        {
            manaCurrent += manaRegenOnHit;
            float currentManaPct = (float)manaCurrent / maxMana;
            OnManaChange(currentManaPct);

            if(manaCurrent == maxMana) isFullMana = true;
        }

        public override bool IsFullMana() => isFullMana;
    }
}

