using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    // class ini dipakai sebagai tag kalau target ada / target bisa diserang atau yang lain
    // jika mamasukan component ini maka health juga akan masuk kedalam game object tersebut(RequireComponent)
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour
    {

    }
    
}