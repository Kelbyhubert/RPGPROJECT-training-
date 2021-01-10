using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class HeatlhDisplay : MonoBehaviour {
        Health health;
        
        private void Awake() {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update() {
            GetComponent<Text>().text = string.Format("Health : {0:0.0}%" , health.showPercentage());
        }
    }
    
}