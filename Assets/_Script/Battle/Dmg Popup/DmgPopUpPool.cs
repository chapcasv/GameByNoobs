using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH.PopUp
{
    public class DmgPopUpPool : MonoSingleton<DmgPopUpPool>
    {
        [SerializeField] DmgPopUp pfDmgPopUp;
        [SerializeField] DamageType heal;
        [SerializeField] DamageType magic;
        [SerializeField] DamageType physical;

        private Queue<DmgPopUp> pfsDmgPopUp = new Queue<DmgPopUp>();

        private bool isInit = false;
        public void Init()
        {
            if (isInit)
            {
                return;
            }
            else
            {
                isInit = true;
            }
        }

        public void Create(int dmg, DamageType type, Vector3 pos, bool isCrit)
        {
            var DmgPopUp = Get();

            if (isCrit)
            {
                DmgPopUp.SetUpCrit(dmg, pos);
            }
            else
            {
                DmgPopUp.SetUp(dmg, type, pos);
            }
        }
        
        public void CreateMagicDmg(int dmg, Vector3 pos, bool isCrit)
        {
            var DmgPopUp = Get();
            DmgPopUp.SetUp(dmg, magic, pos);
        }

        public void CreatePhysical(int dmg, Vector3 pos, bool isCrit)
        {
            var DmgPopUp = Get();

            if (isCrit)
            {
                DmgPopUp.SetUpCrit(dmg, pos);
            }
            else
            {
                DmgPopUp.SetUp(dmg, physical, pos);
            }
        }

        public void CreateHeal(int value, Vector3 pos)
        {
            var DmgPopUp = Get();
            DmgPopUp.SetUp(value, heal, pos);
        }

        private DmgPopUp Get()
        {
            if(pfsDmgPopUp.Count == 0)
            {
                AddToPool(1);
            }

            return pfsDmgPopUp.Dequeue();
        }

        private void AddToPool(int count)
        {
            for (int i = 0; i < count; i++)
            {
                DmgPopUp dmgPopUp = Instantiate(pfDmgPopUp,transform);
                dmgPopUp.gameObject.SetActive(false);
                pfsDmgPopUp.Enqueue(dmgPopUp);
            }
        }

        public void ReturnPool(DmgPopUp dmgPopUp)
        {
            dmgPopUp.gameObject.SetActive(false);
            pfsDmgPopUp.Enqueue(dmgPopUp);
        }
    }
}

