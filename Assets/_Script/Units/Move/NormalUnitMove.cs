using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class NormalUnitMove : UnitMove
    {
        public override bool MoveTowards(Node nextNode,BaseUnit currentTarget)
        {
            if (nextNode == null ) return false;

            Vector3 direction = (nextNode.WorldPosition - myTransform.position);

            //Unit stay in next Node => return true
            if (direction.sqrMagnitude <= 0.05f)
            {
                myTransform.position = nextNode.WorldPosition;
                animator.SetBool(AnimEnum.IsMoving.ToString(), false);
                return true;
            }
            else //Unit moving to next Node => return false
            {
                RotationFollow(nextNode);

                animator.SetBool(AnimEnum.IsMoving.ToString(), true);
                myTransform.position += direction.normalized * moveSpeed * Time.deltaTime;

                return false;
            }
        }

        protected virtual void RotationFollow(Node nextNode)
        {
            Vector3 directionNode = nextNode.WorldPosition - myTransform.position;

            _rigidbody.rotation = Quaternion.LookRotation(directionNode);
        }
    }
}

