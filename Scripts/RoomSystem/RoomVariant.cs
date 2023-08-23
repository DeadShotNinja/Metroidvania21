using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Metro
{
	public class RoomVariant : MonoBehaviour
	{
		[Header("Set up")]
		[SerializeField] private PolygonCollider2D _cameraConfiner;

		[Header("Debugging")]
        [SerializeField, ReadOnly] private SpawnPoint[] _spawnPoints;
		[FormerlySerializedAs("_roomTriggers")] [SerializeField, ReadOnly] private RoomSwitchTrigger[] _roomSwitchTriggers;

        // Create enemmy spawn points, can make a serializable struct or a constructor for this that holds a transform and type of 
        // enemy to spawn.

        public PolygonCollider2D CameraConfiner => _cameraConfiner;
        public SpawnPoint[] SpawnPoints => _spawnPoints;

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
				trigger.SetUp(this);
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

        public void OnTransitionTriggered(int targetHolderID, int targetSpawnID)
        {
			// TODO: should this be in level manager? Maybe not...
			ChangeRoomEvent roomEvent;
			roomEvent.TargetRoomID = targetHolderID;
			roomEvent.TargetSpawnID = targetSpawnID;
			EventManager.TriggerEvent(roomEvent);
        }
    }
}