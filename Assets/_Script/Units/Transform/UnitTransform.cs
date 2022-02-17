using PH.GraphSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PH
{
    public class UnitTransform : MonoBehaviour
    {
        private readonly float speed = 3f;
        private Transform _enemyBase;

        public void SetUp(BaseUnit unitHolder, Node spawnNode, Transform enemyBase)
        {
            _enemyBase = enemyBase;
            SetUpRotation();
            SetUpPosByNode(unitHolder, spawnNode);
        }

        private void SetUpRotation()
        {   
            if(PhaseSystem.CurrentPhase is BeforeTeamFight)
            {   
                //ToWard Enemy
                Vector3 direction = _enemyBase.position - transform.position;
                transform.rotation = Quaternion.LookRotation(direction);
            }
            else
            {
                //ToWard Player
                transform.rotation = new Quaternion(0f, 180f, 0f, 0);
            }
        }

        private void SetUpPosByNode(BaseUnit unitHolder, Node spawnNode)
        {
            unitHolder.CurrentNode = spawnNode;
            transform.position = spawnNode.WorldPosition;
            spawnNode.SetOccupied(true);
        }

        public void RotationTowardEnemyBase()
        {
            StartCoroutine(RotationToward());
        }

        private IEnumerator RotationToward()
        {
            Vector3 direction = _enemyBase.position - transform.position;

            Quaternion enemyBase = Quaternion.LookRotation(direction);

            enemyBase = Quaternion.Euler(0, enemyBase.eulerAngles.y, 0);

            while (PhaseSystem.CurrentPhase is BeforeTeamFight)
            {
                Quaternion current = transform.rotation;
                transform.rotation = Quaternion.Slerp(current, enemyBase, speed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

