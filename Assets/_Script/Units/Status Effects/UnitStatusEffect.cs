using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class UnitStatusEffect : MonoBehaviour
    {
        [SerializeField] private List<DataRealTimeStatusEffect> _dataRealTimeStatusEffects;
        private List<DataRealTimeStatusEffect> _outTime;
        private BaseUnit _unitHolder;

        public void SetUp(BaseUnit unit)
        {
            _unitHolder = unit;
            _dataRealTimeStatusEffects = new List<DataRealTimeStatusEffect>();
            _outTime = new List<DataRealTimeStatusEffect>();
        }

        public void ApplyStatusEffect(StatusEffect statusEffect)
        {
            var newDataRealTime = new DataRealTimeStatusEffect(statusEffect);
            _dataRealTimeStatusEffects.Add(newDataRealTime);

            statusEffect.Execute(_unitHolder);
        }

        public void Execute()
        {
            if (_dataRealTimeStatusEffects.Count == 0) return;

            foreach (var dataRealTime in _dataRealTimeStatusEffects)
            {
                dataRealTime.currentTime += Time.deltaTime;

                if(dataRealTime.currentTime > dataRealTime.statusEffect.LifeTime)
                {
                    dataRealTime.statusEffect.Remove(_unitHolder);
                    _outTime.Add(dataRealTime);
                    continue;
                }

                if(dataRealTime.currentTime > dataRealTime.nextTickTime)
                {
                    dataRealTime.nextTickTime += dataRealTime.statusEffect.TickSpeed;
                    dataRealTime.statusEffect.Execute(_unitHolder);
                }
            }

            RemoveOutTimeEffect();
        }

        private void RemoveOutTimeEffect()
        {
            if (_outTime.Count == 0) return;

            foreach (var effect in _outTime)
            {
                if (_dataRealTimeStatusEffects.Contains(effect))
                {
                    _dataRealTimeStatusEffects.Remove(effect);
                }
            }

            _outTime.Clear();
        }

       
    }
}

