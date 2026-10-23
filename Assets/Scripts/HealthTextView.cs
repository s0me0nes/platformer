using UnityEngine;
using UnityEngine.UI;

public class HealthTextView : MonoBehaviour
{
    [SerializeField] private Text _healthTxt;
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.HealthChanged += ShowHealthText;
        ShowHealthText(_health.CurrentHealth, _health.MaxHealth);
    }

    private void OnDisable()
    {
        _health.HealthChanged -= ShowHealthText;
    }

    private void ShowHealthText(int current, int max)
    {
        _healthTxt.text = current + "/" + max;
    }
}
