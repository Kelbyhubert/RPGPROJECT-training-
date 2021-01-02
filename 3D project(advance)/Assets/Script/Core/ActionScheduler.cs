using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour {

        //mirip mirip dengan State
        // class ini untuk memberikan perintah aksi apa yang sedang aktif dan aksi apa yang nonaktif , atau dinon aktifkan
        // jika ada action baru maka action lama akan dinonakfikan dan currentaction akan diganti dengan yang diaktifkan
        IAction currentAction;

        public void StartAction(IAction action){

        
            if(currentAction == action) return;
            if(currentAction != null){
                currentAction.cancelAction();
            }
            currentAction = action;
        }


        public void cancelCurrentAction(){
            // buat action selanjutnya menjadi null 
            // jadi tidak ada action yang dijalakan
            // kata lain ini menonaktifkan semua action
            // jika tidak dijadikan null gameObject akan tetap melakukan action selanjutnya yang diassign
            // contoh jika musuh action tidak dijadiin null maka tetap akan jalan
            StartAction(null);
        }
    }
}