using UnityEngine;

namespace Metro
{
	public class HitFallingPropState : IFallingPropState
	{
		private float _stateTick;
		private float _feedbackDuration;
		
		public void EnterState(FallingProp prop)
		{
			//_isAlive = false;
			//_isFalling = false;
			prop.Rigidbody2D.simulated = false;
			if (prop.SpriteRenderer != null) prop.SpriteRenderer.enabled = false;
			prop.Collider2D.enabled = false;

			_stateTick = Time.time;
			_feedbackDuration = 0f;
			if (prop.OnHitFeedbacks != null)
			{
				prop.OnHitFeedbacks.PlayFeedbacks();
				_feedbackDuration = prop.OnHitFeedbacks.TotalDuration;
			}
		}

		public void UpdateState(FallingProp prop)
		{
			if (ShouldHandleRespawn())
			{
				if (prop.IsRespawnable)
				{
					prop.ChangeState(prop.RespawnState);
				}
				else
				{
					prop.KillProp();
				}
			}
		}

		public void OnCollisionEnter2DState(FallingProp prop, Collision2D collision)
		{
			
		}

		public void ExitState(FallingProp prop)
		{
			
		}
		
		private bool ShouldHandleRespawn()
		{
			return _stateTick + _feedbackDuration < Time.time;
		}
	}
}