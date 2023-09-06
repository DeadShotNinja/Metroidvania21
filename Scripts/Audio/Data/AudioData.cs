using System;
using UnityEngine;

namespace Metro
{
	[Serializable]
	public class AudioData<T>
	{
		public T AudioType;
		public AK.Wwise.Event AudioEvent;
	}
}