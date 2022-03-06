using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{   
    [CreateAssetMenu(menuName = "ScriptableObject/Pluggable/Ability VFX/Undead Green")]
    public class PluggableUndeadGreenVFX : PluggableAbilityVFX
    {
        public override void PlayVFX(Vector3 pos)
        {
            VFXManager.Instance.PlayVFX(pos, KeysVFX.UndeadGreen.ToString());
        }
    }
}

