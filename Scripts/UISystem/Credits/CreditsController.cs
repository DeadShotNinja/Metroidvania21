using Metro;
using MoreMountains.Feedbacks;
using System.Collections;
using UnityEngine;

public class CreditsController : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private RectTransform _creditsHolder;
    [SerializeField] private float _scrollSpeed;
    [SerializeField] private MMFeedbacks _fadeInFeedback;
    [SerializeField] private MMFeedbacks _fadeOutFeedback;

    private float _initialPosY;
    private float _endPosY;

    private void Start()
    {
        _fadeOutFeedback.PlayFeedbacks();

        float canvasHeight = _creditsHolder.parent.GetComponent<RectTransform>().rect.height;

        _initialPosY = -canvasHeight / 2 - _creditsHolder.rect.height / 2;
        _creditsHolder.anchoredPosition = new Vector2(_creditsHolder.anchoredPosition.x, _initialPosY);

        _endPosY = canvasHeight / 2 + _creditsHolder.rect.height / 2;

        Invoke(nameof(ScrollCredits), 1f);
    }

    private void ScrollCredits()
    {
        StartCoroutine(Scroll_Coroutine());
    }

    private IEnumerator Scroll_Coroutine()
    {
        while (_creditsHolder.anchoredPosition.y < _endPosY)
        {
            _creditsHolder.anchoredPosition += new Vector2(
                0f, _scrollSpeed * Time.deltaTime);
            yield return null;
        }

        _fadeInFeedback.PlayFeedbacks();
        Invoke(nameof(GoToMainMenu), 1f);
    }

    private void GoToMainMenu()
    {
        SceneLoader.LoadScene(2);
    }
}
