using System;
using UnityEngine;
using Utils.Timers;

namespace Abilities
{
    public abstract class TimedAbility : Ability
    {
        public float duration;
        
        protected Timer Timer;

        private void Awake()
        {
            Timer = new Timer(duration);
        }

        public override void OnEnter()
        {
            Timer.Reset();
        }

        // TODO(calco): Handle delta time not in this manner.
        public abstract int Tick(float deltaTime);
        public override int OnUpdate()
        {
            float deltaTime = Time.deltaTime;
            Timer.Update(deltaTime);

            if (Timer.IsReady())
                return FallbackState;

            return Tick(deltaTime);
        }
    }
}