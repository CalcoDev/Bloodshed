using UnityEngine;

namespace Abilities
{
    public abstract class Ability : MonoBehaviour
    {
        [Header("Settings")]
        public AbilitySO abilityData;

        public bool lockInput;
        public bool lockMovement;

        protected Player.Player Player;
        protected int FallbackState = -1;
        protected int SelfState = -1;

        public void Initialise(Player.Player player, int fallbackState, int selfState) {
            Player = player;
            FallbackState = fallbackState;
            SelfState = selfState;
        }
        
        public void SetPlayer(Player.Player player)
        {
            Player = player;
        }
        public void SetFallbackState(int state)
        {
            FallbackState = state;
        }
        public void SetSelfState(int state)
        {
            SelfState = state;
        }
        
        public virtual void OnEnter()
        {
        }

        public virtual int OnUpdate()
        {
            return SelfState;
        }

        public virtual int OnPhysicsUpdate()
        {
            return SelfState;
        }

        public virtual void OnLateUpdate()
        {
        }

        public virtual void OnExit()
        {
        }
    }
}