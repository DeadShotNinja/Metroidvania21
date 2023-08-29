using System;
using System.Collections.Generic;
using UnityEngine;

namespace Metro
{
	[RequireComponent(typeof(Rigidbody2D))]
	[RequireComponent(typeof(Collider2D))]
	public class MovableProp : MonoBehaviour
	{
		public enum MovementType { OneWay, Loop, PingPong }

		[Header("Movement Settings")]
		[SerializeField] private float _speed = 5f;
		[SerializeField] private Vector3[] _waypoints;
		[SerializeField] private MovementType _movementType;

		private Rigidbody2D _rigidbody2D;
		private Vector3 _startingPosition;
		private int _currentWaypoint = 0;
		private bool _isMoving = true;
		private bool _forward = true;
		private List<Transform> _attachedEntities = new List<Transform>();

		private void Start()
		{
			_startingPosition = transform.position;
			_rigidbody2D = GetComponent<Rigidbody2D>();
		}
		
		private void Update()
		{
			if (_isMoving)
			{
				Vector3 prevPosition = transform.position;
				MovePlatform();
				Vector3 moveDelta = transform.position - prevPosition;

				foreach (Transform entity in _attachedEntities)
				{
					entity.position += moveDelta;
				}
			}
		}
		
		public void StartMoving()
		{
			_isMoving = true;
		}
		
		public void StopMoving()
		{
			_isMoving = false;
		}
		
		private void MovePlatform()
		{
			if (_waypoints.Length == 0) return;
			
			Vector3 target = _startingPosition + _waypoints[_currentWaypoint];
			Vector3 moveDelta = Vector3.MoveTowards(_rigidbody2D.position, target, _speed * Time.deltaTime) -
			                    new Vector3(_rigidbody2D.position.x, _rigidbody2D.position.y, 0f);
			
			_rigidbody2D.MovePosition(_rigidbody2D.position + new Vector2(moveDelta.x, moveDelta.y));
			
			if (transform.position == target)
			{
				switch (_movementType)
				{
					case MovementType.OneWay:
						_currentWaypoint++;
						if (_currentWaypoint >= _waypoints.Length) { StopMoving(); }
						break;
					case MovementType.Loop:
						_currentWaypoint = (_currentWaypoint + 1) % _waypoints.Length;
						break;
					case MovementType.PingPong:
						if (_forward)
						{
							_currentWaypoint++;
							if (_currentWaypoint >= _waypoints.Length)
							{
								_forward = false;
								_currentWaypoint = _waypoints.Length - 2;
							}
						}
						else
						{
							_currentWaypoint--;
							if (_currentWaypoint < 0)
							{
								_forward = true;
								_currentWaypoint = 1;
							}
						}
						break;
				}
			}
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				other.gameObject.transform.SetParent(transform);
				_attachedEntities.Add(other.gameObject.transform);
			}
		}

		private void OnCollisionExit2D(Collision2D other)
		{
			if (other.gameObject.CompareTag("Player"))
			{
				other.gameObject.transform.SetParent(null);
				Transform foundEntity = _attachedEntities.Find(t => t == other.gameObject.transform);
				if (foundEntity != null)
				{
					_attachedEntities.Remove(foundEntity);
				}
			}
		}

		private void OnDrawGizmosSelected()
		{
			if (_waypoints == null) return;

			Vector3 initialPosition = transform.position;
			
			GUIStyle labelStyle = new GUIStyle();
			labelStyle.fontSize = 30;
			labelStyle.normal.textColor = Color.yellow;
			labelStyle.alignment = TextAnchor.MiddleCenter;
			Gizmos.color = Color.green;
			
			for (int i = 0; i < _waypoints.Length; i++)
			{
				Vector3 waypointPosition = initialPosition + _waypoints[i];
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