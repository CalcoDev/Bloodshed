using System;
using System.Collections;
using Abilities;
using UnityEngine;
using UnityEngine.InputSystem;
using Utils.StateMachine;
using Utils.Timers;

namespace Player
{
    public class Player : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private PlayerDataSO _playerData;
        [SerializeField] private Transform _sprite;
        
        [Header("Grounded")]
        [SerializeField] private Transform _groundCheckPosition;
        [SerializeField] private Vector2 _groundCheckSize;
        
        [SerializeField] private LayerMask _groundLayer;
        
        [Header("Grapple")]
        [SerializeField] private LayerMask _grappleLayer;
        
        [SerializeField] private Transform _grappleGun;
        [SerializeField] private Transform _grappleGunTip;
        
        [Header("Combat")]
        [SerializeField] private Ability _passiveAbility;
        [SerializeField] private Ability _primaryAbility;
        [SerializeField] private Ability _secondaryAbility;
        [SerializeField] private Ability _ultimateAbility;

        #region Input

        private PlayerInput _input;
        
        private float _horizontalInput;
        private float _lastHorizontalDir = 1f;
        
        private bool _pressedJump;
        private bool _releasedJump;

        private bool _isGrapplePressed;
        private bool _isCrouchPressed;
        
        private Vector2 _mousePosition;

        private bool _pressedPassiveAbility;
        private bool _pressedPrimaryAbility;
        private bool _pressedSecondaryAbility;
        private bool _pressedUltimateAbility;

        private bool _pressedPrimaryAttack;
        private bool _pressedSecondaryAttack;

        #endregion

        #region Timers
        
        private Timer _jumpBufferTimer;
        private Timer _jumpCoyoteTimer;
        private Timer _passiveAbilityTimer;
        private Timer _primaryAbilityTimer;
        private Timer _secondaryAbilityTimer;
        private Timer _ultimateAbilityTimer;

        #endregion
        
        #region State Machines

        private StateMachine _movementStateMachine;
        public const int WalkSt = 0;
        public const int FreeFallSt = 1;
        public const int JumpSt = 2;
        public const int GrappleSt = 3;
        public const int LockedMovementSt = 4;
        public const int NoInputSt = 5;

        
        private StateMachine _abilityStateMachine;
        public const int NoAbilitySt = 0;
        public const int PassiveAbilitySt = 1;
        public const int PrimaryAbilitySt = 2;
        public const int SecondaryAbilitySt = 3;
        public const int UltimateAbilitySt = 4;
        
        private StateMachine _equipmentStateMachine;
        public const int NotAttackingSt = 0;
        
        
        #endregion

        #region Walk

        private bool _isCrouching;
        
        #endregion
        
        #region Jump
        
        private bool _isGrounded;
        private int _currentJumpCount;

        private bool HasJumpBuffered => !_jumpBufferTimer.IsReady();
        private bool HasCoyoteTime => !_jumpCoyoteTimer.IsReady();
        private bool HasJumpAvailable => _currentJumpCount < _playerData.jumpCount;
        private bool ShouldJump => HasJumpBuffered && (HasCoyoteTime || HasJumpAvailable);

        private bool _reachedJumpApex;
        
        #endregion

        #region Grapple

        private LineRenderer _lineRenderer;
        private SpringJoint2D _springJoint;

        private Vector2 _grapplePoint;
        
        private float _grappleDistance;
        private bool _hitGrapple;
        
        #endregion

        #region References

        private Rigidbody2D _rb;
        private Animator _animator;
        private Camera _camera;

        #endregion

        #region Geters & Setters 

        public Rigidbody2D Rigidbody => _rb;
        public Animator Animator => _animator;
        public PlayerDataSO Data => _playerData;
        public float LastHorizontalDir => _lastHorizontalDir;
        public Vector2 MousePosition => _mousePosition;

        #endregion

