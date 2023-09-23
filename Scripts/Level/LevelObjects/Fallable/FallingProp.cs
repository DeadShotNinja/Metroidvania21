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
		}

		private void Update()
		{
			_currentState.UpdateState(this);
		}
		
		public void ChangeState(IFallingPropState newState)
		{
			_currentState.ExitState(this);
			_currentState = newState;
			_currentState.EnterState(this);
		}

		public void Activate()
		{
			if (_currentState == IdleState)
			{
				ChangeState(PreFallState);
			}
		}

		public void Deactivate()
		{
			ChangeState(IdleState);
		}

		private void OnCollisionEnter2D(Collision2D other)
		{
			_currentState.OnCollisionEnter2DState(this, other);
		}
		
		public void KillProp()
		{
			Destroy(this);
		}

		public void OnDestroy()
		{
			_currentState.ExitState(this);
		}
	}
}