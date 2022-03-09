using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Pluggable/Ability VFX/Hit Yellow")]
    public class PluggableHitYellow : PluggableAbilityVFX
    {
        public override void PlayVFX(Vector3 pos)
        {
            VFXManager.Instance.PlayVFX(pos, KeysVFX.Hit.ToString());
        }
    }

}
