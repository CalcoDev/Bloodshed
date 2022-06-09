using System;
using UnityEngine;
using Utils.Timers;

namespace Abilities
{
    [CreateAssetMenu(fileName = "Ability", menuName = "Bloodshed/Abilities/Ability", order = 1)]
    public class AbilitySO : ScriptableObject
    {
        [Header("Settings")]
        public new string name;
        
        [TextArea(4, 10)]
        public string description;

        public Sprite icon;
        
        public float cooldown = 1f;
    }
}