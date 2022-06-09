using System;
using UnityEngine;

namespace Utils.StateMachine
{
    [Serializable]
    public class StateMachine
    {
        [SerializeField]
        private int state;
        public int State
        {
            get => state;
            set
            {
                if (value >= _updates.Length || value < 0)
                    throw new IndexOutOfRangeException("State out of range: " + value);

                if (state == value) return;
                
                ChangedStates = true;
                PreviousState = state;
                state = value;
                    
                if (PreviousState != -1 && _exits[PreviousState] != null)
                    _exits[PreviousState]();

                if (_enters[state] != null)
                {
                    _enters[state]();
                }
                
                if (_onStateChanged != null)
                {
                    _onStateChanged(PreviousState, state);
                }
            }
        }
        
        private Action[] _enters;
        private Action[] _exits;

        private Func<int>[] _updates;
        private Func<int>[] _physicsUpdates;
        private Action[] _lateUpdates;
        
        private Action<int, int> _onStateChanged;
        
        public bool ChangedStates { get; private set; }
        
        public int PreviousState { get; private set; }

        public static implicit operator int(StateMachine stateMachine) => stateMachine.State;
        
        public StateMachine(int maxState = 100, int defaultState = 0, Action<int, int> onStateChanged = null)
        {
            PreviousState = state = defaultState;

            _onStateChanged = onStateChanged;
            
            _enters = new Action[maxState];
            _exits = new Action[maxState];
            _updates = new Func<int>[maxState];
            _physicsUpdates = new Func<int>[maxState];
            _lateUpdates = new Action[maxState];
        }

        public void ForceState(int toState)
        {
            if (toState >= _updates.Length || toState < 0)
                throw new IndexOutOfRangeException("State out of range.");

            ChangedStates = true;
            PreviousState = state;
            state = toState;
                    
            if (PreviousState != -1 && _exits[PreviousState] != null)
                _exits[PreviousState]();

            if (_enters[state] != null)
            {
                _enters[state]();
            }
            
            if (_onStateChanged != null)
            {
                _onStateChanged(PreviousState, state);
            }
        }

        public void AddState(BaseState stateToAdd)
        {
            SetCallbacks(stateToAdd.StateIndex, stateToAdd.OnUpdate, stateToAdd.OnPhysicsUpdate, stateToAdd.OnEnter, stateToAdd.OnExit);
        }
        
        public void SetCallbacks(int stateIndex, Func<int> onUpdate = null, Func<int> onPhysicsUpdate = null, Action onLateUpdate = null, Action enter = null, Action exit = null)
        {
            _updates[stateIndex] = onUpdate;
            _physicsUpdates[stateIndex] = onPhysicsUpdate;
            _lateUpdates[stateIndex] = onLateUpdate;
            _enters[stateIndex] = enter;
            _exits[stateIndex] = exit;
        }

        public void Update()
        {
            ChangedStates = false;

            if (_updates[state] != null)
                State = _updates[state]();
        }

        public void PhysicsUpdate()
        {
            if (_physicsUpdates[state] != null)
                State = _physicsUpdates[state]();
        }

        public void LateUpdate()
        {
            if (_lateUpdates[state] != null)
                _lateUpdates[state]();
        }
    }
}