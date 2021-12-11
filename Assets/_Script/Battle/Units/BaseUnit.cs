using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;
using System.Linq;

namespace PH
{
    public class BaseUnit : MonoBehaviour
    {
        private float attackSpeed = 1f; //Attacks per second
        private float moveSpeed = 1f; //Attacks per second
        private bool canAttack;
        private Node currentNode;
        protected BaseUnit currentTarget;
        protected bool moving;
        protected Node destination;
        protected float waitBetweenAttack;

        public Node CurrentNode { get => currentNode; set => currentNode = value; }
        public bool CanAttack { get => canAttack; set => canAttack = value; }

        private void OnEnable()
        {
            Debug.Log("Enable");
        }

        public void Setup(Node spawnNode)
        {
            CurrentNode = spawnNode;
            transform.position = spawnNode.WorldPosition;
            spawnNode.SetOccupied(true);
        }

        protected void FindTarget()
        {
            //var allEnemies = GameManager.Instance.GetEntitiesAgainst(myTeam);
            List<BaseUnit> allEnemies = new List<BaseUnit>();

            float minDistance = Mathf.Infinity;
            BaseUnit entity = null;
            foreach (BaseUnit e in allEnemies)
            {
                if (Vector3.Distance(e.transform.position, this.transform.position) <= minDistance)
                {
                    minDistance = Vector3.Distance(e.transform.position, this.transform.position);
                    entity = e;
                }
            }

            currentTarget = entity;
        }

        protected bool MoveTowards(Node nextNode)
        {
            Vector3 direction = (nextNode.WorldPosition - this.transform.position);
            if (direction.sqrMagnitude <= 0.005f)
            {
                transform.position = nextNode.WorldPosition;

                return true;
            }

            this.transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            return false;
        }

        protected void GetInRange()
        {
            if (currentTarget == null)
                return;

            if (!moving)
            {
                destination = null;
                List<Node> candidates = GridBoard.GetNodesCloseTo(currentTarget.CurrentNode);
                candidates = candidates.OrderBy(x => Vector3.Distance(x.WorldPosition, this.transform.position)).ToList();
                for (int i = 0; i < candidates.Count; i++)
                {
                    if (!candidates[i].IsOccupied)
                    {
                        destination = candidates[i];
                        break;
                    }
                }
                if (destination == null)
                    return;

                var path = GridBoard.GetPath(CurrentNode, destination);
                if (path == null && path.Count >= 1)
                    return;

                if (path[1].IsOccupied)
                    return;

                path[1].SetOccupied(true);
                destination = path[1];
            }

            moving = !MoveTowards(destination);
            if (!moving)
            {
                //Free previous node
                CurrentNode.SetOccupied(false);
                CurrentNode = destination;
            }
        }

        public void TakeDamage(int amount)
        {
           
        }

        protected virtual void Attack()
        {
            if (!CanAttack)
                return;


            waitBetweenAttack = 1 / attackSpeed;
            StartCoroutine(WaitCoroutine());
        }

        IEnumerator WaitCoroutine()
        {
            CanAttack = false;
            yield return null;

            yield return new WaitForSeconds(waitBetweenAttack);
            CanAttack = true;
        }
    }
}

