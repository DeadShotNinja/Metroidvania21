using UnityEngine;

namespace Metro
{
	public class PreFallFallingPropState : IFallingPropState
	{
		private float _stateTick;
		private float _randWalkTime;
		private bool _feedbackPlayed;
		private float _feedbackWaitTime;
		
		public void EnterState(FallingProp prop)
		{
			_stateTick = Time.time;
			_randWalkTime = prop.IsWalkable ? Random.Range(prop.MinWalkTime, prop.MaxWalkTime) : 0f;
			_feedbackPlayed = false;
			_feedbackWaitTime = 0f;
		}

		public void UpdateState(FallingProp prop)
		{
			if (ShouldPlayFeedback())
			{
				if (prop.OnPreFallFeedbacks != null)
				{
					prop.OnPreFallFeedbacks.PlayFeedbacks();
					_feedbackWaitTime = prop.OnPreFallFeedbacks.TotalDuration;
				}

				_feedbackPlayed = true;
				_stateTick = Time.time;
			}
			
			if (ShouldSwitchToFall())
			{
				prop.ChangeState(prop.FallState);
			}
		}

		public void OnCollisionEnter2DState(FallingProp prop, Collision2D collision)
		{
			
		}

		public void ExitState(FallingProp prop)
		{
			
		}
		
		private bool ShouldPlayFeedback()
		{
			if (_feedbackPlayed)
				return false;
			
			return _stateTick + _randWalkTime < Time.time;
		}
		
		private bool ShouldSwitchToFall()
		{
			if (!_feedbackPlayed)
				return false;
			
			return _stateTick + _feedbackWaitTime < Time.time;
		}
	}
}