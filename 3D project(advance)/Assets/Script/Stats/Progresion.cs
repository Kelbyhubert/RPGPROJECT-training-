using UnityEngine;

namespace RPG.Stats
{
    [CreateAssetMenu(fileName = "Progresion", menuName = "Progresion/Create new Progresion", order = 0)]
    public class Progresion : ScriptableObject {
        [SerializeField] CharacterProgression[] characterClasses = null;

        //buat database stat yang akan di pass ke player atau musuh sesuai lv 
        // mungkin ini harus ntn video nya baru bisa ngerti
        public float getStat(Stat stat , CharacterClass characterClass , int level){
            
            foreach ( CharacterProgression c in characterClasses)
            {
                if(c.characterClass != characterClass) continue;

                foreach (ProgresionStats s in c.stats)
                {
                    if(s.stat != stat) continue;

                    if(s.level.Length < level) continue;

                    return s.level[level - 1];
                }  
            }
            return 0;
        }

        [System.Serializable]
        class CharacterProgression{
            public CharacterClass characterClass;
            public ProgresionStats[] stats;
            // public float[] health;
        }

        [System.Serializable]
        class ProgresionStats{
            public Stat stat;
            public int[] level;
        }
    }
    
}
