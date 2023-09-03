using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;
    private float _defaultSpeed = 2;

    void Start()
    {
        if (_rotationSpeed == 0)
            _rotationSpeed = _defaultSpeed;
    }

    void Update()
    {
        transform.Rotate(0f, _rotationSpeed, 0);
    }
}
