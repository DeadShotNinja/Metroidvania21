using UnityEngine;

namespace Metro
{
	public class InitializeGameState : BaseGameState
	{
		public InitializeGameState(GameManager gameManager, StateMachine<BaseGameState> stateMachine) : base(gameManager, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			
			Debug.Log("GameManager State: Initialize");
			
			EventManager.StartListening<LevelReadyEvent>(OnLevelReady);
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
			
			EventManager.StopListening<LevelReadyEvent>(OnLevelReady);
		}
		
		private void OnLevelReady(LevelReadyEvent eventData)
		{
			_gameManager.GameStateMachine.ChangeState(_gameManager.PlayGameState);
		}
	}
}