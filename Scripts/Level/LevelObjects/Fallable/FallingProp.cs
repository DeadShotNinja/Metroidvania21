using System;
using System.Collections;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metro
{
	[RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
	public class FallingProp : MonoBehaviour, IInteractableTarget
	{
		[Header("Setup")]
		[Tooltip("If true, the prop will respawn after a certain amount of time.")]
		[SerializeField] private bool _isRespawnable;
		[Tooltip("The time it takes for the prop to respawn.")]
		[ShowIf(nameof(_isRespawnable))]	
		[SerializeField] private float _respawnTime;
		[Tooltip("If true, the prop will react to player walking on it.")]
		[SerializeField] private bool _isWalkable;
		[Tooltip("The minimum time the player has to walk on the prop before it falls.")]
		[ShowIf(nameof(_isWalkable))]
		[SerializeField] private float _minWalkTime;
		[Tooltip("The maximum time the player has to walk on the prop before it falls.")]
		[ShowIf(nameof(_isWalkable))]
		[SerializeField] private float _maxWalkTime;
		[Tooltip("The speed at which the prop will fall.")]
		[SerializeField] private float _fallSpeed;
		[Tooltip("This buffer is to prevent colliding with wall if the prop starts inside a wall.")]
		[SerializeField] private float _collisionCheckBuffer = 0.1f;
		
		[Header("Feedbacks")]
		[Tooltip("The feedbacks to play when the prop is about to fall. (NOTE: Make sure to set the proper total" +
		         "duration of the feedbacks!)")]
		[SerializeField] private MMFeedbacks _onPreFallFeedbacks;
		[Tooltip("The feedbacks to play when the prop is about to fall. (NOTE: Make sure to set the proper total" +
		         "duration of the feedbacks!)")]
		[SerializeField] private MMFeedbacks _onFallFeedbacks;
		[Tooltip("The feedbacks to play when the prop hits something. (NOTE: Make sure to set the proper total" +
		         "duration of the feedbacks!)")]
		[SerializeField] private MMFeedbacks _onHitFeedbacks;
		[Tooltip("The feedbacks to play when the prop is about to fall. (NOTE: Make sure to set the proper total" +
		         "duration of the feedbacks!)")]
		[SerializeField] private MMFeedbacks _onRespawnFeedbacks;
		
		//private bool _isFalling;
		//private bool _isAlive = true;
		
		//private Coroutine _currentPrefallRoutine;
		private IFallingPropState _currentState;

		#region Properties

		public bool IsRespawnable => _isRespawnable;
		public float RespawnTime => _respawnTime;
		public bool IsWalkable => _isWalkable;
		public float MinWalkTime => _minWalkTime;
		public float MaxWalkTime => _maxWalkTime;
		public float FallSpeed => _fallSpeed;
		public float CollisionCheckBuffer => _collisionCheckBuffer;
		
		public MMFeedbacks OnPreFallFeedbacks => _onPreFallFeedbacks;
		public MMFeedbacks OnFallFeedbacks => _onFallFeedbacks;
		public MMFeedbacks OnHitFeedbacks => _onHitFeedbacks;
		public MMFeedbacks OnRespawnFeedbacks => _onRespawnFeedbacks;
		
		public Rigidbody2D Rigidbody2D { get; private set; }
		public SpriteRenderer SpriteRenderer { get; private set; }
		public Collider2D Collider2D { get; private set; }
		public Vector3 OriginalPosition { get; private set; }

		#endregion

		#region States

		public IdleFallingPropState IdleState { get; } = new IdleFallingPropState();
		public PreFallFallingPropState PreFallState { get; } = new PreFallFallingPropState();
		public FallFallingPropState FallState { get; } = new FallFallingPropState();
		public HitFallingPropState HitState { get; } = new HitFallingPropState();
		public RespawnFallingPropState RespawnState { get; } = new RespawnFallingPropState();

		#endregion

		private void Awake()
		{
			InitializeComponents();

			_currentState = IdleState;
			_currentState.EnterState(this);
		}
		
		private void InitializeComponents()
		{
			Rigidbody2D = GetComponent<Rigidbody2D>();
			SpriteRenderer = GetComponentInChildren<SpriteRenderer>();
			Collider2D = GetComponent<Collider2D>();
			OriginalPosition = transform.position;
			//SetupRigidbody();
		}
		
		// private void SetupRigidbody()
		// {
		// 	Rigidbody2D.simulated = _isWalkable;
		// 	Rigidbody2D.isKinematic = _isWalkable;
		// }

		private void OnEnable()
		{
			// switch (_currentState)
			// {
			// 	case PropState.Idle:
			// 		// Do nothing
			// 		break;
			// 	case PropState.PreFall:
			// 		_currentPrefallRoutine = StartCoroutine(PreFallFeedbackDelay());
			// 		break;
			// 	case PropState.Falling:
			// 		// Handle Falling state
			// 		break;
			// 	case PropState.Hit:
			// 		StartCoroutine(HitFeedbackDelay());
			// 		break;
			// 	case PropState.Respawning:
			// 		StartCoroutine(Respawn());
			// 		break;
			// }
		}

		private void OnDisable()
		{
			// _currentPrefallRoutine = null;
			// StopAllCoroutines();
		}

		private void Update()
		{
			//HandleFalling();
			_currentState.UpdateState(this);
		}
		
		public void ChangeState(IFallingPropState newState)
		{
			_currentState.ExitState(this);
			_currentState = newState;
			_currentState.EnterState(this);
		}
		
		// private void HandleFalling()
		// {
		// 	if (!_isFalling) return;
		// 	transform.position += Vector3.down * (_fallSpeed * Time.deltaTime);
		// }

		public void Activate()
		{
			if (_currentState == IdleState)
			{
				ChangeState(PreFallState);
			}
			
			//if (ShouldIgnoreActivation()) return;
			//_currentPrefallRoutine = StartCoroutine(PreFallFeedbackDelay());
		}
		
		// private bool ShouldIgnoreActivation()
		// {
		// 	return !_isAlive || _isFalling || _currentPrefallRoutine != null;
		// }

		public void Deactivate()
		{
			ChangeState(IdleState);
			
			// _rigidbody2D.simulated = false;
			// _collider2D.enabled = true;
			// _isFalling = false;
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			// if (_isWalkable && other.gameObject.TryGetComponent(out PlayerEntity player))
			// {
			// 	//HandleCollision(other);
			// 	return;
			// }
			// else if ()
			
			//ChangeState(HitState);
			_currentState.OnCollisionEnter2DState(this, other);
			
			//HandleCollision(other);
		}
		
		public void KillProp()
		{
			Destroy(this);
		}

		public void OnDestroy()
		{
			_currentState.ExitState(this);
		}

		// private void HandleCollision(Collision2D other)
		// {
		// 	if (!_isWalkable)
		// 	{
		// 		StartCoroutine(HitFeedbackDelay());
		// 		return;
		// 	}
		//
		// 	if (ShouldStartPreFall(other))
		// 	{
		// 		_currentPrefallRoutine = StartCoroutine(PreFallFeedbackDelay());
		// 	}
		// 	else if (_isFalling && ShouldStartHit(other))
		// 	{
		// 		StartCoroutine(HitFeedbackDelay());
		// 	}
		// }
		//
		// private bool ShouldStartPreFall(Collision2D other)
		// {
		// 	return !_isFalling && _currentPrefallRoutine == null && other.gameObject.TryGetComponent(out PlayerEntity player);
		// }
		//
		// private bool ShouldStartHit(Collision2D other)
		// {
		// 	return !other.gameObject.TryGetComponent(out PlayerEntity player);
		// }
		//
		// // private IEnumerator PreFallFeedbackDelay()
		// // {
		// // 	_currentState = PropState.PreFall;
		// // 	yield return WaitForWalkTime();
		// // 	yield return PlayFeedbackAndWait(_onPreFallFeedbacks);
		// // 	_isFalling = true;
		// // 	yield return new WaitForSeconds(_collisionCheckBuffer);
		// // 	SetupFalling();
		// // 	_currentState = PropState.Falling;
		// // }
		//
		// private IEnumerator WaitForWalkTime()
		// {
		// 	float waitBeforeFall = _isWalkable ? UnityEngine.Random.Range(_minWalkTime, _maxWalkTime) : 0f;
		// 	yield return new WaitForSeconds(waitBeforeFall);
		// }
		//
		// private IEnumerator PlayFeedbackAndWait(MMFeedbacks feedbacks)
		// {
		// 	if (feedbacks != null)
		// 	{
		// 		feedbacks.PlayFeedbacks();
		// 		yield return new WaitForSeconds(feedbacks.TotalDuration);
		// 	}
		// }
		//
		// private void SetupFalling()
		// {
		// 	Rigidbody2D.isKinematic = false;
		// 	Rigidbody2D.simulated = true;
		// 	Collider2D.enabled = true;
		// 	if (_onFallFeedbacks != null) _onFallFeedbacks.PlayFeedbacks();
		// 	_currentPrefallRoutine = null;
		// }
		//
		// private IEnumerator HitFeedbackDelay()
		// {
		// 	_currentState = PropState.Hit;
		// 	SetupAfterHit();
		// 	yield return PlayFeedbackAndWait(_onHitFeedbacks);
		// 	CheckForRespawn();
		// 	_currentState = PropState.Respawning;
		// }
		//
		// // private void SetupAfterHit()
		// // {
		// // 	_isAlive = false;
		// // 	_isFalling = false;
		// // 	Rigidbody2D.simulated = false;
		// // 	if (SpriteRenderer != null) SpriteRenderer.enabled = false;
		// // 	Collider2D.enabled = false;
		// // }
		//
		// private void CheckForRespawn()
		// {
		// 	if (_isRespawnable)
		// 	{
		// 		StartCoroutine(Respawn());
		// 	}
		// 	else
		// 	{
		// 		Destroy(gameObject);
		// 	}
		// }
		//
		// private IEnumerator Respawn()
		// {
		// 	_currentState = PropState.Respawning;
		// 	yield return new WaitForSeconds(_respawnTime);
		// 	yield return WaitForClearArea();
		// 	ResetState();
		// 	_currentState = PropState.Idle;
		// }
		//
		// private IEnumerator WaitForClearArea()
		// {
		// 	Vector2 boxSize = Collider2D.bounds.size;
		// 	while (true)
		// 	{
		// 		RaycastHit2D hit = Physics2D.BoxCast(OriginalPosition, boxSize, 0f, Vector2.zero, 0f);
		// 		if (hit.collider != null && hit.collider.gameObject.TryGetComponent(out PlayerEntity player))
		// 		{
		// 			yield return new WaitForSeconds(0.25f);
		// 		}
		// 		else
		// 		{
		// 			break;
		// 		}
		// 	}
		// }
		//
		// private void ResetState()
		// {
		// 	_isFalling = false;
		// 	_isAlive = true;
		// 	transform.position = OriginalPosition;
		// 	Rigidbody2D.velocity = Vector2.zero;
		// 	Rigidbody2D.isKinematic = _isWalkable;
		// 	Rigidbody2D.simulated = _isWalkable;
		// 	if (SpriteRenderer != null) SpriteRenderer.enabled = true;
		// 	Collider2D.enabled = true;
		// 	if (_onRespawnFeedbacks != null) _onRespawnFeedbacks.PlayFeedbacks();
		// }
	}
}