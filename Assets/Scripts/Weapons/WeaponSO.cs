using UnityEngine;

namespace Weapons
{
    public abstract class WeaponSO : ScriptableObject
    {
        [Header("Settings")] 
        public new string name;
        
        [TextArea(4, 10)]
        public string description;
    }
}