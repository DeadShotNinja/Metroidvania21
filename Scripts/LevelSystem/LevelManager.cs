using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Rendering;

namespace Metro
{
	public class LevelManager : MonoBehaviour
	{
		[Header("Player Setup")]
		[Tooltip("Make this true if the player prefab was dragged into the scene manually. (Must be tagged with Player)")]
		[SerializeField] private bool _playerInScene = false;
		[HideIf(nameof(_playerInScene))]
		[Tooltip("The player prefab that will be spawned into the level.")]
		[SerializeField] private GameObject _playerPrefab;
		
		[Header("Camera Setup")]
		[Tooltip("If there's a proper cinemachine camera in scene tagged 'MainCamera' this should be true.")]
		[SerializeField] private bool _autoFindCamera = true;
		[HideIf(nameof(_autoFindCamera))]
		[Tooltip("Drag a reference to the camera in the scene.")]
		[SerializeField] private Camera _playerCamera;
		
		[Header("Room Setup")]
		[Tooltip("List of room prefabs that will be used throughout the game. (Note: Order matters)")]
		[SerializeField] private GameObject[] _roomHolderPrefabs;
		[Tooltip("The ID of the room the player will initially spawn in.")]
		[SerializeField] private int _startingRoomID = 1;
		[Tooltip("The ID of the spawn the player will initially spawn on.")]
		[SerializeField] private int _startingSpawnID = 1;
		
		[Header("Post-Processing Setup")]
		[Tooltip("If the post processing prefab is in the scene, make sure to tick this true.")]
		[SerializeField] private bool _volumeInScene = false;
		[HideIf(nameof(_volumeInScene))]
		[Tooltip("The prefab that will be spawned and used for post processing volumes.")]
		[SerializeField] private GameObject _postProcessPrefab;
		[Tooltip("Profile used for Pest time frame.")]
		[SerializeField] private VolumeProfile _pastProfile;
		[Tooltip("Profile used for Present time frame.")]
		[SerializeField] private VolumeProfile _presentProfile;

		[Header("Debugging")]
		[SerializeField] private bool _drawRoomSwapGizmos;
		[SerializeField, ReadOnly] private TimeState _timeState = TimeState.Present;

		#region Properties

		public bool PlayerInScene => _playerInScene;
		public GameObject PlayerPrefab => _playerPrefab;
		public PlayerEntity PlayerEntity { get; set; }
		public Camera PlayerCamera => _playerCamera;
		public bool VolumeInScene => _volumeInScene;
		public GameObject PostProcessPrefab => _postProcessPrefab;
		public Volume PostProcessVolume { get; set; }
		public VolumeProfile PastProfile => _pastProfile;
		public VolumeProfile PresentProfile => _presentProfile;
		public bool DrawRoomSwapGizmos => _drawRoomSwapGizmos;
		public GameObject[] RoomHolderPrefabs => _roomHolderPrefabs;
		public int StartingRoomID => _startingRoomID;
		public int StartingSpawnID => _startingSpawnID;

		// TODO: do we meed this to be a public getter? Private may be more appropriate.
		public List<Room> RoomHolders { get; private set; }
		public Room CurrentRoom { get; set; }

		public TimeState TimeState
		{
			get => _timeState;
			set => _timeState = value;
		}

		public Vector3 CheckPoint { get; set; }

		public StateMachine<BaseLevelState> LevelStateMachine { get; private set; }

		#endregion
		
		#region Level States

		public InitializeLevelState InitializeLevelState { get; private set; }
		public GameplayLevelState GameplayLevelState { get; private set; }
		public PauseLevelState PauseLevelState { get; private set; }
		public EndGameLevelState EndGameLevelState { get; private set; }

		#endregion
		
		private void Awake()
		{
			RoomHolders = new List<Room>();
			LevelStateMachine = new StateMachine<BaseLevelState>();

			InitializeLevelState = new InitializeLevelState(this, LevelStateMachine);
			GameplayLevelState = new GameplayLevelState(this, LevelStateMachine);
			PauseLevelState = new PauseLevelState(this, LevelStateMachine);
			EndGameLevelState = new EndGameLevelState(this, LevelStateMachine);

			if (_playerInScene) _playerPrefab = null;
			if (_playerPrefab == null) TryFindPlayer();
			if (_autoFindCamera) _playerCamera = null;
			if (_playerCamera == null) TryFindCamera();
			if (_volumeInScene) _postProcessPrefab = null;
			if (_postProcessPrefab == null) TryFindPostProcessing();
			
			LevelStateMachine.Initialize(InitializeLevelState);
		}

        private void Update()
        {
            LevelStateMachine.CurrentState.LogicUpdate();
        }

		// TODO; Remove if no physics update loops needed.
        private void FixedUpdate()
        {
            LevelStateMachine.CurrentState.PhysicsUpdate();
        }
        
        private void TryFindPlayer()
        {
	        _playerPrefab = GameObject.FindGameObjectWithTag("Player");
	        if (_playerPrefab == null)
	        {
		        Debug.LogError("Player was not found, make sure to tag player prefab as 'Player' or provide a" +
		                       "player prefab to the LevelManager component.");
	        }
        }
        
        private void TryFindCamera()
        {
	        _playerCamera = Camera.main;
	        if (_playerCamera == null)
	        {
		        Debug.LogError("Camera was not found, make sure to tag the camera in scene as 'MainCamera' or provide a" +
		                       "camera prefab to the LevelManager component.");
	        }
        }
        
        private void TryFindPostProcessing()
        {
	        _postProcessPrefab = GameObject.FindGameObjectWithTag("PostProcess");
	        if (_postProcessPrefab == null)
	        {
		        Debug.LogError("PostProcess GameObject was not found in scene, make sure its tagged correctly.");
	        }
        }

        private void OnDrawGizmosSelected()
        {
	        if (Application.isPlaying)
				LevelStateMachine.CurrentState.DrawGizmosWhenSelected();
        }

        private void OnDestroy()
        {
	        LevelStateMachine.CurrentState.Exit();
        }
	}
}