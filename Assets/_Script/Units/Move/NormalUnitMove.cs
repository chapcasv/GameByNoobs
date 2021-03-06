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
            Vector3 direction = (nextNode.WorldPosition - myTransform.position);

            if (direction.sqrMagnitude <= 0.05f)
            {
                //Unit stay in next Node => return true

                myTransform.position = nextNode.WorldPosition;
                animator.SetBool(AnimEnum.IsMoving.ToString(), false);
                return true;
            }
            else 
            {
                //Unit moving to next Node => return false

                RotationFollow(nextNode, currentTarget);

                animator.SetBool(AnimEnum.IsMoving.ToString(), true);
                myTransform.position += moveSpeed * Time.deltaTime * direction.normalized;

                return false;
            }
        }

        protected virtual void RotationFollow(Node nextNode, BaseUnit currentTarget )
        {
            Vector3 directionNode = nextNode.WorldPosition - myTransform.position;
            Quaternion node = Quaternion.LookRotation(directionNode);
            _rigidbody.rotation = Quaternion.Lerp(_rigidbody.rotation, node, CHANGE_ROTATE_SPEED * Time.deltaTime);
        }
    }
}

