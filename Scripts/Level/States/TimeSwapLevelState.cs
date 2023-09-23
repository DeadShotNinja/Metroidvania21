using UnityEngine;

namespace Metro
{
	public class TimeSwapLevelState : BaseLevelState
	{
		private bool _hasCheckedOverlap;
		private Vector3 _lastCheckedPosition;
		private Vector3 _lastCheckedSize;
		private bool _timeSwapFailed;
		private bool _isFadingOut;
		private float _timeInState;
		private Vector2 _cachedVeolicty;
		private bool _goingToPresent;
		
		public TimeSwapLevelState(LevelManager levelManager, StateMachine<BaseLevelState> stateMachine) : base(levelManager, stateMachine) { }
		public override void Enter()
		{
			base.Enter();

			_isFadingOut = false;
			_timeSwapFailed = false;
			_timeInState = Time.time;
			AttemptTimeSwap();
		}

		public override void LogicUpdate()
		{
			base.LogicUpdate();

			if (ShouldSwapToGameplay())
			{
				if (!_timeSwapFailed)
				{
					_levelManager.PlayerEntity.EntityRigidbody.simulated = true;
					_levelManager.PlayerEntity.EntityRigidbody.velocity = _cachedVeolicty;
				}

				_stateMachine.ChangeState(_levelManager.GameplayLevelState);
				return;
			}

			if (ShouldFadeOut())
			{
				_isFadingOut = true;
				TimeSwap();
				if (GameDatabase.Instance != null) 
					GameDatabase.Instance.GetEnvironmentAudioEvent(EnvironmentAudioType.Play_DimensionShift)?.Post(_levelManager.gameObject);

				ProjectilePooler.Instance.ReturnAllToPool();

				EventManager.TriggerEvent(new TimeChangedEvent(_goingToPresent, _levelManager.SwapFadeOutDuration));
				EventManager.TriggerEvent(new ScreenFadeEvent(ScreenFadeType.Out, _levelManager.SwapFadeOutDuration));
			}
		}

		public override void Exit()
		{
			base.Exit();
		}
		
		private bool ShouldSwapToGameplay()
		{
			if (_timeSwapFailed)
				return true;

			return _timeInState + _levelManager.SwapFadeInDuration + _levelManager.SwapFadeOutDuration < Time.time;
		}
		
		private bool ShouldFadeOut()
		{
			if (_isFadingOut)
				return false;
			
			return _timeInState + _levelManager.SwapFadeInDuration < Time.time;
		}
		
		private void AttemptTimeSwap()
		{
			_timeSwapFailed = false;
			
			if (_levelManager.TimeState == TimeState.Past)
			{
				_levelManager.CurrentRoom.ShowPresentVariant();
				if (!IsPeriodSwapValid())
				{
					_levelManager.CurrentRoom.ShowPastVariant();
					_timeSwapFailed = true;
					//EventManager.TriggerEvent(new TimeSwapFailedEvent());
					if (_levelManager.FailTimeSwitchFeedbacks != null) _levelManager.FailTimeSwitchFeedbacks.PlayFeedbacks();
					return;
				}
				_levelManager.CurrentRoom.ShowPastVariant();
				_goingToPresent = true;
			}
			else
			{
				_levelManager.CurrentRoom.ShowPastVariant();
				if (!IsPeriodSwapValid())
				{
					_levelManager.CurrentRoom.ShowPresentVariant();
					_timeSwapFailed = true;
                    //EventManager.TriggerEvent(new TimeSwapFailedEvent());
                    if (_levelManager.FailTimeSwitchFeedbacks != null) _levelManager.FailTimeSwitchFeedbacks.PlayFeedbacks();
                    return;
				}
				_levelManager.CurrentRoom.ShowPresentVariant();
				_goingToPresent = false;
			}

			_cachedVeolicty = _levelManager.PlayerEntity.EntityRigidbody.velocity;
			_levelManager.PlayerEntity.EntityRigidbody.simulated = false;
            EventManager.TriggerEvent(new TimeChangedEvent(_goingToPresent, _levelManager.SwapFadeInDuration));
            EventManager.TriggerEvent(new ScreenFadeEvent(ScreenFadeType.In, _levelManager.SwapFadeInDuration));
		}
		
		private void TimeSwap()
		{
			if (_levelManager.TimeState == TimeState.Past)
			{
				_levelManager.CurrentRoom.ShowPresentVariant();
				_levelManager.TimeState = TimeState.Present;
				_levelManager.PostProcessVolume.profile = _levelManager.PresentProfile;
			}
			else
			{
				_levelManager.CurrentRoom.ShowPastVariant();
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