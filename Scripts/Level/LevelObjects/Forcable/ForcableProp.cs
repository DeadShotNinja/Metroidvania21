using MoreMountains.Feedbacks;
using UnityEngine;

namespace Metro
{
	public class ForcableProp : MonoBehaviour
	{
		[Header("Setup")]
		[SerializeField] private float _force = 10f;
		[SerializeField] private Vector2 _direction = Vector2.up;
		[SerializeField] private ForceMode2D _forceMode = ForceMode2D.Impulse;

		[Header("Feedbacks")]
		[SerializeField] private MMFeedbacks _onBounceFeedbacks;
		
		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.TryGetComponent(out PlayerEntity player))
			{
				if (!player.Collision.IsGrounded) return;
				
				player.EntityRigidbody.AddForce(_force * _direction, _forceMode);
				if (_onBounceFeedbacks != null) _onBounceFeedbacks.PlayFeedbacks();
			}
		}
	}
}