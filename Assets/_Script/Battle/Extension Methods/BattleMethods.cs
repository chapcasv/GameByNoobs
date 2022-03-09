using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public static class BattleMethods
    {

        public static Vector3 GetPosMiddle(BaseUnit target)
        {
            if (!target.IsLive || target == null) return Vector3.zero;

            Vector3 targetPos = target.transform.position;
            float OffsetY = target.Col.size.y / 2;

            targetPos = new Vector3(targetPos.x, OffsetY, targetPos.z);
            return targetPos;
        }
    }

}
