using System;
using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Metro
{
	[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
	public abstract class BaseProjectile : MonoBehaviour, IPooledObject
	{
		[Header("Projectile Setup")]
		[Tooltip("How long the projectile will live before being destroyed, if it does not hit something.")]
		[SerializeField] private float _lifeTime = 5f;
		[Tooltip("The feedbacks that will play when the projectile hits something. (NOTE: Make sure total duration" +
		         "is properly setup or this will not work as expected.)")]
		[SerializeField] private MMFeedbacks _onHitFeedbacks;
		
		protected Vector3 _direction;
		protected float _speed;

		private Rigidbody2D _rigidbody2D;
		private Collider2D _collider2D;
		private SpriteRenderer _spriteRenderer;
		
		public GameObject OriginalPrefab { get; set; }

		protected virtual void Awake()
		{
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_collider2D = GetComponent<Collider2D>();
			_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		}

		public virtual void Launch(Vector2 direction, float speed)
		{
			_direction = direction;
			_speed = speed;

			StartCoroutine(ReturnToPoolAfterTime(_lifeTime));
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			StopAllCoroutines();

			_speed = 0f;
			_rigidbody2D.simulated = false;
			_collider2D.enabled = false;
			_spriteRenderer.enabled = false;
			_onHitFeedbacks.PlayFeedbacks();
			
			StartCoroutine(ReturnToPoolAfterTime(_onHitFeedbacks.TotalDuration));
		}

		private IEnumerator ReturnToPoolAfterTime(float delay)
		{
			yield return new WaitForSeconds(delay);
			ProjectilePooler.Instance.ReturnToPool(this);
		}
		
		public virtual void ResetPooledObject()
		{
			StopAllCoroutines();

			_rigidbody2D.simulated = true;
			_collider2D.enabled = true;
			_spriteRenderer.enabled = true;
		}
	}
}