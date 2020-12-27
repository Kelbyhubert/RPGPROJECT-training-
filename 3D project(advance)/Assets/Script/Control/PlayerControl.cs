using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using System;

namespace RPG.Control
{
    public class PlayerControl : MonoBehaviour 
    {
        
        
        private void Update() {
            // membuat priority , jika click target yang bisa diserang maka prioritynya akan menjadi combatControl , jika tidak maka moveControl akan menjadi prioritynya
            // jika click target maka combatControl akan menjadi true dan melakukan method combat Control dan langsung return untuk tidak mengakses method moveControl
            // jika combatControl return false maka method combat control tidak ada dilakukan dan melakukan moveControl
            // jika klik diluar world maka player tidak akan melakukan apa apa
            if(combatControl()) return;
            if(moveControl()) return;
            Debug.Log("out of bound");
        
        }

        private Boolean combatControl()
        {
            //mengambil semua object yang terkena laser atau ray (tebus object) RaycastAll
            //memasukan semua informasi ke dalam array Raycasthit
            //looping dipake untuk mengambil informasi hit ke dalam target terus menerus 
            //jika tidak bisa diserang maka index tersebut akan di skip dan lanjut ke index selanjutnya
            //jika ada target dan click mouse 0 maka akan melakukan method attack 
            RaycastHit[] hitArrays = Physics.RaycastAll(getRay());
            foreach (RaycastHit hit in hitArrays)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if(!GetComponent<PlayerCombat>().canAttack(target)) continue;

                if(Input.GetMouseButtonDown(0))
                {
                    GetComponent<PlayerCombat>().attack(target);
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
                 GetComponent<mover>().startMove(hit.point);
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