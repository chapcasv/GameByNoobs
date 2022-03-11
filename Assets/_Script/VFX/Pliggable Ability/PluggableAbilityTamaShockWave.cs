using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Pluggable/Ability VFX/Tama Shock Wave")]
    public class PluggableAbilityTamaShockWave : PluggableAbilityVFX
    {
        public override void PlayVFX(Vector3 pos)
        {

            VFXManager.Instance.PlayVFX(pos, KeysVFX.TamaShockWave.ToString());
        }
    }
}

