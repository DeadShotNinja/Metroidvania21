using UnityEngine;

namespace Metro
{
	public class PlayGameState : BaseGameState
	{
		public PlayGameState(GameManager gameManager, StateMachine<BaseGameState> stateMachine) : base(gameManager, stateMachine)
		{
		}

        public override void Enter()
        {
            base.Enter();

            Debug.Log("GameManager state: Play");
        }        

        public override void LogicUpdate()
        {
            base.LogicUpdate();
        }

        public override void PhysicsUpdate()
        {
            base.PhysicsUpdate();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}