using System;
using UnityEngine;

namespace Metro
{
	// Defines the Class, Type and Data for AudioData.
	[Serializable]
	public class AudioData<T>
	{
		public T AudioType;
		public AK.Wwise.Event AudioEvent;
	}
}