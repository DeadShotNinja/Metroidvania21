using System;
using UnityEngine;

namespace Metro
{
	public class Room : MonoBehaviour
	{
        [Tooltip("Unique ID of this room (Note: Make sure it is different from other rooms)")]
        [SerializeField] private int _roomID;
        
        [Header("Setup Rooms")]
        [SerializeField] private RoomVariant pastTimeRoomVariant;
		[SerializeField] private RoomVariant presentTimeRoomVariant;

		public int RoomID => _roomID;
		public RoomVariant CurrentRoomVariant { get; private set; }

		private void Awake()
		{
			ValidateRoomVariants();
		}
		
		private void ValidateRoomVariants()
		{
			if (pastTimeRoomVariant == null || presentTimeRoomVariant == null)
			{
				Debug.LogWarning("Room not properly setup, trying to set up automatically.", this);
				RoomVariant[] rooms = GetComponentsInChildren<RoomVariant>(true);
				if (rooms.Length >= 2)
				{
					pastTimeRoomVariant = rooms[0];
					presentTimeRoomVariant = rooms[1];
				}
				else
				{
					Debug.LogError("Room missing RoomVariant child components", this);
					return;
				}
			}

			pastTimeRoomVariant.RoomSwitchTriggeredAction += OnRoomSwitchTriggered;
			presentTimeRoomVariant.RoomSwitchTriggeredAction += OnRoomSwitchTriggered;
		}
		
		public void ShowPastVariant()
		{
			presentTimeRoomVariant.Hide();
			pastTimeRoomVariant.Show();
			CurrentRoomVariant = pastTimeRoomVariant;
		}
		
		public void ShowPresentVariant()
		{
			pastTimeRoomVariant.Hide();
			presentTimeRoomVariant.Show();
			CurrentRoomVariant = presentTimeRoomVariant;
		}

		public void HideAllVariants()
		{
			presentTimeRoomVariant.Hide();
			pastTimeRoomVariant.Hide();
		}

		public Vector3 GetFirstSpawnPoint()
		{
			return CurrentRoomVariant.SpawnPoints[0].transform.position;
		}	
		
		private void OnRoomSwitchTriggered(int targetRoomID, int targetSpawnID)
		{
			ChangeRoomEvent roomEvent;
			roomEvent.TargetRoomID = targetRoomID;
			roomEvent.TargetSpawnID = targetSpawnID;
			EventManager.TriggerEvent(roomEvent);
		}

		private void OnDestroy()
		{
			pastTimeRoomVariant.RoomSwitchTriggeredAction -= OnRoomSwitchTriggered;
			presentTimeRoomVariant.RoomSwitchTriggeredAction -= OnRoomSwitchTriggered;
		}
	}
}