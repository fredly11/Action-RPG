using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Movement;
using System;
using RPG.Combat;

namespace RPG.Control{
    public class PlayerController : MonoBehaviour
    {
        public bool isSelected;

        // Update is called once per frame
        void Update()
        {
            if(isSelected){
                if(InteractWithCombat()) return;
                if(InteractWithMovement()) return;
                print("Nothing to do");
            }       
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit item in hits)
            {
                CombatTarget target = item.collider.gameObject.GetComponent<CombatTarget>();
                if(target == null){continue;}

               if(!GetComponent<Fighter>().CanAttack(target.gameObject))
               {
                   continue;
               }
               if(Input.GetMouseButton(1)){
                   GetComponent<Fighter>().Attack(target.gameObject);
               }
                return true; 
            }
            return false;
        }


        private bool InteractWithMovement()
        {
            RaycastHit hit;
            bool hasHit = Physics.Raycast(GetMouseRay(), out hit);
            if (hasHit)
            {
                if (Input.GetMouseButton(1))
                {
                GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                }
                return true;
            }
            return false;
        }

        public static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
