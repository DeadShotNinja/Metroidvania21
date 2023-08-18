using UnityEngine;

namespace Metro
{
	public class Room : MonoBehaviour
	{
		public void Show()
		{
			gameObject.SetActive(true);
		}
		
		public void Hide()
		{
			gameObject.SetActive(false);
		}
	}
}