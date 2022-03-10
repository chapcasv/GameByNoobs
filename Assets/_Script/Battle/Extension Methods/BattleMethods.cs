using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public static class BattleMethods
    {
        public static Vector3 GetMidPos(BaseUnit target)
        {
            Vector3 targetPos = target.transform.position;
            float OffsetY = target.Col.size.y / 2;

            return new Vector3(targetPos.x, OffsetY, targetPos.z);
        }

        public static Vector3 GetTopPos(BaseUnit target)
        {
            Vector3 targetPos = target.transform.position;
            float OffsetY = target.Col.size.y;

            return new Vector3(targetPos.x, OffsetY, targetPos.z);
        }
    }
}

