using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public static class PlayModeData 
    {
        private static PlayMode currentMode;

        public static PlayMode CurrentMode { get => currentMode; set => currentMode = value; }
    }
}

