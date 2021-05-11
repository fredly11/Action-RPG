using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using RPG.Control;


namespace RPG.Core{
public class PartyManager : MonoBehaviour
{
        [SerializeField] Text text;
        [SerializeField] UISound uiSound;
        GameObject selectedCharacter;
        
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                SelectCharacter();
            }
        }

        public void SelectCharacter(){
                
            if(selectedCharacter){
                if(selectedCharacter.GetComponent<PlayerController>().isSelected == true){
                    uiSound.Deselect();
                }
                selectedCharacter.GetComponent<PlayerController>().isSelected = false;

            }
            RaycastHit[] hits = Physics.RaycastAll(PlayerController.GetMouseRay());
            foreach (RaycastHit item in hits)
            {
                if (item.collider.tag == "Player")
                {
                    selectedCharacter = item.collider.gameObject;
                    text.text = ("Selected: " + selectedCharacter.name);
                    selectedCharacter.GetComponent<PlayerController>().isSelected = true;
                    uiSound.SelectCharacter();
                    return;
                }
            }

        
        }
    }
}
