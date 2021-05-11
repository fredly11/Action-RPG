using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;
using UnityEngine.AI;

namespace RPG.Movement{
	public class Mover : MonoBehaviour, IAction {
		[SerializeField] float maxSpeed;
		NavMeshAgent navMeshAgent;
		Status status;

		void Start() 
		{
			navMeshAgent = GetComponent<NavMeshAgent>();
			status = GetComponent<Status>();
		}

		void Update () 
		{
			navMeshAgent.enabled = !status.IsDead();
			UpdateAnimator();
		}

		public void StartMoveAction(Vector3 destination, float speedFraction)
		{
			if(CanMove() == false){return;}
				GetComponent<ActionScheduler>().StartAction(this);
				MoveTo(destination, speedFraction);
			
		}

		public void MoveTo(Vector3 destination, float speedFraction)
		{
            navMeshAgent.destination = destination;
			navMeshAgent.speed = maxSpeed * Mathf.Clamp01(speedFraction);
			navMeshAgent.isStopped = false;
		}

		public void Cancel()
		{
			navMeshAgent.isStopped = true;
		}

		private void UpdateAnimator()
		{
			Vector3 velocity = navMeshAgent.velocity;
			Vector3 localVelocity = transform.InverseTransformDirection(velocity);
			float speed = localVelocity.z;
			GetComponent<Animator>().SetFloat("forwardSpeed", speed);
		}

		public bool CanMove(){
			if(status.IsDead())
			{
				return false;
			}
			else
			{
				return true;
			}
		}

	}
}
