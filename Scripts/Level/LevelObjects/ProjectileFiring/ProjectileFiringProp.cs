using System;
using System.Collections;
using MoreMountains.Feedbacks;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metro
{
	public class ProjectileFiringProp : MonoBehaviour, IInteractableTarget
	{
		[Header("Projectile Setup")]
		[Tooltip("The projectile prefab that will be fired.")]
		[SerializeField] private GameObject _projectilePrefab;
		[Tooltip("The spawn point of the projectile.")]
		[SerializeField] private Transform _projectileSpawnPoint;

		[Header("Firing Setup")]
		[Tooltip("If true, this projectile firing prop will not need to be activated to start firing.")]
		[SerializeField] private bool _fireOnStart = true;
		[Tooltip("Whether the fire rate should be random or not.")]
		[SerializeField] private bool _randomFireRate;
		[HideIf(nameof(_randomFireRate))]
		[Tooltip("The constant fire rate of the projectile.")]
		[SerializeField] private float _constFireRate;
		[ShowIf(nameof(_randomFireRate))]
		[Tooltip("The minimum fire rate of the projectile.")]
		[SerializeField] private float _minFireRate;
		[ShowIf(nameof(_randomFireRate))]
		[Tooltip("The maximum fire rate of the projectile.")]
		[SerializeField] private float _maxFireRate;
		[Tooltip("The speed of the projectile.")]
		[SerializeField] private float _projectileSpeed;
		[Tooltip("The direction the projectile will be fired in.")]
		[SerializeField] private Vector2 _fireDirection;
		
		[Header("Feedbacks")]
		[Tooltip("Feedbacks that will be played when this projectile firing prop fires.")]
		[SerializeField] private MMFeedbacks _onFireFeedbacks;

		private IProjectilePropState _currentState;

		#region Properties

		public GameObject ProjectilePrefab => _projectilePrefab;
		public Transform ProjectileSpawnPoint => _projectileSpawnPoint;
		
		public bool RandomFireRate =>  _randomFireRate;
		public float ConstFireRate => _constFireRate;
		public float MinFireRate => _minFireRate;
		public float MaxFireRate => _maxFireRate;
		public float ProjectileSpeed => _projectileSpeed;
		public Vector2 FireDirection => _fireDirection;
		
		public MMFeedbacks OnFireFeedbacks => _onFireFeedbacks;

		#endregion
		
		#region States
		
		public IdleProjectilePropState IdleState { get; } = new IdleProjectilePropState();
		public FireProjectilePropState FireState { get; } = new FireProjectilePropState();
		public ReloadProjectilePropState ReloadState { get; } = new ReloadProjectilePropState();

		#endregion

		private void Awake()
		{
			_currentState = IdleState;
			_currentState.EnterState(this);
		}

		private void Start()
		{
			if (_fireOnStart) Activate();
		}
		
		private void Update()
		{
			_currentState.UpdateState(this);
		}
		
		public void ChangeState(IProjectilePropState newState)
		{
			_currentState.ExitState(this);
			_currentState = newState;
			_currentState.EnterState(this);
		}

		public void Activate()
		{
			if (_currentState == IdleState) ChangeState(FireState);
		}
		
		public void Deactivate()
		{
			if (_currentState != IdleState) ChangeState(IdleState);
		}
	}
}