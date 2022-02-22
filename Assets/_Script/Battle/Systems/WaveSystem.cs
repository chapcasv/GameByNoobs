using System;
using UnityEngine;

namespace PH
{
    [CreateAssetMenu(menuName = "ScriptableObject/Battle System/Waves System")]
    public class WaveSystem : ScriptableObject
    {
        public event Action OnWaveIndexChange;

        private Wave[] _waves;
        private int _index = 0;
        public bool IsLastWave() => _index == (_waves.Length - 1);
        public void IncreaseIndex()
        {
            _index++;
            OnWaveIndexChange?.Invoke();
        }

        public int GetCurrentIndex() => _index;

        public string GetCurrentIndexString()
        {
            int index = _index + 1;
            return index.ToString();
        }

        public Wave GetCurrentWave() => _waves[_index];

        public void SetData(PlayModeEnemy raid)
        {
            _waves = new Wave[raid.Waves.Length];
            raid.Waves.CopyTo(_waves, 0);
            _index = 0;
        }
    }
}

