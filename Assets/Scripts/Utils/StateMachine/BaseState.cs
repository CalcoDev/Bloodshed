namespace Utils.StateMachine
{
    public abstract class BaseState
    {
        public int StateIndex { get; private set; }

        public abstract int OnUpdate();
        public abstract int OnPhysicsUpdate();

        public virtual void OnEnter()
        {
        }

        public virtual void OnExit()
        {
        }

        protected BaseState(int stateIndex)
        {
            StateIndex = stateIndex;
        }
        
        public static implicit operator int(BaseState state) => state.StateIndex;
    }
}