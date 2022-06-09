using UnityEngine;

namespace Abilities.Visn
{
    public class VisnAbility : TimedAbility
    {
        public override int Tick(float deltaTime)
        {
            Debug.Log("Visning Matthew");

            return SelfState;
        }

        public override void OnEnter()
        {
            base.OnEnter();

            Debug.Log("Started visning");
        }

        public override void OnExit()
        {
            Debug.Log("Stopped visning");
        }
    }
}