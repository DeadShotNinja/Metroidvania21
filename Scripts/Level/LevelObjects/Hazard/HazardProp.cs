using System;
using UnityEngine;

namespace Metro
{
	[RequireComponent(typeof(Collider2D))]
	public class HazardProp : MonoBehaviour
	{
		[Header("Setup")]
		[Tooltip("If true, this game object will be destroyed after damaging something damagable.")]
		[SerializeField] private bool _destroyOnKill;
		
		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.TryGetComponent(out IDamagable damagable))
			{
				damagable.TakeDamage();
				if (_destroyOnKill) Destroy(gameObject);
			}
		}
	}
}