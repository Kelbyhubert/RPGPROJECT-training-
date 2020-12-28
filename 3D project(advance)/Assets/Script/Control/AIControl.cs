using RPG.Combat;
using RPG.Core;
using RPG.Movement;
using UnityEngine;


namespace RPG.Control
{
    public class AIControl : MonoBehaviour {
        
        [SerializeField] float triggerRange = 5;
        PlayerCombat combatBehaviour;
        GameObject player;
        Health health;

        Vector3 guardPosition;

        
        private void Start() {
            // buat ambil component biar tidak kepanjangan di method
            combatBehaviour = GetComponent<PlayerCombat>();
            player = GameObject.FindWithTag("Player");
            health = GetComponent<Health>();

            guardPosition = transform.position;
            
        }


        private void Update()
        {
            //jika player didalam range AI dan player tersebut bisa diserang maka akan melakukan attack ke player
            // karena cara serang AI enemy sama dengan cara serang Player maka bisa dipakai method yang di dalam class PlayerCombat
            //jika sudah tidak pada range atau sudah mati maka AI enemy akan balik ke posisi awal

            if(health.isDead()) return;
            
            if(inRange() && combatBehaviour.canAttack(player))
            {
                combatBehaviour.attack(player);
            }else{
                
                GetComponent<mover>().startMove(guardPosition);

            }
                
        }


        private bool inRange()
        {
            // hitung selisih range posisi object ini dengan object dengan tag Player
            // jika calculateDistance nya lebih kecil dari triggerRange maka return true jika tidak return false
            float calculateDistance = Vector3.Distance(player.transform.position, transform.position);

            return calculateDistance < triggerRange;
        }


        private void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position,triggerRange);
        }
    }
    
}
