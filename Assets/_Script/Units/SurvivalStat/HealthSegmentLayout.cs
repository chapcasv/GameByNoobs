using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class HealthSegmentLayout : HealthBase
    {
        public Color Color
        {
            set
            {
                foreach (Border border in Borders)
                {
                    border.Color = value;
                }
            }
        }
        private Border[] Borders => _borders == null || _borders.Length == 0 ? GetComponentsInChildren<Border>(true) : _borders;
        private Border[] _borders;
        private void Awake()
        {
            _borders = Borders;
        }
        public void UpdateSegment(int curMaxHP,int segmentAmount, bool isSegment, float borderWidth)
        {
            float effectiveWidth = Width + borderWidth;

            for (int i = 0; i < Borders.Length; i++)
            {
                float currentAmount = (i + 1) * segmentAmount;

                Borders[i].SizeDelta = new Vector2(borderWidth, 0);

                if (currentAmount < curMaxHP && isSegment)
                {
                    float x = currentAmount / curMaxHP * effectiveWidth - effectiveWidth / 2f;

                    Borders[i].LocalPos = new Vector3(x, 0, 0);

                    Borders[i].SizeDelta = new Vector2(borderWidth, 0);

                    Borders[i].IsActive = true;
                }
                else
                {
                    Borders[i].LocalPos = Vector3.zero;

                    Borders[i].IsActive = false;
                }
            }
        }
    }
}

