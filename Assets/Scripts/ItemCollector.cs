using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private ScoreDisplay _scoreDisplay;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Coin>(out Coin coin))
        {
            _scoreDisplay.AddScore(1);

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.TryGetComponent<HealthKit>(out HealthKit kit))
        {
            Debug.Log("Health UP!");
            Destroy(collision.gameObject);
        }
    }
}
