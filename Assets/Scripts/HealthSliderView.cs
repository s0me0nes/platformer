using UnityEngine;
using UnityEngine.UI;

public class HealthSliderView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;

    private void OnEnable()
    {
        _health.HealthChanged += ShowHealthSlider;
        ShowHealthSlider(_health.CurrentHealth, _health.MaxHealth);
    }

    private void OnDisable()
    {
        _health.HealthChanged -= ShowHealthSlider;
    }

    private void ShowHealthSlider(int current, int max)
    {
        _slider.maxValue = max;
        _slider.value = current;
    }
}