using System;
using System.Collections.Generic;
using UnityEngine;

namespace Metro
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	public class MovableProp : MonoBehaviour, IMovable, IAttachableHandler, IInteractableTarget
	{
		[Header("Movement Setup")]
		[Tooltip("The speed at which the platform moves.")]
		[SerializeField] private float _speed = 5f;
		[Tooltip("Whether the platform should start moving when game starts.")]
		[SerializeField] private bool _moveOnStart = true;
		[Tooltip("The type of movement the platform should follow.")]
		[SerializeField] private MovementType _movementType;
		[Tooltip("The waypoints the platform should follow.")]
		[SerializeField] private Vector2[] _waypoints;

		private Vector2 _startingPosition;
		private int _currentWaypoint;
		private bool _isMoving;
		private bool _forward = true;
		private List<IAttachable> _attachables = new();
		
		private IMovementStrategyFactory _movementStrategyFactory;
		private IMovementStrategy _movementStrategy;

		private void Start()
		{
			Initialize();
		}
		
		private void Update()
		{
			if (_isMoving)
			{
				Move();
			}
		}

		public void Activate()
		{
			_isMoving = true;
		}
		
		public void Deactivate()
		{
			_isMoving = false;
		}

		private void Initialize()
		{
			_isMoving = _moveOnStart;
			_startingPosition = transform.position;

			_movementStrategyFactory = new MovementStrategyFactory();
			_movementStrategy = _movementStrategyFactory.Create(_movementType);
		}
		
		public void Move()
		{
			Vector2 moveDelta = _movementStrategy.CalculateMovement(transform.position, _startingPosition, _speed,
				Time.deltaTime, ref _currentWaypoint, ref _forward, _waypoints);
			transform.position += new Vector3(moveDelta.x, moveDelta.y, 0f);

			foreach (IAttachable attachable in _attachables)
			{
				attachable.AddToTransformPosition(moveDelta);
			}
		}
		
		public void HandleAttach(IAttachable attachable)
		{
			_attachables.Add(attachable);
			attachable.IsAttached = true;
		}
		
		public void HandleDetach(IAttachable attachable)
		{
			_attachables.Remove(attachable);
			attachable.IsAttached = false;
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			// TODO: Might not work properly if not the player due to the event that sets camera properly for player.
			if (other.gameObject.TryGetComponent(out IAttachable attachable))
			{
				HandleAttach(attachable);
			}
		}

		private void OnCollisionExit2D(Collision2D other)
		{
			// TODO: Might not work properly if not the player due to the event that sets camera properly for player.
			if (other.gameObject.TryGetComponent(out IAttachable attachable))
			{
				HandleDetach(attachable);
			}
		}

		private void OnDrawGizmosSelected()
		{
			if (_waypoints == null) return;

			Vector2 initialPosition;
			if (Application.isPlaying) initialPosition = _startingPosition;
			else initialPosition = transform.position;
			
			GUIStyle labelStyle = new ()
			{
				fontSize = 30,
				normal = { textColor = Color.yellow },
				alignment = TextAnchor.MiddleCenter
			};
			Gizmos.color = Color.green;
			
			for (int i = 0; i < _waypoints.Length; i++)
			{
				Vector2 waypointPosition = initialPosition + _waypoints[i];
				Gizmos.DrawWireSphere(waypointPosition, 0.3f);
				UnityEditor.Handles.Label(waypointPosition, i.ToString(), labelStyle);

				if (i < _waypoints.Length - 1)
				{
					Gizmos.DrawLine(waypointPosition, initialPosition + _waypoints[i + 1]);
				}
				else if (_movementType == MovementType.Loop)
				{
					Gizmos.DrawLine(waypointPosition, initialPosition + _waypoints[0]);
				}
			}
		}
	}
}