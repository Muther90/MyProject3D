using System.Collections;
using UnityEngine;

public class BombRenderer : MonoBehaviour
{
    [SerializeField] private GroupRendererController _groupRendererController;
    [SerializeField] private float _fadeOutRate;

    private WaitForSeconds _waitForUpdate;
    private Coroutine _fadeOutCoroutine;

    public void StartFadeOut(float duration)
    {
        if (_fadeOutCoroutine != null)
        {
            StopCoroutine(_fadeOutCoroutine);
        }

        _fadeOutCoroutine = StartCoroutine(FadeOutCoroutine(duration));
    }

    private IEnumerator FadeOutCoroutine(float duration)
    {
        if (duration > 0)
        {
            float startAlpha = _groupRendererController.Alpha;
            float targetAlpha = 0f;
            _waitForUpdate = new WaitForSeconds(_fadeOutRate);

            int steps = Mathf.CeilToInt(duration / _fadeOutRate);

            for (int i = 0; i <= steps; i++)
            {
                float progress = (float)i / steps;
                _groupRendererController.SetAlphaForAll(Mathf.Lerp(startAlpha, targetAlpha, progress));

                yield return _waitForUpdate;
            }
        }

        _groupRendererController.SetAlphaForAll(0);

        _fadeOutCoroutine = null;
    }
}