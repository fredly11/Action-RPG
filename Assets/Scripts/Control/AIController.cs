using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;
using RPG.Core;


namespace RPG.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] bool isGuard;
        [SerializeField] float chaseDistance = 5f;
        [SerializeField] float suspicionTime = 5f;
        [SerializeField] float waitTime = 4f;
        [SerializeField] PatrolPath patrolPath;
        [SerializeField] float waypointTolerance = 1f;
        [SerializeField] float pauseChance;
        [SerializeField] float forceChangeWaypointTime;
        [Range(0,1)]
        [SerializeField] float walkSpeedFraction = .625f;
        Fighter fighter;
        Mover mover;
        GameObject player;
        Vector3 guardPosition;
        float targetDistance;
        float timeSinceReturnedToPatrol = Mathf.Infinity;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        float timeSinceReachedWaypoint = Mathf.Infinity;
        int currentWaypointIndex = 0;
        bool isReturningToPatrol = false;
        

        private void Start() {
            fighter = GetComponent<Fighter>();
            mover = GetComponent<Mover>();
            player = GameObject.FindWithTag("Player");
            if(isGuard){
                guardPosition = transform.position;
            }
        }

        private void Update()
        {

            if (InAggroRange(player) && fighter.CanAttack(player))
            {
                AttackBehavior();
            }
            else if (isGuard && timeSinceLastSawPlayer < suspicionTime)
            {
                GuardSuspicionBehavior();
            }
            else if (isGuard && timeSinceLastSawPlayer >= suspicionTime)
            {
                PatrolBehavior();
            }
            else
            {
                CancelBehavior();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceReturnedToPatrol += Time.deltaTime;
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceReachedWaypoint += Time.deltaTime;
        }

        private void CancelBehavior()
        {
            fighter.Cancel();
            if(patrolPath != null)
            {
                isReturningToPatrol = true;
                timeSinceReturnedToPatrol = 0;
            }
            
        }

        private void PatrolBehavior()
        {
            Vector3 nextPosition = guardPosition;
            if(patrolPath != null)
            {
                if(isReturningToPatrol && timeSinceReturnedToPatrol > forceChangeWaypointTime){
                    CycleWaypoint();
                }
                nextPosition = patrolPath.GetWaypoint(currentWaypointIndex);
                if(AtWaypoint())
                {
                    if(isReturningToPatrol)
                    {
                        isReturningToPatrol = false;
                    }
                    float random = UnityEngine.Random.value;
                    if(random < pauseChance){
                        timeSinceReachedWaypoint = 0;
                    }
                    CycleWaypoint();
                } else
                {
                    nextPosition = GetCurrentWaypoint();
                }


            }
            if (timeSinceReachedWaypoint > waitTime)
            {
                mover.StartMoveAction(nextPosition, walkSpeedFraction);
            }
        }

        private bool AtWaypoint()
        {
            float distanceToWaypoint = Vector3.Distance(transform.position, GetCurrentWaypoint());
            return distanceToWaypoint < waypointTolerance;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex);
        }

        private void GuardSuspicionBehavior()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void AttackBehavior()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        private bool InAggroRange(GameObject target)
        {
            targetDistance = Vector3.Distance(target.transform.position, transform.position);
            return targetDistance <= chaseDistance;

        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }

}