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
        protected UnitFinding Find;
        protected UnitMove Move;
        protected UnitAttack Atk;

        protected IHealth Health;
        protected IMana Mana;

        protected Node currentNode;
        protected UnitTeam _myTeam;
        protected bool inTeamFight = false;
        protected BaseUnit currentTarget;
        protected Node destination;

        protected bool HasEnemy => currentTarget != null;
        public Node CurrentNode { get => currentNode; set => currentNode = value; }
        public bool IsLive { get => Health.IsLive; }
        public bool InTeamFight { set => inTeamFight = value; }

        protected virtual void Update()
        {
            if (!inTeamFight) return;

            if (!HasEnemy) currentTarget = Find.CurrentTarget();

            if (!Mana.IsFullMana())
            {
                AttackInRange();
            }
            else CastSkillInRange();
        }

        public virtual void Setup(Node spawnNode, CardUnit unit, UnitTeam team)
        {
            _myTeam = team;
            CurrentNode = spawnNode;
            transform.position = spawnNode.WorldPosition;
            spawnNode.SetOccupied(true);

            SetUpHealthSystem(unit, team);
            SetUpManaSystem(unit);
            SetUpAttack(unit);
            SetUpMove(unit);
            SetUpFinding(team);
        }

        protected virtual void SetUpFinding(UnitTeam team) => Find = new NormalFinding(team, transform);

        protected virtual void SetUpMove(CardUnit unit) => Move = new NormalUnitMove(unit.MoveSpeed, transform);

        protected virtual void SetUpAttack(CardUnit unit)
        {
            Atk = gameObject.AddComponent(typeof(NormalUnitAtk)) as NormalUnitAtk;
            Atk.Constructor(unit.AtkSpeed, unit.Range, unit.Damage, unit.CritRate);
        }

        protected virtual void SetUpHealthSystem(CardUnit unit, UnitTeam myTeam)
        {
            Health = GetComponent<IHealth>();
            Health.SetUp(unit.Hp, unit.Armor, unit.MagicResist, myTeam);
            Health.OnDie += Die;
        }

        protected virtual void SetUpManaSystem(CardUnit unit)
        {
            Mana = GetComponent<IMana>();
            Mana.SetMana(unit.ManaMax, unit.ManaStart, unit.ManaRegen);
        }

        protected void GetInRange()
        {
            if (currentTarget == null)
                return;

            if (!Move.IsMoving)
            {
                destination = Find.Destination(currentNode);
            }

            Move.IsMoving = !Move.MoveTowards(destination);

            if (!Move.IsMoving)
            {
                //Free previous node
                CurrentNode.SetOccupied(false);
                CurrentNode = destination;
            }
        }


        protected virtual void AttackInRange()
        {
            if (Atk.IsInRange(currentTarget) && Move.IsMoving)
            {
                if (Atk.CanAtk)
                {
                    Atk.Attack(currentTarget);
                    Mana.IncreaseMana();
                }
                
            }
            else GetInRange();
        }

        protected virtual void CastSkillInRange()
        {

        }

        public virtual void TakeDamage(int amount)
        {
            Health.TakeDamage(amount);
            Mana.IncreaseMana();
        }

        protected virtual void Die()
        {
            DictionaryTeamBattle.RemoveUnit(_myTeam, this);
            currentNode.SetOccupied(false);
            Destroy(gameObject);
        }


    }
}
