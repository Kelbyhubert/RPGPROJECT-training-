using RPG.Movement;
using RPG.Core;
using UnityEngine;


namespace RPG.Combat
{
    public class PlayerCombat : MonoBehaviour , IAction {
        

        [SerializeField] float attackRange = 2f;
        Transform target;

        private void Update()
        {
            // jika ada target dan rangeCalculate nya false maka player bisa jalan
            // jika rangeCalculate true maka player akan berhenti
            // jika target null maka akan di return langsung tampa menjalankan sisanya

            if(target == null) return;

            if (!rangeCalculate())
            {
                GetComponent<mover>().moveTo(target.position);
            }
            else
            {
                GetComponent<mover>().cancelAction();
            }
        }

        private bool rangeCalculate()
        {
            //hitung selisih jarak target dengan player 
            // jika lebih kecil dari range maka akan return true
            return Vector3.Distance(target.position, transform.position) < attackRange;
        }

        public void attack(CombatTarget target){
            // untuk memhentikan current action dan lalu menganti action menjadi action class ini
            // lalu memulia action di class ini 
            // memasukan data target method ke dalam target di class 
            GetComponent<ActionScheduler>().StartAction(this);
            this.target = target.transform;
        }

        public void cancelAttack(){
            // reset target ke null
            this.target = null;
        }

        public void cancelAction()
        {
            //dari interface
            cancelAttack();
        }
    }
    
}