using UnityEngine;


namespace RPG.Combat
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
            Debug.Log("total Health : " + totalHealth);
            die();
        }

        private void die(){
            if(totalHealth <= 0){
                GetComponent<Animator>().SetTrigger("diaAnimTrigger");
                // GetComponent<CapsuleCollider>().enabled = false;
                dead = true;
            }
        }
    }
    
}

