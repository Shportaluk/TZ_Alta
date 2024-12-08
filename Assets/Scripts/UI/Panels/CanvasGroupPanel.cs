using System;
using System.Collections;
using UnityEngine;

public class CanvasGroupPanel : AbstractPanel
{
	[SerializeField] private CanvasGroup _canvasGroup;
	[SerializeField] private float _showDuration = 0.2f;
	[SerializeField] private float _hideDuration = 0.3f;
	[SerializeField] private float _showValue = 1f;
	[SerializeField] private float _hideValue = 0f;

	private Coroutine _coroutine;


	private void OnDestroy()
	{
		TryStopCoroutine(_coroutine);
	}

	public override void Show()
    {
		TryStopCoroutine(_coroutine);
		_coroutine = StartCoroutine(Cor(_showValue, _showDuration, OnCompletedShow));
    }

	public override void Hide()
    {
		TryStopCoroutine(_coroutine);
		_coroutine = StartCoroutine(Cor(_hideValue, _hideDuration, OnCompletedHide));
	}

	private void TryStopCoroutine(Coroutine cor)
    {
		if(cor != null)
        {
			StopCoroutine(cor);
		}
    }

	private IEnumerator Cor(float targetValue, float duration, Action onCompleted)
    {
		float startValue = _canvasGroup.alpha;
		float time = 0;
		while(time < duration)
        {
			float t = time / duration;
			_canvasGroup.alpha = Mathf.Lerp(startValue, targetValue, t);
			yield return null;
			time += Time.deltaTime;
        }
		_canvasGroup.alpha = targetValue;

		onCompleted?.Invoke();
    }

	private void OnCompletedShow()
	{
		_canvasGroup.interactable = true;
		_canvasGroup.blocksRaycasts = true;
	}

	private void OnCompletedHide()
	{
		_canvasGroup.interactable = false;
		_canvasGroup.blocksRaycasts = false;
	}

	public enum States
	{
		Showed = 0,
		Showing = 1,

		Hided = 10,
		Hiding = 11,
	}
}