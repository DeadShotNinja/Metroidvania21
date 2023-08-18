using UnityEngine;

namespace Metro
{
	public class RoomHolder : MonoBehaviour
	{
		[Header("Rooms")]
		[SerializeField] private Room _pastTimeRoom;
		[SerializeField] private Room _currentTimeRoom;

		private void Awake()
		{
			ValidateRooms();
		}
		
		private void ValidateRooms()
		{
			if (_pastTimeRoom == null || _currentTimeRoom == null)
			{
				Debug.LogWarning("Missing room setup, trying to set up automatically.", this);
				Room[] rooms = GetComponentsInChildren<Room>(true);
				if (rooms.Length >= 2)
				{
					_pastTimeRoom = rooms[0];
					_currentTimeRoom = rooms[1];
				}
				else
				{
					Debug.LogError("RoomHolder missing Room child components", this);
				}
			}
		}

		public void ShowRoom(TimePeriod period)
		{
			switch (period)
			{
				case TimePeriod.Past:
					_currentTimeRoom.Hide();
					_pastTimeRoom.Show();
					break;
				case TimePeriod.Current:
					_pastTimeRoom.Hide();
					_currentTimeRoom.Show();
					break;
				default:
					Debug.LogError("Invalid time period", this);
					break;
			}
		}
	}
}