using MoreMountains.Feedbacks;
using UnityEngine;

namespace Metro
{
	/// <summary>
	/// This feedback will instantiate a particle system and play/stop it when playing/stopping the feedback
	/// </summary>
	[AddComponentMenu("")]
	[FeedbackHelp("This feedback will instantiate the specified ParticleSystem at the specified position on Start or on Play, optionally nesting them.")]
	[FeedbackPath("Particles/Custom Pooled Particle")]
	public class MMF_CustomPooledParticle : MMF_ParticlesInstantiation
	{
		protected override void CreatePools(MMF_Player owner)
		{
			if (ParentTransform == null)
			{
				ParentTransform = ProjectilePooler.Instance.transform;
			}
			
			base.CreatePools(owner);
		}
	}
}