using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace  RPG.Combat
{
    public class PickUpItem : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;

        private void OnTriggerStay(Collider other) {
            // jika object ini collide dengan gameobject tag player dan input E maka akan mengambil object itu sesuai object yang di assign dalam weapon
            if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E)){
                other.GetComponent<PlayerCombat>().equipWeapon(weapon);
                Destroy(gameObject);
            }
        }
        
    }
    
}
