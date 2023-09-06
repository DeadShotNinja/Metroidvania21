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
		
		public void FadeScreen(ScreenFadeType fadeType, float fadeSpeed)
		{
			gameObject.SetActive(true);
			
			switch (fadeType)
			{
				case ScreenFadeType.In:
					StopCurrentFadeCoroutine();
					_currentFadeCoroutine = StartCoroutine(FadeIn(fadeSpeed));
					break;
				case ScreenFadeType.Out:
					StopCurrentFadeCoroutine();
					_currentFadeCoroutine = StartCoroutine(FadeOut(fadeSpeed));
					break;
			}
		}

		private IEnumerator FadeIn(float fadeSpeed)
		{
			gameObject.SetActive(true);
			
			for (float t = 0f; t <= 1; t += Time.deltaTime * fadeSpeed)
			{
				Color newColor = new Color(0f, 0f, 0f, Mathf.Lerp(0f, 1f, t));
				_fadeImage.color = newColor;
				yield return null;
			}
			
			_currentFadeCoroutine = null;
		}
		
		private IEnumerator FadeOut(float fadeSpeed)
		{
			for (float t = 0f; t <= 1f; t += Time.deltaTime * fadeSpeed)
			{
				Color newColor = new Color(0f, 0f, 0f, Mathf.Lerp(1f, 0f, t));
				_fadeImage.color = newColor;
				yield return null;
			}
			
			_currentFadeCoroutine = null;
			gameObject.SetActive(false);
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