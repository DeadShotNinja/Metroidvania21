using System;
using Cinemachine;
using UnityEngine;

namespace Metro
{
	public class GameplayLevelState : BaseLevelState
	{
		public GameplayLevelState(LevelManager levelManager, StateMachine<BaseLevelState> stateMachine) : base(levelManager, stateMachine) { }

		public override void Enter()
		{
			base.Enter();
            
			//EventManager.StartListening<RequestPeriodChangeEvent>(OnChangePeriod);
			EventManager.StartListening<ChangeRoomEvent>(OnChangeRoom);
			EventManager.StartListening<PlayerDiedEvent>(OnPlayerDied);
			
			EventManager.TriggerEvent(new PlayerControlsEvent(true));
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if (ShouldGoToTimeSwap())
			{
				_stateMachine.ChangeState(_levelManager.TimeSwapLevelState);
				return;
			}
		}

		public override void Exit()
		{
			base.Exit();
            
			EventManager.StopListening<ChangeRoomEvent>(OnChangeRoom);
			EventManager.StopListening<PlayerDiedEvent>(OnPlayerDied);
		}
		
		public bool ShouldGoToTimeSwap()
		{
			return _levelManager.PlayerInput.TimeSwapInput.Pressed;
		}
        
		private void OnChangeRoom(ChangeRoomEvent eventData)
		{
			Room variant = _levelManager.RoomHolders.Find(x => x.RoomID == eventData.TargetRoomID);
			if (variant == null)
			{
				Debug.LogError($"Room ID {eventData.TargetRoomID} does not exist in collection of rooms. Can't switch rooms.");
				return;
			}
			
			SetTargetRoomVariant(variant);
			SpawnPoint newSpawn = Array.Find(variant.CurrentRoomVariant.SpawnPoints,
				x => x.SpawnID == eventData.TargetSpawnID);
			if (newSpawn == null)
			{
				Debug.LogError($"Target spawn point {eventData.TargetSpawnID} does not exist in room ID {eventData.TargetRoomID}." +
				               $" Attempting to get defaulting spawn point...");
				Vector2 newPos = _levelManager.CurrentRoom.GetFirstSpawnPoint();
				if (newPos == Vector2.zero)
				{
					Debug.LogError($"No spawn points found in room ID {eventData.TargetRoomID}. Can't switch rooms.");
					return;
				}

				_levelManager.QueuedSpawn = newPos;
			}
			else
			{
				_levelManager.QueuedSpawn = newSpawn.transform.position;
			}
            
			
			_levelManager.QueuedRoom = variant;
			_stateMachine.ChangeState(_levelManager.TransitionLevelState);
		}
		
		private void SetTargetRoomVariant(Room room)
		{
			if (_levelManager.TimeState == TimeState.Past)
			{
				room.ShowPastVariant();
			}
			else
			{
				room.ShowPresentVariant();
			}
			
			room.HideAllVariants();
		}
		
		private void OnPlayerDied(PlayerDiedEvent eventData)
		{
			if (_levelManager.PlayerDiedFeedbacks != null) _levelManager.PlayerDiedFeedbacks.PlayFeedbacks();
			_stateMachine.ChangeState(_levelManager.RespawnLevelState);
		}
	}
}