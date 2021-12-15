using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Waves System")]
    public class WaveSystem : ScriptableObject
    {
        private Wave[] _waves;
        [NonSerialized] int _index = 0;

        public event Action OnWaveIndexChange;

        public void IncreaseIndex()
        {
            _index++;
            OnWaveIndexChange?.Invoke();
        }

        public int GetCurrentIndex() => _index;

        public Wave GetCurrentWave() => _waves[_index];

        public void SetData(PVE_Raid raid)
        {
            _waves = new Wave[raid.Waves.Length];
            raid.Waves.CopyTo(_waves, 0);
        }
    }
}

