using UnityEngine;

namespace PH
{
    public abstract class PluggableAbilityVFX : ScriptableObject
    {
        public abstract void PlayVFX(Vector3 pos);
    }
}

