using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    public class ChaseBehaviourState : BaseBehaviourState
    {
        public ChaseBehaviourState(AIBrain brain, StateMachine<BaseBehaviourState> stateMachine) : base(brain, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();
            
            float distance = Vector2.Distance(_brain.transform.position, _brain.Target.position);
            if (distance < _brain.StopDistance)
            {
                _brain.BehaviourStateMachine.ChangeState(_brain.IdleBehaviourState);
            }

            Vector2 direction = _brain.Target.position - _brain.transform.position;
            float xDirection = direction.x;
            if (xDirection > 0f)
            {
                _brain.MoveInput = new Vector2(1, 0f);
            }
            else if (xDirection < 0f)
            {
                _brain.MoveInput = new Vector2(-1, 0f);
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}