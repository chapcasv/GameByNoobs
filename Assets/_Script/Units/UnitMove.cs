using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public abstract class UnitMove
    {
        protected float moveSpeed;
        protected Transform myTransform;

        public bool IsMoving { get; set ; }

        public UnitMove(float moveSpeed, Transform transform)
        {
            this.moveSpeed = moveSpeed;
            myTransform = transform;
            IsMoving = false;
        }

        public void SetMoveSpeed(float value) => moveSpeed = value;
        public abstract bool MoveTowards(Node nextNode);
    }
}

