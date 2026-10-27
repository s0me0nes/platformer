using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthSmoothSliderView : HealthBarView
{
    private Coroutine _coroutine;

    protected override void ShowHealthSlider(int current, int max)
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
        }

        _coroutine = StartCoroutine(MoveSlider(current, max));
    }

    private IEnumerator MoveSlider(int current, int max)
    {
        if (max <= 0)
        {
            yield break;
        }

        float startPoint = Slider.value;
        float endPoint = current / (float)max;

        float progress = 0;

        while (progress < 1)
        {
            progress += Time.deltaTime / 0.5f;

            if (progress > 1)
            {
                progress = 1;
            }

            Slider.value = Mathf.Lerp(startPoint, endPoint, progress);

            yield return null;
        }
    }
}

