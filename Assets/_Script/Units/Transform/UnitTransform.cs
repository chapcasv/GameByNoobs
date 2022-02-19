using PH.GraphSystem;
using System.Collections;
using UnityEngine;

namespace PH
{
    public class UnitTransform : MonoBehaviour
    {
        private readonly float speed = 3f;
        private readonly float smooth = 0.05f;
        private Transform _enemyBase;

        public void SetUp(BaseUnit unitHolder, Node spawnNode, Transform enemyBase)
        {
            _enemyBase = enemyBase;
            SetUpPosByNode(unitHolder, spawnNode);
            SetUpRotation();
        }

        private void SetUpRotation()
        {
            if (PhaseSystem.CurrentPhase is BeforeTeamFight)
            {
                Quaternion enemyBase = LookToWardEnemyBase();
                transform.rotation = enemyBase;
            }
            else
            {
                //ToWard Player
                transform.rotation = new Quaternion(0f, 180f, 0f, 0);
            }
        }

        private Quaternion LookToWardEnemyBase()
        {
            Vector3 direction = _enemyBase.position - transform.position;
            Quaternion enemyBase = Quaternion.LookRotation(direction);

            //Freeze x,z
            enemyBase = Quaternion.Euler(0, enemyBase.eulerAngles.y, 0);
            return enemyBase;
        }

        private void SetUpPosByNode(BaseUnit unitHolder, Node spawnNode)
        {
            unitHolder.CurrentNode = spawnNode;
            spawnNode.SetOccupied(true);
            transform.position = spawnNode.WorldPosition;
        }

        public void RotationTowardEnemyBase()
        {
            StartCoroutine(RotationToward());
        }

        private IEnumerator RotationToward()
        {
            Quaternion enemyBase = LookToWardEnemyBase();

            while (PhaseSystem.CurrentPhase is BeforeTeamFight)
            {
                Quaternion current = transform.rotation;
                transform.rotation = Quaternion.Slerp(current, enemyBase, speed * Time.deltaTime);
                yield return new WaitForSeconds(smooth * Time.deltaTime);
            }
        }        
    }
}

