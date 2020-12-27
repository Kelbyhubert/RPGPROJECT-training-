using UnityEngine;

namespace RPG.Combat
{
    // jika mamasukan component ini maka health juga akan masuk kedalam game object tersebut(RequireComponent)
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {

    }
    
}