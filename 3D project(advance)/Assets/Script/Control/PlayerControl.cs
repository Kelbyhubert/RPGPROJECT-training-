using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;
using System;
using RPG.Resources;

namespace RPG.Control
{
    // class ini berfungsi untuk input data ke dalam method dari class lain yang diperlukan untuk gameObject 
    public class PlayerControl : MonoBehaviour 
    {
        Health health;
        private void Start() {
            health = GetComponent<Health>();
        }
        
        
        private void Update() {
            // membuat priority , jika click target yang bisa diserang maka prioritynya akan menjadi combatControl , jika tidak maka moveControl akan menjadi prioritynya
            // jika click target maka combatControl akan menjadi true dan melakukan method combat Control dan langsung return untuk tidak mengakses method moveControl
            // jika combatControl return false maka method combat control tidak ada dilakukan dan melakukan moveControl
            // jika klik diluar world maka player tidak akan melakukan apa apa

            if(health.isDead()) return;
            if(combatControl()) return;
            if(moveControl()) return;
            Debug.Log("out of bound");
        
        }

        private Boolean combatControl()
        {
            //mengambil semua object yang terkena laser atau ray (tebus object) RaycastAll
            //memasukan semua informasi ke dalam array Raycasthit
            //looping dipake untuk mengambil informasi hit ke dalam target terus menerus 
            //(ini buat jadi handler/checker bila gameObject tersebut tidak ada atau ada komponen combatTarget karena nanti yang diambil dari target adalah gameObjectnya)
            //jika target tidak ada komponen combatTarget maka index tersebut akan diskip dan lanjut ke index selanjutnya
            //jika tidak bisa diserang maka index tersebut akan di skip dan lanjut ke index selanjutnya
            //jika ada target dan click mouse 0 maka akan melakukan method attack 
            RaycastHit[] hitArrays = Physics.RaycastAll(getRay());
            foreach (RaycastHit hit in hitArrays)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(target == null) continue;

                if(!GetComponent<PlayerCombat>().canAttack(target.gameObject)) continue;

                if(Input.GetMouseButton(0))
                {
                    GetComponent<PlayerCombat>().attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool moveControl(){

            // memasukan posisi click dari mouse ke dalam Ray 
            // memasukan data posisi ray ke dalam raycasthit yang akan dipakai sebagai target (Physics.Raycast())
            // hit.point = impact point in the world space where ray hit the collider
            // cancelAttack untuk mereset target menjadi null
            RaycastHit hit;

            bool hashit = Physics.Raycast(getRay(), out hit);

            if (hashit)
            {
                if(Input.GetMouseButton(0)){
                    GetComponent<mover>().startMove(hit.point ,1f);
                }
                return true;
            }
            return false;

        }

        private static Ray getRay()
        {
            // memasukan posisi click dari mouse ke dalam Ray
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
    
}