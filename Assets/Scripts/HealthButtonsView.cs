using UnityEngine;
using UnityEngine.UI;

public class HealthButtonsView : MonoBehaviour
{
    [SerializeField] private Health _health;

    [SerializeField] private Button _takeDamageBtn;
    [SerializeField] private Button _healingBtn;

    private int _damageValue = 10;
    private int _healValue = 10;

    private void OnEnable()
    {
        _takeDamageBtn.onClick.AddListener(OnTakeDamageClicked);
        _healingBtn.onClick.AddListener(OnHealClicked);
    }

    private void OnDisable()
    {
        _takeDamageBtn.onClick.RemoveListener(OnTakeDamageClicked);
        _healingBtn.onClick.RemoveListener(OnHealClicked);
    }

    private void OnTakeDamageClicked() => _health.TakeDamage(_damageValue);
    private void OnHealClicked() => _health.Heal(_healValue);
}