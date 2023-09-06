using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Metro
{
	public class UIManager : MonoBehaviour
	{
		[Header("UI Panels")]
		[SerializeField] private HUDPanel _hudPanel;
		[SerializeField] private PausePanel _pausePanel;
		[SerializeField] private DisplayPanel _displayPanel;
		[SerializeField] private ConfirmationPanel _confirmationPanel;

		[Header("Fade Setup")]
		[SerializeField] private FadePanel _fadePanel;
		
		private void Awake()
		{
			if (_hudPanel != null) _hudPanel.gameObject.SetActive(true);
			if (_pausePanel != null) _pausePanel.gameObject.SetActive(false);
            if (_displayPanel != null) _displayPanel.gameObject.SetActive(false);
			if (_confirmationPanel != null) _confirmationPanel.gameObject.SetActive(false);
		}

		private void OnEnable()
		{
			EventManager.StartListening<ScreenFadeEvent>(OnFadeScreen);
			EventManager.StartListening<WinGameEvent>(OnWinGame);
		}

		private void OnDisable()
		{
			EventManager.StopListening<ScreenFadeEvent>(OnFadeScreen);
			EventManager.StopListening<WinGameEvent>(OnWinGame);
		}
		
		private void OnFadeScreen(ScreenFadeEvent eventData)
		{
			_fadePanel.FadeScreen(eventData.FadeType, eventData.FadeSpeed);
		}
		
		private void OnWinGame(WinGameEvent eventData)
		{
			_displayPanel.gameObject.SetActive(true);
		}
		
		public void OnClick_GTFOButton()
		{
			Application.Quit();
		}
	}
}