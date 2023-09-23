using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace Metro
{
	public class FadePanel : MonoBehaviour
	{
		private Image _fadeImage;
		private Coroutine _currentFadeCoroutine;

		private void Awake()
		{
			_fadeImage = GetComponent<Image>();
		}
		
		public void FadeScreen(ScreenFadeType fadeType, float fadeDuration)
		{
			gameObject.SetActive(true);
			
			switch (fadeType)
			{
				case ScreenFadeType.In:
					StopCurrentFadeCoroutine();
					_currentFadeCoroutine = StartCoroutine(FadeIn(fadeDuration));
					break;
				case ScreenFadeType.Out:
					StopCurrentFadeCoroutine();
					_currentFadeCoroutine = StartCoroutine(FadeOut(fadeDuration));
					break;
			}
		}

		private IEnumerator FadeIn(float fadeDuration)
		{
			gameObject.SetActive(true);
			
			for (float t = 0f; t <= 1; t += Time.deltaTime / fadeDuration)
			{
				Color newColor = new Color(0f, 0f, 0f, Mathf.Lerp(0f, 1f, t));
				_fadeImage.color = newColor;
				yield return null;
			}
			
			SetFinalColor(1f);
			_currentFadeCoroutine = null;
		}
		
		private IEnumerator FadeOut(float fadeDuration)
		{
			for (float t = 0f; t <= 1f; t += Time.deltaTime / fadeDuration)
			{
				Color newColor = new Color(0f, 0f, 0f, Mathf.Lerp(1f, 0f, t));
				_fadeImage.color = newColor;
				yield return null;
			}
			
			SetFinalColor(0f);
			_currentFadeCoroutine = null;
			gameObject.SetActive(false);
		}
		
		private void SetFinalColor(float alpha)
		{
			Color finalColor = new Color(0f, 0f, 0f, alpha);
			_fadeImage.color = finalColor;
		}
		
		private void StopCurrentFadeCoroutine()
		{
			if (_currentFadeCoroutine != null)
			{
				StopCoroutine(_currentFadeCoroutine);
			}
		}
	}
}