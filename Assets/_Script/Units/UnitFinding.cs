using UnityEngine;
using PH.GraphSystem;

namespace PH
{
    public abstract class UnitFinding 
    {
        protected UnitTeam myTeam;
        protected Transform myTransform;
        protected BaseUnit currentTarget;

        public UnitFinding(UnitTeam team, Transform transform)
        {
            myTeam = team;
            myTransform = transform;
            currentTarget = null;
        }

        public abstract BaseUnit CurrentTarget();

        public abstract Node Destination(Node currentNode);

    }
}