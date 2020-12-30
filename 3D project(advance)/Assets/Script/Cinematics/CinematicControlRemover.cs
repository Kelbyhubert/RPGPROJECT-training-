using UnityEngine;
using UnityEngine.Playables;
using RPG.Core;
using RPG.Control;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour {

            GameObject player;
        
        private void Start() {
            //observerPattern
            // unity Event 
            // seperti memasukan method ke sebuah list 
            // jika scene sedang play maka akan menjalankan method disableControl
            // jika scene sudah selesai atau berhenti maka akan menjalakan method enableControl
            GetComponent<PlayableDirector>().played += disableControl;
            GetComponent<PlayableDirector>().stopped += enableControl;
            player = GameObject.FindGameObjectWithTag("Player");

        }


        void disableControl(PlayableDirector pd){
            
            // buat semua action di cancel dan dinonaktifkan
            // component PlayerControl di disable supaya tidak bisa jalan kemana kemana
            player.GetComponent<ActionScheduler>().cancelCurrentAction();
            player.GetComponent<PlayerControl>().enabled = false;

        }

        void enableControl(PlayableDirector pd){

            // component PlayerControl di enable
            player.GetComponent<PlayerControl>().enabled = true;
        }
    }
    
}

