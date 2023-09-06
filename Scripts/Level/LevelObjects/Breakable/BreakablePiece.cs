using System;
using System.Collections;
using UnityEngine;

namespace Metro
{
	[RequireComponent(typeof(Collider2D))]
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(SpriteRenderer))]
	public class BreakablePiece : MonoBehaviour
	{
		private Collider2D _collider2D;
		private Rigidbody2D _rigidbody2D;
		private SpriteRenderer _spriteRenderer;

		private void Awake()
		{
			_collider2D = GetComponent<Collider2D>();
			_rigidbody2D = GetComponent<Rigidbody2D>();
			_spriteRenderer = GetComponent<SpriteRenderer>();
		}
		
		public void ActivatePiece(float force, Vector2 forceOrigin, bool randomizeForceDirection)
		{
			_collider2D.enabled = true;
			_rigidbody2D.bodyType = RigidbodyType2D.Dynamic;

			Vector2 forceDirection;

			if (randomizeForceDirection)
			{
				float randomAngle = UnityEngine.Random.Range(0f, 360f);
				forceDirection = new Vector2(Mathf.Cos(randomAngle * Mathf.Deg2Rad), Mathf.Sin(randomAngle * Mathf.Deg2Rad));
			}
			else
			{
				forceDirection = (Vector2)transform.position - forceOrigin;
				forceDirection.Normalize();
			}
			
			_rigidbody2D.AddForce(forceDirection * (force * Time.deltaTime), ForceMode2D.Impulse);
			
			StartCoroutine(FadeOut_Coroutine());
		}
		
		private IEnumerator FadeOut_Coroutine()
		{
			yield return new WaitForSeconds(1f);
			
			float alpha = _spriteRenderer.color.a;
			for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1f)
			{
				Color originalColor = _spriteRenderer.color;
				Color newColor = new Color(originalColor.r, originalColor.g, originalColor.b, Mathf.Lerp(alpha, 0, t));
				_spriteRenderer.color = newColor;
				yield return null;
			}
			
			Destroy(gameObject);
		}
	}
}