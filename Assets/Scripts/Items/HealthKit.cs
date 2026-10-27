using System;
using UnityEngine;

public class HealthKit : MonoBehaviour
{
    public int HealAmount {  get; private set; }

    public event Action<HealthKit> Collect;

    [SerializeField] private int _healAmount = 10;

    private void Awake()
    {
        HealAmount = _healAmount;
    }

    public void HealthCollect()
    {
        Collect?.Invoke(this);
    }
}
