using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseBehaviourState : BaseState<BaseBehaviourState>
    {
        protected AIBrain _brain;
        
        public BaseBehaviourState(AIBrain brain, StateMachine<BaseBehaviourState> stateMachine) : base(stateMachine)
        {
            _brain = brain;
        }
    }
}