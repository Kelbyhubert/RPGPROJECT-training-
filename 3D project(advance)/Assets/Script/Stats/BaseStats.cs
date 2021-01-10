using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    // class stats
    public class BaseStats : MonoBehaviour
    {
        // akan menjadi component gameobject player dan musuh
        [Range(1,99)]
        [SerializeField] int level = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progresion progresion = null;

        public float getStats(Stat stat){
            return progresion.getStat(stat , characterClass,level);
        }

        
    }
    
}

