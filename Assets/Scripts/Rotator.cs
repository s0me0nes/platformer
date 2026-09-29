using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed;

    private float _defaultSpeed = 2;

    private void Start()
    {
        _rotationSpeed = _rotationSpeed <= 0 ? _defaultSpeed : _rotationSpeed;
    }

    private void Update()
    {
        transform.Rotate(0f, _rotationSpeed, 0);
    }
}
