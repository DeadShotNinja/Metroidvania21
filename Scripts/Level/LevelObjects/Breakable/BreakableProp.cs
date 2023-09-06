using System;
using UnityEngine;

namespace Metro
{
	[RequireComponent(typeof(Collider2D))]
	public class BreakableProp : MonoBehaviour
	{
		[Header("Setup")]
		[Tooltip("The force that will be applied to the pieces when the prop is broken.")]
		[SerializeField] private float _breakForce = 10f;
		[Tooltip("If true, the direction the break force will be applied will be random.")]
		[SerializeField] private bool _randomizeDirection;
		
		private Collider2D _collider2D;
		private BreakablePiece[] _breakablePieces;
		
		private void Awake()
		{
			_collider2D = GetComponent<Collider2D>();
			_breakablePieces = GetComponentsInChildren<BreakablePiece>();
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				_collider2D.enabled = false;
				ActivatePieces(other.gameObject.transform.position);
			}
		}
		
		public void HandleBreakable(GameObject player)
		{
            Collider2D[] results = new Collider2D[10];
            int numColliders = _collider2D.OverlapCollider(new ContactFilter2D(), results);
    
            for (int i = 0; i < numColliders; i++)
            {
                if (results[i].gameObject == player)
                {
                    _collider2D.enabled = false;
                    ActivatePieces(player.gameObject.transform.position);
                    return;
                }
            }
            
            _collider2D.isTrigger = true;
		}
		
		public void ResetCollider()
		{
			_collider2D.isTrigger = false;
		}
		
		private void ActivatePieces(Vector2 forceOrigin)
		{
			foreach (BreakablePiece piece in _breakablePieces)
			{
				piece.ActivatePiece(_breakForce, forceOrigin, _randomizeDirection);
			}
		}
	}
}