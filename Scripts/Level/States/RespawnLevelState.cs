using UnityEngine;

namespace Metro
{
	public class RespawnLevelState : BaseLevelState
	{
		public RespawnLevelState(LevelManager levelManager, StateMachine<BaseLevelState> stateMachine) : base(levelManager, stateMachine) { }

		public override void Enter()
		{
			base.Enter();
			
			EventManager.StartListening<PlayerRespawnedEvent>(OnPlayerRespawned);
			
			_levelManager.PlayerEntity.EntityRespawn(_levelManager.CheckPoint);
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
			
			EventManager.StopListening<PlayerRespawnedEvent>(OnPlayerRespawned);
		}
		
		private void OnPlayerRespawned(PlayerRespawnedEvent eventData)
		{
			_stateMachine.ChangeState(_levelManager.GameplayLevelState);
		}
	}
}