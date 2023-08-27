using System;
using Cinemachine;
using UnityEngine;

namespace Metro
{
	public class GameplayLevelState : BaseLevelState
	{
		private bool _hasCheckedOverlap;
		private Vector3 _lastCheckedPosition;
		private Vector3 _lastCheckedSize;
		
		public GameplayLevelState(LevelManager levelManager, StateMachine<BaseLevelState> stateMachine) : base(levelManager, stateMachine)
		{
		}

		public override void Enter()
		{
			base.Enter();
            
			EventManager.StartListening<RequestPeriodChangeEvent>(OnChangePeriod);
			EventManager.StartListening<ChangeRoomEvent>(OnChangeRoom);
			EventManager.StartListening<PlayerDiedEvent>(OnPlayerDied);
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
            
			EventManager.StopListening<RequestPeriodChangeEvent>(OnChangePeriod);
			EventManager.StopListening<ChangeRoomEvent>(OnChangeRoom);
			EventManager.StopListening<PlayerDiedEvent>(OnPlayerDied);
		}
		
		private void OnChangePeriod(RequestPeriodChangeEvent eventData)
		{
			if (_levelManager.TimeState == TimeState.Past)
			{
				_levelManager.CurrentRoom.ShowPresentVariant();
				if (!IsPeriodSwapValid())
				{
					_levelManager.CurrentRoom.ShowPastVariant();
					return;
				}
				
				_levelManager.TimeState = TimeState.Present;
				_levelManager.PostProcessVolume.profile = _levelManager.PresentProfile;
			}
			else
			{
				_levelManager.CurrentRoom.ShowPastVariant();
				if (!IsPeriodSwapValid())
				{
					_levelManager.CurrentRoom.ShowPresentVariant();
					return;
				}
				
				_levelManager.TimeState = TimeState.Past;
				_levelManager.PostProcessVolume.profile = _levelManager.PastProfile;
			}
		}
		
		private bool IsPeriodSwapValid()
		{
			Vector3 colliderSize = _levelManager.PlayerEntity.EntityCollider.bounds.size;
			Vector3 reducedColSize = new Vector3(colliderSize.x * 0.9f, colliderSize.y * 0.9f, colliderSize.z);
			Vector3 playerPos = _levelManager.PlayerEntity.transform.position;
			Collider2D overlapResult = Physics2D.OverlapBox(playerPos, reducedColSize,
				0f, _levelManager.PlayerEntity.Collision.CollidableLayers);

			_hasCheckedOverlap = true;
			_lastCheckedPosition = playerPos;
			_lastCheckedSize = reducedColSize;
			
			return overlapResult == null;
		}
        
		private void OnChangeRoom(ChangeRoomEvent eventData)
		{
			Room variant = _levelManager.RoomHolders.Find(x => x.RoomID == eventData.TargetRoomID);
			if (variant == null)
			{
				Debug.LogError("Room ID does not exist in collection of rooms.");
				return;
			}
            
			ChangeRoom(variant);
            SetPlayerPosition(eventData.TargetSpawnID);
			ModifyCameraConfiner();
		}
		
		private void ChangeRoom(Room newRoom)
		{
			_levelManager.CurrentRoom.HideAllVariants();
			_levelManager.CurrentRoom = newRoom;

			if (_levelManager.TimeState == TimeState.Past)
			{
				_levelManager.CurrentRoom.ShowPastVariant();
			}
			else
			{
				_levelManager.CurrentRoom.ShowPresentVariant();
			}
		}
		
		private void SetPlayerPosition(int targetSpawnID)
		{
			SpawnPoint newSpawn = Array.Find(_levelManager.CurrentRoom.CurrentRoomVariant.SpawnPoints,
				x => x.SpawnID == targetSpawnID);
			Vector3 newPos;
			if (newSpawn == null)
			{
				Debug.LogError("Specified target spawn point did not exist, defaulting to first spawn found.");
				newPos = _levelManager.CurrentRoom.GetFirstSpawnPoint();
			}
			else
			{
				newPos = newSpawn.transform.position; 
			}
			newPos = newSpawn.transform.position;
			_levelManager.PlayerEntity.transform.position = newPos;
			_levelManager.CheckPoint = newPos;
		}
		
		private void ModifyCameraConfiner()
		{
			CinemachineConfiner2D confiner = _levelManager.PlayerCamera.GetComponentInChildren<CinemachineConfiner2D>();
			confiner.m_BoundingShape2D = _levelManager.CurrentRoom.CurrentRoomVariant.CameraConfiner;
		}
		
		private void OnPlayerDied(PlayerDiedEvent eventData)
		{
			//_levelManager.PlayerEntity.transform.position = _levelManager.CheckPoint;
			_levelManager.PlayerEntity.EntityRespawn(_levelManager.CheckPoint);
		}
		
		public override void DrawGizmosWhenSelected()
		{
			base.DrawGizmosWhenSelected();
			
			if (_levelManager.DrawRoomSwapGizmos && _hasCheckedOverlap)
			{
				Gizmos.color = Color.red;
				Gizmos.DrawWireCube(_lastCheckedPosition, _lastCheckedSize);
			}
		}
	}
}