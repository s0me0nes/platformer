using System;
using UnityEngine;

[RequireComponent(typeof(Destroyer))]
public class Health : MonoBehaviour, IDamageable
{
    private readonly int _minHealth = 0;

    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currentHealth;
    
    public event Action<int,int> HealthChanged;
    public event Action Dead;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        NotifyHealthChanged();
    }


    public void TakeDamage(int damage)
    {
        if (damage < 0)
        {
            return;
        }

        _currentHealth -= damage;
        _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);
        
        NotifyHealthChanged();

        if (_currentHealth <= _minHealth)
        {
            Dead?.Invoke();
        }
    }

    public void Heal(int value)
    {
        if ( value < 0)
        {
            return;
        }

        _currentHealth += value;
        _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);

        NotifyHealthChanged();
    }
    private void NotifyHealthChanged()
    {
        HealthChanged?.Invoke(_currentHealth, _maxHealth);
    }
}
