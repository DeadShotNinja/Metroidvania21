using UnityEngine;

namespace Metro
{
	public interface IProjectilePropState
	{
		public void EnterState(ProjectileFiringProp prop);
		public void UpdateState(ProjectileFiringProp prop);
		public void ExitState(ProjectileFiringProp prop);
	}
}