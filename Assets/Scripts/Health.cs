using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    public int CurrentHealth => _currentHealth;
    public int MaxHealth => _maxHealth;

    public event Action<int,int> HealthChanged;

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;

    private int _minHealth = 0;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    private void NotifyHealthChanged()
    {
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        
        NotifyHealthChanged();

        if (_currentHealth <= _minHealth)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(int value)
    {
        _currentHealth += value;
        _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);

        NotifyHealthChanged();
    }
}
