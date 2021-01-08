using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace  RPG.Combat
{
    public class PickUpItem : MonoBehaviour
    {
        [SerializeField] Weapon weapon = null;
        [SerializeField] float respawnTime = 5;

        private void OnTriggerStay(Collider other) {
            // jika object ini collide dengan gameobject tag player dan input E maka akan mengambil object itu sesuai object yang di assign dalam weapon
            if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E)){
                other.GetComponent<PlayerCombat>().equipWeapon(weapon);
                StartCoroutine(SpawnTime(respawnTime));
            }
        }

        // respawn time (sementara karena belum pake inventory)

        IEnumerator SpawnTime(float second){

            showPickUp(false);
            yield return new WaitForSeconds(second);
            showPickUp(true);
        }

        private void showPickUp(bool show)
        {
            GetComponent<Collider>().enabled = show;
            foreach (Transform child in transform)
            {
                child.gameObject.SetActive(show);
            }
        }
    }
    
}