        private void Awake()
        {
            // References
            _rb = GetComponent<Rigidbody2D>();
            _rb.gravityScale = _playerData.gravityScale;
            
            _lineRenderer = _grappleGunTip.parent.GetComponent<LineRenderer>();

            _animator = GetComponent<Animator>();
            
            _camera = Camera.main;
            
            // Input
            _input = new PlayerInput();

            _input.Player.Movement.performed += OnPlayerMovement;
            _input.Player.Movement.canceled += OnPlayerMovementCanceled;
            
            _input.Player.Jump.started += OnPlayerJump;
            _input.Player.Jump.canceled += OnPlayerJumpCancel;
 
            // _input.Player.Grapple.started += OnPlayerGrapple;
            // _input.Player.Grapple.canceled += OnPlayerGrappleCancel;
            
            _input.Player.Crouch.started += OnPlayerCrouch;
            _input.Player.Crouch.canceled += OnPlayerCrouchCancel;

            _input.Player.PassiveAbility.performed += OnPlayerPassiveAbility;
            _input.Player.PrimaryAbility.performed += OnPlayerPrimaryAbility;
            _input.Player.SecondaryAbility.performed += OnPlayerSecondaryAbility;
            _input.Player.UltimateAbility.performed += OnPlayerUltimateAbility;
            
            _input.Player.PrimaryAttack.performed += OnPlayerPrimaryAttack;
            _input.Player.SecondaryAttack.performed += OnPlayerSecondaryAttack;

            // Timers
            _jumpBufferTimer = new Timer(_playerData.jumpBufferTime);
            _jumpCoyoteTimer = new Timer(_playerData.jumpCoyoteTime);
            
            _passiveAbilityTimer = new Timer(_passiveAbility.abilityData.cooldown);
            _primaryAbilityTimer = new Timer(_primaryAbility.abilityData.cooldown);
            _secondaryAbilityTimer = new Timer(_secondaryAbility.abilityData.cooldown);
            _ultimateAbilityTimer = new Timer(_ultimateAbility.abilityData.cooldown);

            // State machine
            _movementStateMachine = new StateMachine();
            _movementStateMachine.SetCallbacks(WalkSt, WalkUpdate, WalkPhysicsUpdate, null, null, WalkExit);
            _movementStateMachine.SetCallbacks(FreeFallSt, FreeFallUpdate, FreeFallPhysicsUpdate, null, FreeFallEnter, FreeFallExit);
            _movementStateMachine.SetCallbacks(JumpSt, JumpUpdate, JumpPhysicsUpdate, null, JumpEnter, JumpExit);
            _movementStateMachine.SetCallbacks(GrappleSt, GrappleUpdate, GrapplePhysicsUpdate, GrappleLateUpdate, GrappleEnter, GrappleExit);
            _movementStateMachine.SetCallbacks(LockedMovementSt, null, null, null, LockedEnter, LockedExit);
            _movementStateMachine.SetCallbacks(NoInputSt);
            
            // TODO(calco): Abilities
            _abilityStateMachine = new StateMachine(onStateChanged: OnSwitchAbility);
            
            _abilityStateMachine.SetCallbacks(NoAbilitySt, NoAbilityUpdate);
            
            InitAbility(_passiveAbility, NoAbilitySt, PassiveAbilitySt, OnEnterPassiveAbility, OnExitPassiveAbility);
            InitAbility(_primaryAbility, NoAbilitySt, PrimaryAbilitySt, OnEnterPrimaryAbility, OnExitPrimaryAbility);
            InitAbility(_secondaryAbility, NoAbilitySt, SecondaryAbilitySt, OnEnterSecondaryAbility, OnExitSecondaryAbility);
            InitAbility(_ultimateAbility, NoAbilitySt, UltimateAbilitySt, OnEnterUltimateAbility, OnExitUltimateAbility);
        }

        #region Unity Functions

        private void OnEnable()
        {
            _input.Player.Enable();
        }

        private void OnDisable()
        {
            _input.Player.Disable();
        }
        
