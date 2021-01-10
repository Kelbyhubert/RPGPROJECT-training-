using UnityEngine;
using RPG.Saving;

namespace RPG.Resources
{
    public class Experience : MonoBehaviour , ISaveable {
        
        [SerializeField] float EXPoint = 0;


        public void gainEXP(float xp){
            EXPoint += xp;
        }

        public object CaptureState()
        {
            return EXPoint;
        }
        public void RestoreState(object state)
        {
            EXPoint = (float)state;
        }
    }
    
}
