using System;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Waves System")]
    public class WaveSystem : ScriptableObject
    {
        private Wave[] _waves;
        [NonSerialized] int _index = 0;

        public event Action OnWaveIndexChange;


        public bool IsLastWave() => _index == (_waves.Length - 1);
        public void IncreaseIndex()
        {
            _index++;
            OnWaveIndexChange?.Invoke();
        }

        public int GetRewardClearWave() => _waves[_index].GoldBonus;

        public int GetWavesLength() => _waves.Length;

        public int GetCurrentIndex() => _index;

        public Wave GetCurrentWave() => _waves[_index];

        public void SetData(PVE_Raid raid)
        {
            _waves = new Wave[raid.Waves.Length];
            raid.Waves.CopyTo(_waves, 0);
        }
    }
}

