using UnityEngine;

namespace Metro
{
	public class ReloadProjectilePropState : IProjectilePropState
	{
		private float _stateTick;
		private float _reloadTime;
		
		public void EnterState(ProjectileFiringProp prop)
		{
			_stateTick = Time.time;
			_reloadTime = prop.RandomFireRate ? Random.Range(prop.MinFireRate, prop.MaxFireRate) : prop.ConstFireRate;
		}

		public void UpdateState(ProjectileFiringProp prop)
		{
			if (ShouldSwithToFire())
			{
				prop.ChangeState(prop.FireState);
			}
		}

		public void ExitState(ProjectileFiringProp prop)
		{
			
		}
		
		private bool ShouldSwithToFire()
		{
			return _stateTick + _reloadTime < Time.time;
		}
	}
}