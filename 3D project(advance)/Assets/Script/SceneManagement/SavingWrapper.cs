using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RPG.Saving;
using System;

namespace RPG.SceneManagement
{
    
    public class SavingWrapper : MonoBehaviour
    {
        // path bisa diliat dari console unity
        // class ini buat input
        const string defaultSaveFileName = "save";
        
        IEnumerator Start() {
            yield return GetComponent<SavingSystem>().LoadLastScene(defaultSaveFileName);
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.L)){
                Load();
            }
            if(Input.GetKeyDown(KeyCode.S)){
                Save();
            }
            if(Input.GetKeyDown(KeyCode.D)){
                Delete();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaultSaveFileName);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaultSaveFileName);
        }
        public void Delete(){
            GetComponent<SavingSystem>().Delete(defaultSaveFileName);
        }
    }
}
