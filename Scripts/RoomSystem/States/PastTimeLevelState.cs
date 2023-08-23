using Cinemachine;
using UnityEngine;

namespace Metro
{
	public class PastTimeLevelState : BaseLevelState
	{
		public PastTimeLevelState(LevelManager levelManager, StateMachine<BaseLevelState> stateMachine) : base(levelManager, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
			
			_levelManager.CurrentRoom.ChangeRoomPeriod(TimePeriod.Past);

			_levelManager.PostProcessVolume.profile = _levelManager.PastProfile;

			// Spawn any enemies that need to spawn
            
			EventManager.TriggerEvent(new LevelReadyEvent());
            
			EventManager.StartListening<ChangePeriodEvent>(OnChangePeriod);
			EventManager.StartListening<ChangeRoomEvent>(OnChangeRoom);
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

			// Do any cleanup (maybe disabling enemies, but it should be taken care of when room disables, unless enemies will not 
			// be under the room itself but in a seperate enemy spawner?
            
			EventManager.StopListening<ChangePeriodEvent>(OnChangePeriod);
			EventManager.StopListening<ChangeRoomEvent>(OnChangeRoom);
		}
        
		private void OnChangePeriod(ChangePeriodEvent eventData)
		{
			_stateMachine.ChangeState(_levelManager.PresentTimeLevelState);
		}
		
		private void OnChangeRoom(ChangeRoomEvent eventData)
		{
			Room holder = _levelManager.RoomHolders.Find(x => x.RoomID == eventData.TargetRoomID);
			if (holder == null)
			{
				Debug.LogError("Room ID does not exist in collection of rooms.");
				return;
			}
			
			_levelManager.CurrentRoom.HideAllVariants();
			_levelManager.CurrentRoom = holder;
			
			// TODO: don't need to change time period, just need to make sure it shows the right room.
			_levelManager.CurrentRoom.ChangeRoomPeriod(TimePeriod.Past);

			// TODO; make this work with multiple spawn points, currently does not look for ID.
			Vector3 spawnPos = holder.GetFirstSpawnPoint();
			_levelManager.PlayerEntity.transform.position = spawnPos;
			
			// TODO; need better setup for this....
			CinemachineConfiner2D confiner = _levelManager.PlayerCamera.GetComponentInChildren<CinemachineConfiner2D>();
			confiner.m_BoundingShape2D = _levelManager.CurrentRoom.CurrentRoomVariant.CameraConfiner;
		}
	}
}