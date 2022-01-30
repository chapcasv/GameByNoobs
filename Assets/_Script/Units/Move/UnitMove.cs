using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public abstract class UnitMove : MonoBehaviour
    {
        protected float moveSpeed;
        protected Transform myTransform;
        protected Animator animator;
        protected Rigidbody _rigidbody;

        public bool IsMoving { get; set ; }
        public bool CanMove { get; set; }

        public void SetUp(float ms, Transform tf, Animator anim, Rigidbody rb)
        {
            moveSpeed = ms;
            myTransform = tf;
            animator = anim;
            IsMoving = false;
            _rigidbody = rb;
            CanMove = true;
        }

        public void SetMoveSpeed(float value) => moveSpeed = value;


        public void Airbone(float time)
        {
            StartCoroutine(AirboneAnim(time));
        }

        private IEnumerator AirboneAnim(float time)
        {   
            //timeFly = 3/4 total time 
            float timeFly = time * (3/4);
            float timeDropGround = time * (1 / 4);

            myTransform.position += new Vector3(0, 1);
            yield return new WaitForSeconds(timeFly);
        }

        private void Up(float timeFly)
        {
            myTransform.position += new Vector3(0, 1);
        }

        public abstract bool MoveTowards(Node nextNode, BaseUnit currentTarget);
    }
}

