using System.Collections.Generic;
using UnityEngine;

namespace Metro
{
	// Creates Scriptable Object for Enemy Wwise Events to be defined and held. Reusable Tool for adding in sounds that can be hot-swapped.
	[CreateAssetMenu(menuName = "Database/Audio/Collections/Enemy", fileName = "New Enemy Collection")]
	public class EnemyAudioCollectionSO : ScriptableObject
	{
		[Header("Enemy Audio Collection")]
		[SerializeField] private List<AudioData<EnemyAudioType>> _enemyAudio = new List<AudioData<EnemyAudioType>>();
		
		public List<AudioData<EnemyAudioType>> EnemyAudio => _enemyAudio;
	}
}