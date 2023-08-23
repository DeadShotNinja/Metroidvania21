using UnityEngine;
using UnityEngine.Serialization;

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
				Debug.LogWarning("Missing room setup, trying to set up automatically.", this);
				RoomVariant[] rooms = GetComponentsInChildren<RoomVariant>(true);
				if (rooms.Length >= 2)
				{
					pastTimeRoomVariant = rooms[0];
					presentTimeRoomVariant = rooms[1];
				}
				else
				{
					Debug.LogError("Room missing RoomVariant child components", this);
				}
			}
		}

		public void ChangeRoomPeriod(TimePeriod period)
		{
			switch (period)
			{
				case TimePeriod.Past:
					presentTimeRoomVariant.Hide();
					pastTimeRoomVariant.Show();
					CurrentRoomVariant = pastTimeRoomVariant;
					break;
				case TimePeriod.Present:
					pastTimeRoomVariant.Hide();
					presentTimeRoomVariant.Show();
					CurrentRoomVariant = presentTimeRoomVariant;
					break;
				default:
					Debug.LogError("Invalid time period", this);
					break;
			}
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
	}
}