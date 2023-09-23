using System.Collections.Generic;
using UnityEngine;

namespace Metro
{
	// Creates Scriptable Object for Environment Wwise Events to be defined and held. Reusable Tool for adding in sounds that can be hot-swapped.
	[CreateAssetMenu(menuName = "Database/Audio/Collections/Environment", fileName = "New Environment Collection")]
	public class EnvironmentAudioCollectionSO : ScriptableObject
	{
		[Header("Environment Audio Collection")]
		[SerializeField] private List<AudioData<EnvironmentAudioType>> _environmentAudio = new List<AudioData<EnvironmentAudioType>>();
		
		public List<AudioData<EnvironmentAudioType>> EnvironemntAudio => _environmentAudio;
	}
}