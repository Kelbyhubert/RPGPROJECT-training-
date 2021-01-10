using System.Collections;
using System.Collections.Generic;
using RPG.Resources;
using UnityEngine.UI;
using UnityEngine;

namespace RPG.Combat
{
    
    public class EnemyHealth : MonoBehaviour
    {
        PlayerCombat targetHealth;

        private void Awake()
        {
            targetHealth = GameObject.FindWithTag("Player").GetComponent<PlayerCombat>();
        }

        private void Update()
        {
            if(targetHealth == null){
                GetComponent<Text>().text = "N/A";
            }
            Health health = targetHealth.getTarget();
            GetComponent<Text>().text = string.Format("Health : {0:0.0}%", health.showPercentage());
        }
    }
}

