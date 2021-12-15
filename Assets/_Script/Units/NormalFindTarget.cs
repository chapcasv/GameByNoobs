using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PH
{

    public class NormalFindTarget : UnitFindTarget
    {   
        public NormalFindTarget(UnitTeam team, Transform transform): base(team,transform)
        {

        }

        public override BaseUnit GetCurrentTarget()
        {
            List<BaseUnit> allEnemies = DictionaryTeamBattle.GetUnitsAgainst(myTeam);

            float minDistance = Mathf.Infinity;
            BaseUnit entity = null;
            foreach (BaseUnit e in allEnemies)
            {
                if (Vector3.Distance(e.transform.position, myTransform.position) <= minDistance)
                {
                    minDistance = Vector3.Distance(e.transform.position, myTransform.position);
                    entity = e;
                }
            }

            return entity;
        }

        public override void GetInRange(BaseUnit currentTarget, Node currentNode, float moveSpeed, ref bool moving)
        {
            Node destination = null;
            if (currentTarget == null) return;

            if (!moving)
            {
                List<Node> candidates = GridBoard.GetNodesCloseTo(currentTarget.CurrentNode);
                candidates = candidates.OrderBy(x => Vector3.Distance(x.WorldPosition, myTransform.position)).ToList();
                for (int i = 0; i < candidates.Count; i++)
                {
                    if (!candidates[i].IsOccupied)
                    {
                        destination = candidates[i];
                        break;
                    }
                }
                if (destination == null) return;


                var path = GridBoard.GetPath(currentNode, destination);

                if (path == null && path.Count >= 1) return;

                if (path[1].IsOccupied) return;

                path[1].SetOccupied(true);
                destination = path[1];
            }

            moving = !MoveTowards(destination, moveSpeed);
            if (!moving)
            {
                //Free previous node
                currentNode.SetOccupied(false);
                currentNode = destination;
            }
            
        }

        protected bool MoveTowards(Node nextNode , float moveSpeed)
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

