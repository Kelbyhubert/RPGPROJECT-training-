using UnityEngine;

using System.Collections;

namespace RPG.SceneManagement
{
    
    public class Fader : MonoBehaviour {
        
        // component UI buat fadernya
        CanvasGroup canvasFader;

        private void Start() {
            // mengambil componentnya biar tidak panjang
            canvasFader = GetComponent<CanvasGroup>();
        }

        // rumus
        // alpha = 1 * deltaTime / time 

        public IEnumerator fadeOut(float time){
            // jika alpha dari canvas lebih kecil dari 1 maka akan melakukan methodnya untuk menambah alphanya
            // return nya null karena yang dibutuhkan hanya prosesnya dari methodnya bukan hasil
            while(canvasFader.alpha < 1){
                canvasFader.alpha += Time.deltaTime / time;
                yield return null;
            }
        }

        public IEnumerator fadeIn(float time)
        {
            // jika alpha dari canvas lebih besar dari 0 maka akan melakukan methodnya untuk mengurangi alphanya
            // return nya null karena yang dibutuhkan hanya prosesnya dari methodnya bukan hasil
            while (canvasFader.alpha > 0)
            {
                canvasFader.alpha -= Time.deltaTime / time;
                yield return null;
            }
        }
    }
    
}