using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace PH
{
    public class HealthSegmentLayout : MonoBehaviour
    {
        [SerializeField] bool isNotSegment;
        /// <summary>
        /// health amount one per segment
        /// </summary>
        [SerializeField] int segmentAmount;
        [SerializeField] float segmentWidth;
        [SerializeField] Color color;
        private Image[] _borders;
        private float startSegmentSize;
        
        private void Awake()
        {
            _borders = GetComponentsInChildren<Image>(true);
            startSegmentSize = _borders[0].rectTransform.rect.width + segmentWidth;
            SetColor();
        }

        //rect
        public void UpdateSegment(int curMaxHP)
        {
            if (isNotSegment) return;
           
            for (int i = 0; i < _borders.Length; i++)
            {
                float currentAmount = (i + 1) * segmentAmount;

                _borders[i].rectTransform.sizeDelta = new Vector2(segmentWidth, 0);

                if (currentAmount < curMaxHP)
                {
                    float x = currentAmount / curMaxHP * startSegmentSize - startSegmentSize / 2f;

                    _borders[i].rectTransform.localPosition = new Vector3(x, 0, 0);

                    _borders[i].rectTransform.sizeDelta = new Vector2(segmentWidth, 0);

                    _borders[i].gameObject.SetActive(true);
                }
                else
                {
                    _borders[i].rectTransform.localPosition = Vector3.zero;

                    _borders[i].gameObject.SetActive(false);
                }
            }
        }
        private void SetColor()
        {
            //_borders.ForEach(border => border.color = color);
            foreach (var border in _borders)
            {
                border.color = color;
            }
        }
    }
}

