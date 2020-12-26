using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour {

        //mirip mirip dengan State
        IAction currentAction;

        public void StartAction(IAction action){

        
            if(currentAction == action) return;
            if(currentAction != null){
                currentAction.cancelAction();
            }
            currentAction = action;
        }
    }
}