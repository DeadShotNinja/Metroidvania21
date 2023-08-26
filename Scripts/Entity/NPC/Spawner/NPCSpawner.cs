using System;
using Unity.Mathematics;
using UnityEngine;

namespace Metro
{
	public class NPCSpawner : MonoBehaviour
	{
		[Header("Setup")]
		[SerializeField] private SpawnData[] _spawnDatas;

		public bool SpawnerInitialized { get; private set; }
		
		public void SpawnNPCs(Transform parent, SpawnPoint[] spawnPoints)
		{
			foreach (SpawnData spawn in _spawnDatas)
			{
				SpawnPoint point = Array.Find(spawnPoints, x => x.SpawnID == spawn.TargetSpawnID);
				
				if (point == null)
				{
					Debug.LogError("Spawn Point ID does not exist.", this);
					continue;
				}

				Vector3 pos = point.transform.position;
				
				GameObject go = Instantiate(spawn.NPCPrefab, pos, quaternion.identity, parent);
			}
			
			SpawnerInitialized = true;
		}
	}
}