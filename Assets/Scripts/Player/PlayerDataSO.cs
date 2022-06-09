using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "Player Data", menuName = "Bloodshed/Player", order = 0)]
    public class PlayerDataSO : ScriptableObject
    {
        [Header("Gravity")]
        public float gravityScale = 1f;
        public float fallGravityMultiplier = 2f;
        
        [Header("Acceleration")]
        public float acceleration = 16f;
        public float deceleration = 20f;
        public float turnSpeed = 30f;
    
        public float airAcceleration = 14f;
        public float airDeceleration = 16f;
        public float airTurnSpeed = 20f;
        
        [Header("Drag")]
        public float groundDrag = 0.2f;
        public float airDrag = 0.15f;
        public float grappleDrag = 0.1f;
        
        [Header("Speed")]
        public float walkSpeed = 12f;
        public float crouchSpeed = 6f;
        public float velocityExponent = 0.85f;

        [Header("Crouching")]
        public float crouchHeight = 0.5f;
        
        [Header("Jump")]
        public float jumpForce = 15f;
        public int jumpCount = 1;
        [Range(0f, 1f)]
        public float jumpCutMultiplier = 0.5f;
        public float jumpCoyoteTime = 0.15f;
        public float jumpBufferTime = 0.1f;
        
        [Header("Grapple")]
        public float maxGrappleDistance = 10f;
        
        public float grapplePullForce = 250f;
        public float grappleFrequency = 4f;
        public float grappleDamping = 7f;
        [Range(0f, 1f)]
        public float minGrappleDistanceMultiplier = 0.25f;
        [Range(0f, 1f)] 
        public float minGrappleDistance = 1f;
        
        public float grappleStopForce = 100f;
    }
}