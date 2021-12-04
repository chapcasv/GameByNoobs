using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public static class Setting 
    {
        private static ResourcesManager _resourcesManager;

        public static BattleManager battleManager;
        public static EffectGridMap effectGridMap;

        public static ResourcesManager GetResourcesManager()
        {
            if(_resourcesManager == null)
            {
                _resourcesManager = Resources.Load("ResourcesManager") as ResourcesManager;
            }
            return _resourcesManager;
        }
    }
}

