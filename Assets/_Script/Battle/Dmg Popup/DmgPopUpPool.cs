using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace PH.PopUp
{
    public class DmgPopUpPool : MonoSingleton<DmgPopUpPool>
    {
        [SerializeField] DmgPopUp pfDmgPopUp;
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

        public void Create(int dmg, DmgType type, Vector3 spawnPos, bool isCrit)
        {
            var DmgPopUp = Get();

            if (isCrit)
            {
                DmgPopUp.SetUpCrit(dmg, spawnPos);
            }
            else
            {
                DmgPopUp.SetUp(dmg, type, spawnPos);
            }
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

