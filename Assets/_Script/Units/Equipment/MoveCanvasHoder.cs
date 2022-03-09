using System;
using UnityEngine;
namespace PH
{
    public class MoveCanvasHoder : MonoBehaviour
    {
        [SerializeField] private RectTransform border;
        [SerializeField] private RectTransform item;
        [SerializeField] private RectTransform itemSlot;

        private float distance;
        public Action<bool> OnChangeCanvasHoder;
        private void OnEnable()
        {
            distance = itemSlot.rect.height;
            OnChangeCanvasHoder += MoveUpHoderBar;
        }
        private void MoveUpHoderBar(bool equiped)
        {
            if (!equiped)
            {
                Move(-distance);

            }
            else
            {
                Move(distance);
            }
        }
        private void Move(float distance)
        {
            float topBorder = border.offsetMax.y + distance;
            float bot = border.offsetMin.y + distance;
            border.offsetMax = new Vector2(border.offsetMax.x, topBorder);
            border.offsetMin = new Vector2(border.offsetMin.x, bot);
            float itemTop = item.offsetMax.y + distance;
            item.offsetMax = new Vector2(item.offsetMax.x, itemTop);
        }
        private void OnDisable()
        {
            OnChangeCanvasHoder -= MoveUpHoderBar;
        }
    }


}
