using UnityEngine;

namespace Metro
{
	public class IdleFallingPropState : IFallingPropState
	{
		public void EnterState(FallingProp prop)
		{
			ResetProp(prop);
		}

		public void UpdateState(FallingProp prop)
		{
			
		}

		public void OnCollisionEnter2DState(FallingProp prop, Collision2D collision)
		{
			if (collision.gameObject.TryGetComponent(out PlayerEntity player))
			{
				prop.ChangeState(prop.PreFallState);
			}
		}

		public void ExitState(FallingProp prop)
		{
			
		}
		
		private void ResetProp(FallingProp prop)
		{
			prop.Rigidbody2D.simulated = prop.IsWalkable;
			prop.Rigidbody2D.isKinematic = prop.IsWalkable;
			prop.Rigidbody2D.velocity = Vector2.zero;
			prop.transform.position = prop.OriginalPosition;
			if (prop.SpriteRenderer != null) prop.SpriteRenderer.enabled = true;
			prop.Collider2D.enabled = true;
		}
	}
}