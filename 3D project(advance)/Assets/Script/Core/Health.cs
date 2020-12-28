using UnityEngine;


namespace RPG.Core
{
    public class Health : MonoBehaviour {
        
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
    }
    
}