        private void Update()
        {
            _jumpCoyoteTimer.Update(Time.deltaTime);
            _jumpBufferTimer.Update(Time.deltaTime);
            
            _passiveAbilityTimer.Update(Time.deltaTime);
            _primaryAbilityTimer.Update(Time.deltaTime);
            _secondaryAbilityTimer.Update(Time.deltaTime);
            _ultimateAbilityTimer.Update(Time.deltaTime);

            _mousePosition = _camera!.ScreenToWorldPoint(Mouse.current.position.ReadValue());

            if (!_isCrouching && _isCrouchPressed)
                StartCrouch();
            else if (_isCrouching && !_isCrouchPressed)
                StopCrouch();

            _movementStateMachine.Update();
            _abilityStateMachine.Update();
            
            _pressedJump = false;
            _releasedJump = false;
            
            _pressedPassiveAbility = false;
            _pressedPrimaryAbility = false;
            _pressedSecondaryAbility = false;
            _pressedUltimateAbility = false;

            _pressedPrimaryAttack = false;
            _pressedSecondaryAttack = false;
        }

        private void FixedUpdate()
        {
            _movementStateMachine.PhysicsUpdate();
            _abilityStateMachine.PhysicsUpdate();
        }

        private void LateUpdate()
        {
            _movementStateMachine.LateUpdate();
            _abilityStateMachine.LateUpdate();
        }

        #endregion

        #region Helpers

        private void InitAbility(Ability ability, int fallbackState, int selfState, Action onEnter, Action onExit)
        {
            if (ability == null)
                return;
            
            ability.Initialise(this, fallbackState, selfState);
            
            _abilityStateMachine.SetCallbacks(
                selfState,
                ability.OnUpdate,
                ability.OnPhysicsUpdate,
                ability.OnLateUpdate,
                onEnter,
                onExit
            );
        }
        
        public void FaceMovementDirection(float horizontalMovement)
        {
            if (horizontalMovement != 0)
            {
                _sprite.localScale = new Vector3(Mathf.Sign(horizontalMovement), _sprite.localScale.y, _sprite.localScale.z);
            }
        }

        private void ComputeGrounded()
        {
            _isGrounded = Physics2D.OverlapBox(_groundCheckPosition.position, _groundCheckSize, 0f, _groundLayer);
        }

        public void ApplyDrag(float drag)
        {
            var frictionForce = drag * _rb.velocity.normalized;

            frictionForce.x = Mathf.Min(Mathf.Abs(_rb.velocity.x), Mathf.Abs(frictionForce.x));
            frictionForce.y = Mathf.Min(Mathf.Abs(_rb.velocity.y), Mathf.Abs(frictionForce.y));

            frictionForce.x *= Mathf.Sign(_rb.velocity.x);
            frictionForce.y *= Mathf.Sign(_rb.velocity.y);

            frictionForce = -frictionForce;
            
            _rb.AddForce(frictionForce, ForceMode2D.Impulse);
        }

        private void ApplyMovement(float horizontalDir, float speed, float acceleration, float deceleration)
        {
            var targetSpeed = horizontalDir * speed;
            var speedDiff = targetSpeed - _rb.velocity.x;
            
            var turning = Math.Abs(Mathf.Sign(targetSpeed) - Mathf.Sign(_rb.velocity.x)) > 0.01f;
            
            var accelRate = turning switch {
                true => _playerData.turnSpeed,
                false => Mathf.Abs(speedDiff) > 0.01f ? acceleration : deceleration
            };

            var appliedForce = Mathf.Pow(Mathf.Abs(speedDiff) * accelRate, _playerData.velocityExponent) * Mathf.Sign(speedDiff);
            _rb.AddForce(appliedForce * Vector2.right);
        }
        
        private void Jump()
        {
            _animator.Play("PlayerJump");
            
            if (_movementStateMachine.State != JumpSt)
            {
                Debug.Log("Forcefully jumping");
                _movementStateMachine.ForceState(JumpSt);
            }

            _rb.velocity = new Vector2(_rb.velocity.x, 0f);
            _rb.velocity = new Vector2(_rb.velocity.x, _playerData.jumpForce);
            
            _reachedJumpApex = false;
            
            _jumpBufferTimer.ForceReady();
            _jumpCoyoteTimer.Reset();

            _currentJumpCount += 1;
        }

