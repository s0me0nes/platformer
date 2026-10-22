using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private Text _healthTxt;

    [SerializeField] private Slider _healthSliderNormal;
    [SerializeField] private Slider _healthSliderSmooth;

    [SerializeField] private Button _takeDamageBtn;
    [SerializeField] private Button _healingBtn;

    [SerializeField] private int _maxHealth;
    [SerializeField] private int _currentHealth;
    [SerializeField] private float _healthChangeSpeed = 50;

    private int _damageValue = 10;

    private void OnEnable()
    {
        _takeDamageBtn.onClick.AddListener(() => TakeDamage(_damageValue));

    }

    private void OnDisable()
    {
        _takeDamageBtn.onClick.RemoveAllListeners();
    }

    private void Awake()
    {
        SetHealthCurrentStatus();
    }

    private void Update()
    {
        if (_healthSliderSmooth.value != _currentHealth)
        {
            _healthSliderSmooth.value = Mathf.MoveTowards(_healthSliderSmooth.value, _currentHealth, _healthChangeSpeed * Time.deltaTime);
        }
    }

    private void SetHealthCurrentStatus()
    {
        _currentHealth = _maxHealth;

        _healthSliderNormal.maxValue = _maxHealth;
        _healthSliderSmooth.maxValue = _maxHealth;

        _healthSliderNormal.value = _currentHealth;
        _healthSliderSmooth.value = _currentHealth;

        ShowCurrentHealth();
    }

    private void ShowCurrentHealth()
    {
        _healthSliderNormal.value = _currentHealth;
        _healthTxt.text = _currentHealth + "/" + _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _currentHealth -= damage;
        
        ShowCurrentHealth();

        if (_currentHealth <= 0)
        {
            _healthSliderSmooth.value = 0;
            Destroy(gameObject);
        }
    }

    public void Heal(int value)
    {
        _currentHealth += value;
        _currentHealth = Mathf.Clamp(_currentHealth, 0, _maxHealth);

        ShowCurrentHealth();
    }
}
