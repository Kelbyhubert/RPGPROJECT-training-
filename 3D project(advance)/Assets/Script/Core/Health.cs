using RPG.Saving;
using UnityEngine;



namespace RPG.Core
{
    public class Health : MonoBehaviour ,ISaveable
    {
        
        [SerializeField] float totalHealth = 100;
        bool dead = false;


        public bool isDead(){
            return dead;
        }


        public void takeDamage(float amountOfDamage){
            //mathf.max untuk membuat value tidak bisa dibawah 0
            if(dead == true) return;
            totalHealth = Mathf.Max(totalHealth - amountOfDamage, 0);
            Debug.Log(transform.name + " total Health : " + totalHealth);
            die();
        }

        private void die(){
            
            if(totalHealth <= 0){
                dead = true;
                GetComponent<Animator>().SetTrigger("diaAnimTrigger");
                print(transform.name + " die");
                GetComponent<ActionScheduler>().cancelCurrentAction();
                // GetComponent<CapsuleCollider>().enabled = false;
            }
        }


        public object CaptureState()
        {
        // untuk mengambil data buat di Save
            return totalHealth;
        }
        public void RestoreState(object state)
        {
        // mengubah value variable dari data yang di save file
            this.totalHealth = (float)state;
            die();
        }
    }
    
}