        private bool StartGrapple(Vector2 dir)
        {
            var hit = Physics2D.Raycast(_rb.position, dir, _playerData.maxGrappleDistance, _grappleLayer);

            if (hit.collider != null)
            {
                
                _grapplePoint = hit.point;
                _springJoint = gameObject.AddComponent<SpringJoint2D>();

                _springJoint.autoConfigureConnectedAnchor = false;
                _springJoint.autoConfigureDistance = false;
                _springJoint.connectedAnchor = _grapplePoint;

                _grappleDistance = hit.distance;
                _springJoint.distance = _grappleDistance * _playerData.minGrappleDistanceMultiplier;

                _springJoint.frequency = _playerData.grappleFrequency;
                _springJoint.dampingRatio = _playerData.grappleDamping;

                _lineRenderer.positionCount = 2;
                
                _lineRenderer.SetPosition(1, _grapplePoint);

                if (_movementStateMachine.State != GrappleSt)
                    _movementStateMachine.State = GrappleSt;

                _hitGrapple = true;
                return true;
            }
            
            // TODO(calco): Some other state
            _hitGrapple = false;
            _movementStateMachine.State = FreeFallSt;
            return false;
        }

        private void StopGrapple()
        {
            _lineRenderer.positionCount = 0;
            Destroy(_springJoint);
        }

        private void StartCrouch()
        {
            // _sprite.localScale = new Vector3(_sprite.localScale.x, _playerData.crouchHeight, _sprite.localScale.z);
            transform.position += Vector3.down * (_playerData.crouchHeight * 0.5f);

            _isCrouching = true;
        }
        
        private void StopCrouch()
        {
            // _sprite.localScale = new Vector3(transform.localScale.x, 1f, transform.localScale.z);
            transform.position += Vector3.up * (_playerData.crouchHeight * 0.5f);

            _isCrouching = false;
        }

        #endregion

        #region State Update Callbacks

        #region Movement

        #region Walk

        private int WalkUpdate()
        {
            _animator.Play(_horizontalInput != 0 ? "PlayerWalk" : "PlayerIdle");

            FaceMovementDirection(_horizontalInput);
            ComputeGrounded();

            if (_isGrounded)
            {
                _jumpCoyoteTimer.Reset();
                _currentJumpCount = 0;
            }

            if (ShouldJump)
                return JumpSt;

            if (_isGrapplePressed)
                return GrappleSt;
            
            if (_rb.velocity.y < 0)
                return FreeFallSt;

            return WalkSt;
        }

        private int WalkPhysicsUpdate()
        {
            var speed = _isCrouching ? _playerData.crouchSpeed : _playerData.walkSpeed;
            ApplyMovement(_horizontalInput, speed, _playerData.acceleration, _playerData.deceleration);
            ApplyDrag(_playerData.groundDrag);

            return WalkSt;
        }

        private void WalkExit()
        {
        }

        #endregion

        #region Free Fall

        private void FreeFallEnter()
        {
            _animator.Play("PlayerFall");
            
            _rb.gravityScale = _playerData.gravityScale * _playerData.fallGravityMultiplier;

            // If just falling, assume we jumped
            if (_currentJumpCount == 0)
            {
                _currentJumpCount = 1;
            }
        }
        
        private int FreeFallUpdate()
        {
            FaceMovementDirection(_horizontalInput);
            ComputeGrounded();

            // TODO(calco): Probably should make a "Landed" state.
            if (_isGrounded)
                return WalkSt;

            if (HasJumpBuffered && HasJumpAvailable)
                return JumpSt;
            
            if (_isGrapplePressed)
                return GrappleSt;

            return FreeFallSt;
        }

        private int FreeFallPhysicsUpdate()
        {
            ApplyMovement(_horizontalInput, _playerData.walkSpeed, _playerData.airAcceleration, _playerData.airDeceleration);
            ApplyDrag(_playerData.airDrag);

            return FreeFallSt;
        }

        private void FreeFallExit()
        {
            _rb.gravityScale = _playerData.gravityScale;
        }


        #endregion
        
        #region Jump

        private void JumpEnter()
        {
            Jump();
        }
        
