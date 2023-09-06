using UnityEngine;

namespace Metro
{
	public class FireProjectilePropState : IProjectilePropState
	{
		public void EnterState(ProjectileFiringProp prop)
		{
			FireProjectile(prop);
		}

		public void UpdateState(ProjectileFiringProp prop)
		{
			if (ShouldSwitchToReload())
			{
				prop.ChangeState(prop.ReloadState);
			}
		}

		public void ExitState(ProjectileFiringProp prop)
		{
			
		}
		
		private bool ShouldSwitchToReload()
		{
			return true;
		}
		
		private void FireProjectile(ProjectileFiringProp prop)
		{
			BaseProjectile projectile = ProjectilePooler.Instance.GetFromPool(prop.ProjectilePrefab);
			if (projectile == null)
			{
				Debug.LogError("Missing Projectile script on projectile or it's not in the ProjectilePooler");
				return;
			}

			projectile.transform.position = prop.ProjectileSpawnPoint.position;
			projectile.Launch(prop.FireDirection, prop.ProjectileSpeed);
			if (prop.OnFireFeedbacks != null) prop.OnFireFeedbacks.PlayFeedbacks();
		}
	}
}