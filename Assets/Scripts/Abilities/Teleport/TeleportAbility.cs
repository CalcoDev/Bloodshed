using System.Collections;
using UnityEngine;

namespace Abilities.Teleport
{
    public class TeleportAbility : Ability
    {
        [Header("Teleport Settings")]
        [SerializeField] private float _saveTime;
        
        private bool _teleportEnded;
        
        public override void OnEnter()
        {
            Player.Animator.Play("PlayerTeleportEnter");

            _teleportEnded = false;
            
            Player.Rigidbody.interpolation = RigidbodyInterpolation2D.None;
            Player.Rigidbody.position = Player.MousePosition;

            StartCoroutine(StopTeleport());
        }

        public override int OnUpdate()
        {
            if (_teleportEnded)
                return FallbackState;

            return SelfState;
        }

        public override void OnExit()
        {
            Player.Animator.Play("PlayerTeleportExit");
            Player.Rigidbody.interpolation = RigidbodyInterpolation2D.Interpolate;
        }

        private IEnumerator StopTeleport()
        {
            yield return new WaitForSeconds(_saveTime);
            
            _teleportEnded = true;
        }
    }
}