using UnityEngine;
using UnityEngine.UI;

public class HealthBarView : MonoBehaviour
{
    [SerializeField] protected Health Health;
    [SerializeField] protected Slider Slider;

    private void OnEnable()
    {
        Health.HealthChanged += ShowHealthSlider;
        ShowHealthSlider(Health.CurrentHealth, Health.MaxHealth);
    }

    private void OnDisable()
    {
        Health.HealthChanged -= ShowHealthSlider;
    }

    protected virtual void ShowHealthSlider(int current, int max)
    {
        if (max <= 0)
        {
            return;
        }

        Slider.value = current / (float)max;
    }
}
