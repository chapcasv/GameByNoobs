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

            if (direction.sqrMagnitude <= 0.005f)
            {
                myTransform.position = nextNode.WorldPosition;
                animator.SetBool(AnimEnum.IsMoving.ToString(), true);
                return true;
            }

            RotationFollow(currentTarget);

            animator.SetBool(AnimEnum.IsMoving.ToString(), true);
            myTransform.position += direction.normalized * moveSpeed * Time.deltaTime;

            return false;
        }

        protected virtual void RotationFollow(BaseUnit currentTarget)
        {
            Vector3 direction = currentTarget.CurrentNode.WorldPosition - myTransform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            GetComponent<Rigidbody>().rotation = rotation;
        }
    }
}

