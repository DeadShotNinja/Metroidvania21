using UnityEngine;

namespace Metro
{
    /// <summary>
    /// Base for all types of states.
    /// </summary>
    public abstract class BaseState<T> where T : BaseState<T>
    {
        protected StateMachine<T> _stateMachine;
        protected float _stateTimer;

        protected BaseState(StateMachine<T> stateMachine)
        {
            _stateMachine = stateMachine;
        }
        
        public virtual void Enter() { }
        public virtual void LogicUpdate() { }
        public virtual void PhysicsUpdate() { }
        public virtual void Exit() { }
    }
}