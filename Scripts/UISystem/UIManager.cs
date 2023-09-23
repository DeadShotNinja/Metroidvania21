using PixelCrushers;
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
			EventManager.StartListening<GameStateChangedEvent>(OnGameStateChanged);
		}

		private void OnDisable()
		{
			EventManager.StopListening<ScreenFadeEvent>(OnFadeScreen);
			EventManager.StopListening<WinGameEvent>(OnWinGame);
            EventManager.StopListening<GameStateChangedEvent>(OnGameStateChanged);
        }
		
		private void OnFadeScreen(ScreenFadeEvent eventData)
		{
			_fadePanel.FadeScreen(eventData.FadeType, eventData.FadeDuration);
		}
		
		private void OnWinGame(WinGameEvent eventData)
		{
			//_displayPanel.gameObject.SetActive(true);
			EventManager.TriggerEvent(new ScreenFadeEvent(ScreenFadeType.In, 3f));
			Invoke(nameof(GoToCredits), 3f);
		}

		private void GoToCredits()
		{
			SceneLoader.LoadScene(4);
		}

		private void OnGameStateChanged(GameStateChangedEvent eventData)
		{
			if (eventData.State == GameState.Playing)
			{
                _pausePanel.gameObject.SetActive(false);
            }
			else
			{
                _pausePanel.gameObject.SetActive(true);
            }			
		}
		
		public void OnClick_GTFOButton()
		{
			Application.Quit();
		}
	}
}