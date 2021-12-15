using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PH.GraphSystem;
using System.Linq;
using SO;

namespace PH
{
    public class BaseUnit : MonoBehaviour
    {
        protected UnitFindTarget FindTarget;
        protected UnitMove Move;
        protected UnitAttack Attack;
        
        protected IUnitHealth Health;
        protected IUnitSkillPoint SkillPoint;
        
        protected Node currentNode;
        protected UnitTeam _myTeam;
        protected bool inTeamFight = false;
        protected BaseUnit currentTarget;
        protected Node destination;
        protected bool HasEnemy => currentTarget != null;      
        public Node CurrentNode { get => currentNode; set => currentNode = value; }
        public bool Dead { get; set ;}
        public bool InTeamFight {set => inTeamFight = value; }

        public virtual void Setup(Node spawnNode, CardUnit unit, UnitTeam team)
        {
            _myTeam = team;
            CurrentNode = spawnNode;
            transform.position = spawnNode.WorldPosition;
            spawnNode.SetOccupied(true);

            SetUpHealth(unit.Hp, team);
            SetUpSkillPoint(unit.SpMax, unit.SpStart, unit.SpRegen);
            SetUpAttack(unit);
            SetUpMove(unit);
            SetUpFindTarget(team);
        }

        private void SetUpFindTarget(UnitTeam team) => FindTarget = new NormalFindTarget(team, transform);

        private void SetUpMove(CardUnit unit) => Move = new NormalUnitMove(unit.MoveSpeed, transform);

        private void SetUpAttack(CardUnit unit)
        {
            Attack = gameObject.AddComponent(typeof(NormalUnitAtk)) as NormalUnitAtk;
            Attack.Constructor(unit.AtkSpeed, unit.Range, unit.Str);
        }

        protected virtual void SetUpHealth(int maxHP,UnitTeam myTeam) 
        {
            Health = GetComponent<IUnitHealth>();
            Health.SetHP(maxHP, myTeam);
        }

        protected virtual void SetUpSkillPoint(int maxSP, int startSP, int spRegen)
        {
            SkillPoint = GetComponent<IUnitSkillPoint>();
            SkillPoint.SetSP(maxSP, startSP, spRegen);
        }

        protected void GetInRange()
        {
            if (currentTarget == null)
                return;

            if (!Move.IsMoving)
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

            Move.IsMoving = !Move.MoveTowards(destination);
            if (!Move.IsMoving)
            {
                //Free previous node
                CurrentNode.SetOccupied(false);
                CurrentNode = destination;
            }

            //FindTarget.GetInRange(currentTarget, currentNode, moveSpeed,ref moving);
        }

        public virtual void AttackTarget()
        {
            Attack.Atk(currentTarget);
            SkillPoint.IncreaseSP();
        }

        public virtual void TakeDamage(int amount)
        {
            Health.TakeDamage(amount, this);
            SkillPoint.IncreaseSP();
        }
 
    }
}

