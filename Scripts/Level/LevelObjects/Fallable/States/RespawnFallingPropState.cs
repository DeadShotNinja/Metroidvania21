using UnityEngine;

namespace Metro
{
	public class RespawnFallingPropState : IFallingPropState
	{
		private FallingProp _prop;
		private float _stateTick;
        
		public void EnterState(FallingProp prop)
		{
			_prop = prop;
			_stateTick = Time.time;
		}

		public void UpdateState(FallingProp prop)
		{
			if (ShouldRespawn())
			{
				if (prop.OnRespawnFeedbacks != null) prop.OnRespawnFeedbacks.PlayFeedbacks();
				prop.ChangeState(prop.IdleState);
			}
		}

		public void OnCollisionEnter2DState(FallingProp prop, Collision2D collision)
		{
			
		}

		public void ExitState(FallingProp prop)
		{
			
		}
		
		private bool ShouldRespawn()
		{
			return _stateTick + _prop.RespawnTime < Time.time || IsAreaClear();
		}
		
		private bool IsAreaClear()
		{
			Vector2 boxSize = _prop.Collider2D.bounds.size;
			RaycastHit2D hit = Physics2D.BoxCast(_prop.OriginalPosition, boxSize, 0f, Vector2.zero, 0f);
			
			return hit.collider == null || !hit.collider.gameObject.TryGetComponent(out PlayerEntity player);
		}
	}
}