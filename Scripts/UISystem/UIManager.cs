using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Metro
{
	public class UIManager : MonoBehaviour
	{
		[SerializeField] private GameObject _UIPanel;

		[Header("Fade Settings")]
		[SerializeField] private Image _fadeImage;
		[SerializeField] private float _fadeSpeed = 1f;

		private Coroutine _currentFadeCoroutine;
		
		private void Awake()
		{
			_UIPanel.SetActive(false);
		}

		private void OnEnable()
		{
			EventManager.StartListening<PlayerDiedEvent>(OnPlayerDied);
			EventManager.StartListening<PlayerRespawnedEvent>(OnPlayerRespawned);
			EventManager.StartListening<WinGameEvent>(OnWinGame);
		}

		private void OnDisable()
		{
			EventManager.StopListening<PlayerDiedEvent>(OnPlayerDied);
			EventManager.StopListening<PlayerRespawnedEvent>(OnPlayerRespawned);
			EventManager.StopListening<WinGameEvent>(OnWinGame);
		}
		
		private void OnPlayerDied(PlayerDiedEvent eventData)
		{
			StopCurrentFadeCoroutine();
			_currentFadeCoroutine = StartCoroutine(FadeIn());
		}
		
		private void OnPlayerRespawned(PlayerRespawnedEvent eventData)
		{
			StopCurrentFadeCoroutine();
			_currentFadeCoroutine = StartCoroutine(FadeOut());
		}
		
		private void StopCurrentFadeCoroutine()
		{
			if (_currentFadeCoroutine != null)
			{
				StopCoroutine(_currentFadeCoroutine);
			}
		}
		
		private IEnumerator FadeIn()
		{
			_fadeImage.gameObject.SetActive(true);
			
			for (float t = 0f; t <= 1; t += Time.deltaTime * _fadeSpeed)
			{
				Color newColor = new Color(0f, 0f, 0f, Mathf.Lerp(0f, 1f, t));
				_fadeImage.color = newColor;
				yield return null;
			}
		}
		
		private IEnumerator FadeOut()
		{
			for (float t = 0f; t <= 1f; t += Time.deltaTime * _fadeSpeed)
			{
				Color newColor = new Color(0f, 0f, 0f, Mathf.Lerp(1f, 0f, t));
				_fadeImage.color = newColor;
				yield return null;
			}
			
			_fadeImage.gameObject.SetActive(false);
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