        private int JumpUpdate()
        {
            FaceMovementDirection(_horizontalInput);
            
            // Small hop
            if (_releasedJump)
            {
                if (_rb.velocity.y > 0f && !_reachedJumpApex)
                {
                    _rb.AddForce(Vector2.down * (_rb.velocity.y * (1 - _playerData.jumpCutMultiplier)), ForceMode2D.Impulse);
                }
            }
            
            if (_rb.velocity.y < 0f)
                _reachedJumpApex = true;

            if (ShouldJump)
                Jump();

            if (_reachedJumpApex)
                return FreeFallSt;
            
            if (_isGrapplePressed)
                return GrappleSt;
            
            return JumpSt;
        }

        private int JumpPhysicsUpdate()
        {
            ApplyMovement(_horizontalInput, _playerData.walkSpeed, _playerData.airAcceleration, _playerData.airDeceleration);
            ApplyDrag(_playerData.airDrag);

            return JumpSt;
        }

        private void JumpExit()
        {
        }
        
        #endregion

        #region Grapple

        private void GrappleEnter()
        {
            _animator.Play("PlayerGrapple");
            
            var dir = (_mousePosition - _rb.position).normalized;
            _rb.gravityScale = _rb.gravityScale * _playerData.fallGravityMultiplier;

            StartGrapple(dir);
        }
        
        private int GrappleUpdate()
        {
            FaceMovementDirection(_rb.velocity.x);
            
            _grappleGun.rotation = Quaternion.FromToRotation(Vector3.right, ((Vector3)_grapplePoint - _grappleGun.position));
            
            // TODO(calco): this doesn't necessarily make sense
            if (!_isGrapplePressed)
                return FreeFallSt;
            
            return GrappleSt;
        }

        private int GrapplePhysicsUpdate()
        {
            if (_springJoint.distance < _playerData.minGrappleDistance)
            {
                _springJoint.distance = _playerData.minGrappleDistance;
                return GrappleSt;
            }
                
            var grappleDirVector = (_grapplePoint - _rb.position).normalized;
                
            _rb.AddForce(-grappleDirVector * grappleDirVector);
            _rb.AddForce(grappleDirVector * _playerData.grapplePullForce);

            _springJoint.distance = Vector2.Distance(transform.position, _grapplePoint);
            
            
            ApplyDrag(_playerData.grappleDrag);
            
            return GrappleSt;
        }

        private void GrappleLateUpdate()
        {
            _lineRenderer.SetPosition(0, _grappleGunTip.position);
        }

        private void GrappleExit()
        {
            StopGrapple();

            if (_hitGrapple)
            {
                var dir = _rb.velocity * new Vector2(1.85f, 0.1f);
                dir = dir.normalized;

                var force = dir * (_playerData.grappleStopForce * _rb.velocity.magnitude * 0.05f); 
                
                _rb.AddForce(force, ForceMode2D.Impulse);
                _rb.gravityScale = _playerData.gravityScale;
            }
            
            _grappleGun.rotation = Quaternion.identity;
        }

        #endregion

        #region Locked

        private void LockedEnter()
        {
            _animator.Play("PlayerLocked");
            
            _rb.gravityScale = 0f;
            _rb.velocity = Vector2.zero;
        }
        
        private int LockedUpdate()
        {
            return LockedMovementSt;
        }

        private void LockedExit()
        {
            _rb.gravityScale = _playerData.gravityScale;
        }

        #endregion

        #endregion

        #region Abilities

        private void OnSwitchAbility(int previousState, int currentState)
        {
            Ability ability;
            
            if (currentState == NoAbilitySt)
            {
                ability = previousState switch
                {
                    PassiveAbilitySt => _passiveAbility,
                    PrimaryAbilitySt => _primaryAbility,
                    SecondaryAbilitySt => _secondaryAbility,
                    UltimateAbilitySt => _ultimateAbility,
                    _ => null
                };

                if (ability == null)
                    return;
                
                if (ability.lockInput || ability.lockMovement)
                    _movementStateMachine.ForceState(FreeFallSt);
                
                return;
            }

            ability = currentState switch
            {
                PassiveAbilitySt => _passiveAbility,
                PrimaryAbilitySt => _primaryAbility,
                SecondaryAbilitySt => _secondaryAbility,
                UltimateAbilitySt => _ultimateAbility,
                _ => null
            };

            if (ability == null)
                return;
                
            if (ability.lockInput)
                _movementStateMachine.ForceState(NoInputSt);
            
            if (ability.lockMovement)
                _movementStateMachine.ForceState(LockedMovementSt);
        }
        
