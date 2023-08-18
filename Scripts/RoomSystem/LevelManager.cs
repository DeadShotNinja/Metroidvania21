using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Metro
{
	public enum TimePeriod
	{
		Past,
		Current
	}
	
	public class LevelManager : MonoBehaviour
	{
		[Header("Room Settings")]
		[Tooltip("List of room prefabs that will be used throughout the game. (Note: Order matters)")]
		[SerializeField] private GameObject[] _roomHolderPrefabs;
		[Tooltip("DON'T CHANGE THIS UNLESS YOU KNOW WHAT YOU ARE DOING!!!!")]
		[SerializeField] private float _roomWidth = 80f;
		[Tooltip("DON'T CHANGE THIS UNLESS YOU KNOW WHAT YOU ARE DOING!!!!")]
		[SerializeField] private float _roomHeight = 40f;

		[Header("Grid Settings")]
		[Tooltip("Width of the grid for the rooms to spawn in on (Note: if GridWidth x GridHeight is less than room" +
		         " count, will not spawn the remaining rooms!!!")]
		[SerializeField] private int _gridWidth = 3;
		[Tooltip("Width of the grid for the rooms to spawn in on (Note: if GridWidth x GridHeight is less than room" +
		         " count, will not spawn the remaining rooms!!!")]
		[SerializeField] private int _gridHeight = 3;

		private List<RoomHolder> _roomHolders = new List<RoomHolder>();
		
		private void Start()
		{
			SetupAllRooms();
		}
		
		private void SetupAllRooms()
		{
			int roomCount = 0;

			for (int y = 0; y < _gridHeight; y++)
			{
				for (int x = 0; x < _gridWidth; x++)
				{
					if (roomCount >= _roomHolderPrefabs.Length) return;

					Vector3 position = new Vector3(x * _roomWidth, -y * _roomHeight, 0f);
					GameObject go = Instantiate(_roomHolderPrefabs[roomCount], position, quaternion.identity, transform);
					
					if (go.TryGetComponent(out RoomHolder roomHolder))
					{
						_roomHolders.Add(roomHolder);
						roomHolder.ShowRoom(TimePeriod.Current);
					}
					else
					{
						Debug.LogError("A room prefab is missing a Room component.");
					}

					roomCount++;
				}
			}
		}
	}
}