using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core{
public class Status : MonoBehaviour
{
    [SerializeField] float healthPoints = 100f;
    bool isDead = false;
    Animator animator;
    ActionScheduler actionScheduler;

    private void Start() {
        animator = GetComponent<Animator>();
        actionScheduler = GetComponent<ActionScheduler>();
    }

    public bool IsDead(){
        return isDead;
    }

    public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            if(healthPoints == 0){
              Die();
            }
        }

        private void Die()
        {
            if (isDead == true){return;}
            animator.SetTrigger("die");
            actionScheduler.CancelCurrentAction();
            isDead = true;
            print(isDead);
            
        }
    }
}