        #region No ability

        private int NoAbilityUpdate()
        {
            if (_pressedPassiveAbility && _passiveAbilityTimer.IsReady())
                return PassiveAbilitySt;
            
            if (_pressedPrimaryAbility && _primaryAbilityTimer.IsReady())
                return PrimaryAbilitySt;
            
            if (_pressedSecondaryAbility && _secondaryAbilityTimer.IsReady())
                return SecondaryAbilitySt;
            
            if (_pressedUltimateAbility && _ultimateAbilityTimer.IsReady())
                return UltimateAbilitySt;
            
            return NoAbilitySt;
        }
        
        #endregion
        
        #region Passive
        
        private void OnEnterPassiveAbility()
        {
            _passiveAbility.OnEnter();
        }
        
        private void OnExitPassiveAbility()
        {
            _passiveAbilityTimer.Reset();
            _passiveAbility.OnExit();
        }
        
        #endregion

        #region Primary

        private void OnEnterPrimaryAbility()
        {
            _primaryAbility.OnEnter();
        }
        
        private void OnExitPrimaryAbility()
        {
            _primaryAbilityTimer.Reset();
            _primaryAbility.OnExit();
        }
        
        #endregion
        
        #region Secondary

        private void OnEnterSecondaryAbility()
        {
            _secondaryAbility.OnEnter();
        }
        
        private void OnExitSecondaryAbility()
        {
            _secondaryAbilityTimer.Reset();
            _secondaryAbility.OnExit();
        }
        
        #endregion
        
        #region Ultimate

        private void OnEnterUltimateAbility()
        {
            _ultimateAbility.OnEnter();
        }
        
        private void OnExitUltimateAbility()
        {
            _ultimateAbilityTimer.Reset();
            _ultimateAbility.OnExit();
        }
        
        #endregion
        
        #endregion
        
        #endregion

        #region Input Callbacks

        private void OnPlayerMovement(InputAction.CallbackContext ctx)
        {
            _horizontalInput = ctx.ReadValue<float>();
            
            if (_horizontalInput != 0f)
            {
                _lastHorizontalDir = _horizontalInput;
            }
        }
        
        private void OnPlayerMovementCanceled(InputAction.CallbackContext ctx)
        {
            _horizontalInput = 0f;
        }

        private void OnPlayerJump(InputAction.CallbackContext ctx)
        {
            _pressedJump = true;
            _jumpBufferTimer.Reset();
        }

        private void OnPlayerJumpCancel(InputAction.CallbackContext ctx)
        {
            _releasedJump = true;
        }

        private void OnPlayerGrapple(InputAction.CallbackContext ctx)
        {
            _isGrapplePressed = true;
        }
        
        private void OnPlayerGrappleCancel(InputAction.CallbackContext ctx)
        {
            _isGrapplePressed = false;
        }
        
        private void OnPlayerCrouch(InputAction.CallbackContext ctx)
        {
            _isCrouchPressed = true;
        }
        
        private void OnPlayerCrouchCancel(InputAction.CallbackContext ctx)
        {
            _isCrouchPressed = false;
        }
        
        private void OnPlayerPassiveAbility(InputAction.CallbackContext ctx)
        {
            _pressedPassiveAbility = true;
        }
        
        private void OnPlayerPrimaryAbility(InputAction.CallbackContext ctx)
        {
            _pressedPrimaryAbility = true;
        }
        
        private void OnPlayerSecondaryAbility(InputAction.CallbackContext ctx)
        {
            _pressedSecondaryAbility = true;
        }
        
        private void OnPlayerUltimateAbility(InputAction.CallbackContext ctx)
        {
            _pressedUltimateAbility = true;
        }
        
        private void OnPlayerPrimaryAttack(InputAction.CallbackContext ctx)
        {
            _pressedPrimaryAttack = true;
        }
        
        private void OnPlayerSecondaryAttack(InputAction.CallbackContext ctx)
        {
            _pressedSecondaryAttack = true;
        }

        #endregion
    }
}