using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class NormalUnitMove : UnitMove
    {
        public NormalUnitMove(float moveSpeed, Transform transform) : base(moveSpeed, transform)
        {
        }

        public override bool MoveTowards(Node nextNode)
        {
            Vector3 direction = (nextNode.WorldPosition - myTransform.position);
            if (direction.sqrMagnitude <= 0.005f)
            {
                myTransform.position = nextNode.WorldPosition;
                return true;
            }

            myTransform.position += direction.normalized * moveSpeed * Time.deltaTime;
            return false;
        }
    }
}

