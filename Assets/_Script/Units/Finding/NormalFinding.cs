using PH.GraphSystem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace PH
{

    public class NormalFinding : UnitFinding
    {   
        public NormalFinding(UnitTeam team, Transform transform): base(team,transform)
        {

        }

        public override BaseUnit CurrentTarget()
        {
            List<BaseUnit> allEnemies = DictionaryTeamBattle.GetUnitsAgainst(myTeam);

            float minDistance = Mathf.Infinity;
            BaseUnit entity = null;

            foreach (BaseUnit e in allEnemies)
            {
                float distance = Vector3.Distance(e.transform.position, myTransform.position);
                if (distance <= minDistance && e.IsLive)
                {
                    minDistance = distance;
                    entity = e;
                }
            }
            currentTarget = entity; //cache for find destination
            return entity;
        }

        public override Node Destination(Node currentNode)
        {
            Node destination = null;
            List<Node> candidates = GetCandidates();

            for (int i = 0; i < candidates.Count; i++)
            {
                if (!candidates[i].IsOccupied)
                {
                    destination = candidates[i];
                    break;
                }
            }

            if (destination == null) return destination;

            var path = GridBoard.GetPath(currentNode, destination);

            if (path == null) return destination;
            if (path[1].IsOccupied) return destination;

            //path[0] is current node
            path[1].SetOccupied(true);
            destination = path[1];

            return destination;
        }

        private List<Node> GetCandidates()
        {
            List<Node> candidates = GridBoard.GetNodesCloseTo(currentTarget.CurrentNode);
            candidates = candidates.OrderBy(x => Vector3.Distance(x.WorldPosition, myTransform.position)).ToList();
            return candidates;
        }
    }
}

