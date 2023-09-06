using UnityEngine;

namespace Metro
{
	public class FallFallingPropState : IFallingPropState
	{
		private float _stateTick;
		
		public void EnterState(FallingProp prop)
		{
			_stateTick = Time.time;
		}

		public void UpdateState(FallingProp prop)
		{
			if (ShouldSetupFalling(prop.CollisionCheckBuffer))
			{
				prop.Rigidbody2D.isKinematic = false;
				prop.Rigidbody2D.simulated = true;
				prop.Collider2D.enabled = true;
				if (prop.OnFallFeedbacks != null) prop.OnFallFeedbacks.PlayFeedbacks();
			}
			
			prop.transform.position += Vector3.down * (prop.FallSpeed * Time.deltaTime);
		}

		public void OnCollisionEnter2DState(FallingProp prop, Collision2D collision)
		{
			prop.ChangeState(prop.HitState);
		}

		public void ExitState(FallingProp prop)
		{
			
		}
		
		private bool ShouldSetupFalling(float fallBuffer)
		{
			return _stateTick + fallBuffer < Time.time;
		}
	}
}