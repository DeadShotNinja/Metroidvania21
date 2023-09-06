using System.Collections.Generic;
using UnityEngine;

namespace Metro
{
	[CreateAssetMenu(menuName = "Database/Audio/Collections/Entity", fileName = "New Entity Collection")]
	public class EntityAudioCollectionSO : ScriptableObject
	{
		[Header("Entity Audio Collection")]
		[SerializeField] private List<AudioData<EntityAudioType>> _entityAudio = new List<AudioData<EntityAudioType>>();
		
		public List<AudioData<EntityAudioType>> EntityAudio => _entityAudio;
	}
}