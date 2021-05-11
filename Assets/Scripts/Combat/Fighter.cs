using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat{
    public class Fighter : MonoBehaviour, IAction {
      Status target;
      bool isInRange;
      Mover mover;
      Animator animator;
      Status status;
      [SerializeField] float weaponRange = 2f;
      [SerializeField] float timeBetweenAttacks = 1f;
      [SerializeField] float weaponDamage = 5f;
      float timeSinceLastAttack = Mathf.Infinity;


      void Start()
        {
            mover = GetComponent<Mover>();
            animator = GetComponent<Animator>();
            status = GetComponent<Status>();
        }
           
        private void Update()
        {
          timeSinceLastAttack += Time.deltaTime;
          if (target == null) return;
          if (target.IsDead()) return;

          if (!GetIsInRange())
          {
            mover.MoveTo(target.transform.position, 1f);
          }
          else
          {
            mover.Cancel();
            AttackBehaviour();
          }
        }

        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);
            if(timeSinceLastAttack > timeBetweenAttacks)
            {
                // This will trigger the Hit event.
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            animator.ResetTrigger("stopAttack");
            animator.SetTrigger("attack");
        }

        // Animation Event
        void Hit()
        {
            if(target == null){return;}
            target.TakeDamage(weaponDamage);
        }

        private bool GetIsInRange()
        {
          return Vector3.Distance(transform.position, target.transform.position) < weaponRange;
        }

        public void Attack(GameObject combatTarget){
          GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Status>();
        }

        public void Cancel()
        {
            TriggerStopAttack();
            target = null;
        }

        private void TriggerStopAttack()
        {
            animator.ResetTrigger("attack");
            animator.SetTrigger("stopAttack");
        }

        public bool CanAttack(GameObject combatTarget){
          if(combatTarget == null || status.IsDead()){return false;}
          Status targetToTest = combatTarget.GetComponent<Status>();
          return targetToTest != null && !targetToTest.IsDead();
        }


    }
}