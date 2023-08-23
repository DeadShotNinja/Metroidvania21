using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Metro
{
	public class RoomVariant : MonoBehaviour
	{
		[Header("Set up")]
		[Tooltip("This should be the polygon collider confiner associated with this room variant. " +
		         "If not provided it will try to find on automatically")]
		[SerializeField] private PolygonCollider2D _cameraConfiner;

		[Header("Debugging")]
        [SerializeField, ReadOnly] private SpawnPoint[] _spawnPoints;
		[SerializeField, ReadOnly] private RoomSwitchTrigger[] _roomSwitchTriggers;

        public PolygonCollider2D CameraConfiner => _cameraConfiner;
        public SpawnPoint[] SpawnPoints => _spawnPoints;

        public event Action<int, int> RoomSwitchTriggeredAction;
        
        private void Awake()
        {
            if (_cameraConfiner == null)
			{
				Debug.LogWarning("Camera Confiner was not set, attempting to set automatically.");
				_cameraConfiner = GetComponentInChildren<PolygonCollider2D>(true);
			}

			_spawnPoints = GetComponentsInChildren<SpawnPoint>(true);
			_roomSwitchTriggers = GetComponentsInChildren<RoomSwitchTrigger>(true);
			foreach (RoomSwitchTrigger trigger in _roomSwitchTriggers)
			{
				trigger.RoomSwitchTriggerAction += OnRoomSwitchTriggered;
			}
        }

        public void Show()
		{
			gameObject.SetActive(true);
		}
		
		public void Hide()
		{
			gameObject.SetActive(false);
		}

        public void OnRoomSwitchTriggered(int targetRoomID, int targetSpawnID)
        {
	        RoomSwitchTriggeredAction?.Invoke(targetRoomID, targetSpawnID);
        }

        private void OnDestroy()
        {
	        foreach (RoomSwitchTrigger trigger in _roomSwitchTriggers)
	        {
		        trigger.RoomSwitchTriggerAction -= OnRoomSwitchTriggered;
	        }
        }
	}
}