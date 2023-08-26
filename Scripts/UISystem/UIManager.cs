using System;
using UnityEngine;

namespace Metro
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] private GameObject _UIPanel;
		
		private void Awake()
		{
			_UIPanel.SetActive(false);
		}

		private void OnEnable()
		{
			EventManager.StartListening<WinGameEvent>(OnWinGame);
		}

		private void OnDisable()
		{
			EventManager.StopListening<WinGameEvent>(OnWinGame);
		}
		
		private void OnWinGame(WinGameEvent eventData)
		{
			_UIPanel.SetActive(true);
		}
		
		public void OnClick_GTFOButton()
		{
			Application.Quit();
		}
	}
}