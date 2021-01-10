using System;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;



namespace RPG.Resources
{
    public class Health : MonoBehaviour ,ISaveable
    {
        // total health di jadiin -1f untuk jadi checket
        [SerializeField] float totalHealth = -1f;
        bool dead = false;

        private void Start() {
            // ini dijalankan jika restore sudah dijalakan
            // jika total health -1f maka akan totalhealth akan sesuai dengan stat dari lvl
            if(totalHealth < 0){
            totalHealth = GetComponent<BaseStats>().GetComponent<BaseStats>().getStats(Stat.Health);
            }
        }

        public float showPercentage(){
            return 100 * (totalHealth / GetComponent<BaseStats>().getStats(Stat.Health));
        }

        public bool isDead(){
            return dead;
        }


        public void takeDamage(float amountOfDamage , GameObject whoAttack){
            //mathf.max untuk membuat value tidak bisa dibawah 0
            // whoAttack untuk memberi tau siapa yang membunuh musuhnya maka dia yang akan dapat xpnya 
            if(dead == true) return;
            totalHealth = Mathf.Max(totalHealth - amountOfDamage, 0);
            Debug.Log(transform.name + " total Health : " + totalHealth);

            if(totalHealth <= 0){
            die();
            getEXP(whoAttack);
            }
        }

        private void getEXP(GameObject whoAttack)
        {
            Experience EXP = whoAttack.GetComponent<Experience>();
            if(EXP == null) return;

            EXP.gainEXP(GetComponent<BaseStats>().getStats(Stat.EXPGain));
        }

        private void die(){
            
            if(dead) return;
                
                dead = true;
                GetComponent<Animator>().SetTrigger("diaAnimTrigger");
                print(transform.name + " die");
                GetComponent<ActionScheduler>().cancelCurrentAction();
                // GetComponent<CapsuleCollider>().enabled = false;
            }

        
            
        public object CaptureState()
        {
        // untuk mengambil data buat di Save
            return totalHealth;
        }

        public void RestoreState(object state)
        {
            // ini duluan dijalankan untuk set health nya sekalian buat checker
        // mengubah value variable dari data yang di save file
            this.totalHealth = (float)state;
            if (totalHealth <= 0)
            {
                die();
               
            }
        }
        }


    }
    



    


