using System.Collections;
using UnityEngine;

namespace Abilities.Dash
{
    public class DashAbility : Ability
    {
        [Header("Dash Settings")]
        public float dashSpeed = 40f;
        public float dashAttackDrag = 0f;
        public float dashEndDrag = 0.3f;
        
        public float dashAttackTime = 0.1f;
        public float dashEndTime = 0.05f;
        
        public float maxDashDistance = 10f;
        
        private Vector2 _dashStartPos;
        private bool _reachedDashApex;

        private bool _dashEnded;
        
        public override void OnEnter()
        {
            _dashEnded = false;
            
            var dir = new Vector2(Player.LastHorizontalDir, 0f);
            Player.FaceMovementDirection(dir.x);

            StartCoroutine(StartDash(dir));
        }

        public override int OnUpdate()
        {
            var dist = (Player.Rigidbody.position - _dashStartPos).sqrMagnitude;
            if (dist >= maxDashDistance * maxDashDistance)
            {
                CancelInvoke(nameof(StartDash));
                StartCoroutine(StopDashWithEndTime());
            }

            if (_dashEnded)
            {
                return FallbackState;
            }

            return SelfState;
        }

        public override int OnPhysicsUpdate()
        {
            Player.ApplyDrag(_reachedDashApex ? dashAttackDrag : dashEndDrag);

            return SelfState;
        }
        
        private IEnumerator StartDash(Vector2 dir)
        {
            Player.Animator.Play("PlayerDash");
            
            _reachedDashApex = false;
            _dashStartPos = Player.Rigidbody.position;

            Player.Rigidbody.gravityScale = 0f;
            Player.Rigidbody.velocity = dir.normalized * dashSpeed;

            yield return new WaitForSeconds(dashAttackTime);

            _reachedDashApex = true;
            
            yield return StopDashWithEndTime();
        }

        private IEnumerator StopDashWithEndTime()
        {
            yield return new WaitForSeconds(dashEndTime);

            StopDash();
        }

        private void StopDash()
        {
            _dashEnded = true;
            Player.Rigidbody.gravityScale = Player.Data.gravityScale;
        }
    }
}