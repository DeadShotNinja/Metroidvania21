using System;
using Cinemachine;
using UnityEngine;

namespace Metro
{
	public class TransitionLevelState : BaseLevelState
	{
		private float _stateTick;
		private CinemachineConfiner2D camConfiner;
		
		public TransitionLevelState(LevelManager levelManager, StateMachine<BaseLevelState> stateMachine) : base(levelManager, stateMachine) { }

		public override void Enter()
		{
			base.Enter();

			_stateTick = Time.time;
			EventManager.TriggerEvent(new PlayerControlsEvent(false));
			EventManager.TriggerEvent(new ScreenFadeEvent(ScreenFadeType.In, _levelManager.RoomFadeInDuration));
			TogglePlayerActive(false);
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();
			
			if (ShouldTransitionOutOfState())
			{
				ChangeRoom(_levelManager.QueuedRoom);
				SetPlayerPosition(_levelManager.QueuedSpawn);
				ModifyCameraConfiner();
				TogglePlayerActive(true);
				EventManager.TriggerEvent(new ScreenFadeEvent(ScreenFadeType.Out, _levelManager.RoomFadeOutDuration));
				_stateMachine.ChangeState(_levelManager.GameplayLevelState);
			}
		}

		public override void Exit()
		{
			base.Exit();
			
			_levelManager.QueuedSpawn = Vector2.zero;
			_levelManager.QueuedRoom = null;
		}
		
		private bool ShouldTransitionOutOfState()
		{
			return _stateTick + _levelManager.RoomFadeOutDuration < Time.time;
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
		
		private void TogglePlayerActive(bool isActive)
		{
			_levelManager.PlayerEntity.EntityRigidbody.velocity = Vector2.zero;
			_levelManager.PlayerEntity.gameObject.SetActive(isActive);
		}
		
		private void SetPlayerPosition(Vector2 spawnPos)
		{
			_levelManager.PlayerEntity.transform.position = spawnPos;
			_levelManager.CheckPoint = spawnPos;
			_levelManager.CheckPointInPresent = _levelManager.TimeState == TimeState.Present;
		}
		
		private void ModifyCameraConfiner()
		{
			if (camConfiner == null)
			{
				camConfiner = _levelManager.PlayerCamera.GetComponentInChildren<CinemachineConfiner2D>();
			}
			
			camConfiner.m_BoundingShape2D = _levelManager.CurrentRoom.CurrentRoomVariant.CameraConfiner;
		}
	}
}