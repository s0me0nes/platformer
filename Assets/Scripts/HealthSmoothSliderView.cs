using UnityEngine;
using UnityEngine.UI;

public class HealthSmoothSliderView : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private Slider _slider;

    [SerializeField] private float _healthChangeSpeed = 50;

    private bool _isSliderMoving;

    private void OnEnable()
    {
        _health.HealthChanged += ShowHealthSlider;
        _slider.maxValue = _health.MaxHealth;
        _slider.value = _health.CurrentHealth;
    }

    private void OnDisable()
    {
        _health.HealthChanged -= ShowHealthSlider;
    }

    private void Update()
    {
        if (_isSliderMoving)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _health.CurrentHealth, _healthChangeSpeed * Time.deltaTime);

            if (_slider.value == _health.CurrentHealth)
            {
                _isSliderMoving = false;
            }
        }
    }

    private void ShowHealthSlider(int current, int max)
    {
        _isSliderMoving = _slider.value != current;
    }
}

