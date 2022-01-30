using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace PH
{
    [CreateAssetMenu(fileName = "Up power", menuName = "ScriptableObject/Card/Unit/AddON After CS/Plus magic power")]
    public class PlusBaseMagicPower : AddOnAfterCastAbility
    {
        [SerializeField] private int value;
        public override void Execute(BaseUnit unit)
        {
            var atkSys = unit.GetAtkSystem;

            atkSys.UpBaseMagicPower(value);
        }
        
      
        
    }

}
