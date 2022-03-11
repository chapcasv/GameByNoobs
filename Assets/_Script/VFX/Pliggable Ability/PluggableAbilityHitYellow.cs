using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Pluggable/Ability VFX/Hit/Yellow")]
    public class PluggableAbilityHitYellow : PluggableAbilityVFX
    {
        public override void PlayVFX(Vector3 pos)
        {
            VFXManager.Instance.PlayVFX(pos, KeysVFX.Hit.ToString());
        }
    }
}


