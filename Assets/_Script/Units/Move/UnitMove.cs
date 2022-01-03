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
        protected Animator animator;
        protected Rigidbody rigidbody;

        public bool IsMoving { get; set ; }

        public UnitMove(float ms, Transform tf, Animator anim, Rigidbody rb)
        {
            moveSpeed = ms;
            myTransform = tf;
            animator = anim;
            IsMoving = false;
            rigidbody = rb;
        }

        public void SetMoveSpeed(float value) => moveSpeed = value;
        public abstract bool MoveTowards(Node nextNode, BaseUnit currentTarget);
    }
}

