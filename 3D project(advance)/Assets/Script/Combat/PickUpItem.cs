using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace  RPG.Combat
{
    public class PickUpItem : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;

        private void OnTriggerStay(Collider other) {
            if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E)){
                other.GetComponent<PlayerCombat>().equipWeapon(weapon);
                Destroy(gameObject);
            }
        }
        
    }
    
}
