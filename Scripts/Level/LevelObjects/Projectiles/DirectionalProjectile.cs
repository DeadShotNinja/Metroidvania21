using System;
using System.Collections;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace Metro
{
	public class DirectionalProjectile : BaseProjectile
	{
		//private SpriteRenderer _spriteRenderer;
		//private Coroutine _deathCoroutine;
		//private bool _isDead;

		protected override void Awake()
		{
			base.Awake();
			
			//_spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		}
		
		private void Update()
		{
			//if (_isDead) return;
			
			transform.position += _direction * (_speed * Time.deltaTime);
		}

		// public void SetUpProjectile(Vector2 direction, float speed)
		// {
		// 	_direction = direction;
		// 	_speed = speed;
		// }
		//
		// private void OnCollisionEnter2D(Collision2D other)
		// {
		// 	if (_onHitFeedbacks != null)
		// 	{
		// 		_onHitFeedbacks.PlayFeedbacks();
		// 	} 
		//
		// 	_deathCoroutine = StartCoroutine(DestroyAfterEffects());
		// }
		//
		// private IEnumerator DestroyAfterEffects()
		// {
		// 	if (_spriteRenderer != null) _spriteRenderer.enabled = false;
		// 	if (_collider2D != null) _collider2D.enabled = false;
		// 	if (_rigidbody2D != null) _rigidbody2D.simulated = false;
  //           
		// 	yield return new WaitForSeconds(5f);
		// 	
		// 	Destroy(gameObject);
		// }
	}
}