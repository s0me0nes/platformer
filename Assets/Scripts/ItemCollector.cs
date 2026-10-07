using UnityEngine;

[RequireComponent(typeof(Health))]
public class ItemCollector : MonoBehaviour
{
    // [SerializeField] private ScoreDisplay _scoreDisplay;
    private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
     //       _scoreDisplay.AddScore(1);

            coin.CoinCollect();
        }
        else if (collision.TryGetComponent(out HealthKit kit))
        {
            _health.Heal(kit.HealAmount);
            kit.HealthCollect();
        }
    }
}