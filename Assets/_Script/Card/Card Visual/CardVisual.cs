using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace PH
{
    public class CardVisual : MonoBehaviour
    {
        [SerializeField] GameObject _synergyHolder;
        [SerializeField] PropertiesUI[] _basePropertiesViz;
        [SerializeField] GameObject[] _synergySlotViz;

        protected Card _card;

        public void SetCard(Card card)
        {
            this._card = card;
            LoadCard(this._card);
        }

        protected virtual void LoadCard(Card c)
        {
            if (_card == null) return;

            c.OnSetFactionViz(this);

            for (int i = 0; i < c.baseProperties.Length; i++)
            {
                BaseProperties bp = c.baseProperties[i];
                PropertiesUI bpUI = GetBaseProperty(bp.element);

                if (bpUI == null)
                    continue;

                if(bp.element is ElementImage)
                {
                    bpUI.img.sprite = bp.sprite;

                }else if(bp.element is ElementInt)
                {
                    bpUI.text.text = bp.intValue.ToString();
                }
                else if(bp.element is ElementText)
                {
                    bpUI.text.text = bp.stringValue;
                }
            }
        }

        public void LoadSynergy(Faction[] synergies)
        {
            _synergyHolder.SetActive(true);

            if (synergies.Length <= _synergySlotViz.Length)
            {
                HidenSynergiesSlot(); //Reload
                for (int i = 0; i < synergies.Length; i++)
                {
                    if (synergies[i].icon == null) continue;
                    _synergySlotViz[i].GetComponent<Image>().sprite = synergies[i].icon;
                    _synergySlotViz[i].SetActive(true);
                }
            } 
        }

        private void HidenSynergiesSlot()
        {
            for (int i = 0; i < _synergySlotViz.Length; i++)
            {
                _synergySlotViz[i].SetActive(false);
            }
        }

        protected PropertiesUI GetBaseProperty(Element e)
        {
            PropertiesUI result = null;

            for (int i = 0; i < _basePropertiesViz.Length; i++)
            {
                if(_basePropertiesViz[i].element == e)
                {
                    result = _basePropertiesViz[i];
                    break;
                }
            }
            return result;
        }

        public void HidenSynergyHolder() => _synergyHolder.gameObject.SetActive(false);

    }
}

