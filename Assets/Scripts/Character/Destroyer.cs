using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void OnEnable()
    {
        _health.Dead += KillCharacter;
    }

    private void OnDisable()
    {
        _health.Dead -= KillCharacter;
    }

    private void KillCharacter()
    {
        Destroy(gameObject);
    }
}

