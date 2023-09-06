using UnityEngine;

namespace Metro
{
	public interface IFallingPropState
	{
		public void EnterState(FallingProp prop);
		public void UpdateState(FallingProp prop);
		public void OnCollisionEnter2DState(FallingProp prop, Collision2D collision);
		public void ExitState(FallingProp prop);
	}
}