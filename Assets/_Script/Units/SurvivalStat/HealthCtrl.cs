using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    [System.Serializable]
    public class BaseColor
    {
        public Color Border => _border;
        [SerializeField] Color _border = Color.black;
    }
    public class HealthCtrl : HealthBase
    {
        [SerializeField] bool isSegment = true;
        [SerializeField] int segmentAmount;
        [Space]
        [SerializeField] BaseColor baseColor;
        [SerializeField] float segmentBorderWidth;
        private HealthSegmentLayout _segmentManager;
        private HealthSegmentLayout segmentManager => _segmentManager == null ||
            !Application.isPlaying ? GetComponentInChildren<HealthSegmentLayout>() : _segmentManager;

        public void UpdateSegment(int OrMaxHP)
        {
            segmentManager.Color = baseColor.Border;
            segmentManager.UpdateSegment(OrMaxHP, segmentAmount, isSegment, segmentBorderWidth);
        }
    }

}
