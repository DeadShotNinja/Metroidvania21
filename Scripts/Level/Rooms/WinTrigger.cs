using System;
using UnityEngine;

namespace Metro
{
	public class WinTrigger : MonoBehaviour
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.CompareTag("Player"))
			{
				EventManager.TriggerEvent(new WinGameEvent());
			}
		}
	}
}