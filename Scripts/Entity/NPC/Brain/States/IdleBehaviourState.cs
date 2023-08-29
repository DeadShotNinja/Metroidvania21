using UnityEngine;

namespace Metro
{
    /// <summary>
    /// 
    /// </summary>
    public class IdleBehaviourState : BaseBehaviourState
    {
        public IdleBehaviourState(AIBrain brain, StateMachine<BaseBehaviourState> stateMachine) : base(brain, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            
            _brain.NPC.AIStateText.SetText("");
        }

        public override void LogicUpdate()
        {
            base.LogicUpdate();

            if (ShouldSwitchToPatrol())
            {
                _brain.BehaviourStateMachine.ChangeState(_brain.PatrolBehaviourState);
                return;
            }

            if (_brain.Target == null) return;
            
            //float distance = Vector2.Distance(_brain.transform.position, _brain.Target.position);
            // TEMP, ONLY CHECKS X DISTANCE
            Vector2 tempAIPos = _brain.transform.position;
            tempAIPos.y = _brain.Target.position.y;
            float distance = Vector2.Distance(tempAIPos, _brain.Target.position);
            if (distance > _brain.StopDistance)
            {
                _brain.BehaviourStateMachine.ChangeState(_brain.ChaseBehaviorState);
            }
            
            _brain.MoveInput = new Vector2(0f, 0f);
        }

        public override void Exit()
        {
            base.Exit();
        }
        
        private bool ShouldSwitchToPatrol()
        {
            return _brain.IsPatrolling;
        }
    }
}