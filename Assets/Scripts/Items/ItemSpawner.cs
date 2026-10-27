using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _points;
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private HealthKit _healthKitPrefab;
    [SerializeField] private float _spawnTime = 2f;

    [SerializeField, Range(1, 10)] private int _maxCoinSpawnCount = 5;
    [SerializeField, Range(1, 5)] private int _maxHealthKitSpawnCount = 2;

    private List<Transform> _freePoints = new List<Transform>();

    private void Start()
    {
        _freePoints = new List<Transform>(_points);

        StartCoroutine(SpawnPerSeconds());
    }


    private IEnumerator SpawnPerSeconds()
    {
        var timer = new WaitForSeconds(_spawnTime);

        while ((_maxCoinSpawnCount > 0 || _maxHealthKitSpawnCount > 0) && _freePoints.Count > 0)
        {
            SpawnCoins();
            SpawnHealthKits();
            yield return timer;
        }
    }

    private void SpawnCoins()
    {
        if (_maxCoinSpawnCount > 0)
        {
            if (SpawnInFreePoint(_coinPrefab.gameObject))
            {
                _maxCoinSpawnCount--;
            }
        }
    }

    private void SpawnHealthKits()
    {
        if (_maxHealthKitSpawnCount > 0)
        {
            if (SpawnInFreePoint(_healthKitPrefab.gameObject))
            {
                _maxHealthKitSpawnCount--;
            }
        }
    }

    private bool SpawnInFreePoint(GameObject item)
    {
        if (_freePoints.Count == 0)
        {
            return false;
        }

        int rand = Random.Range(0, _freePoints.Count);

        GameObject obj = Instantiate(item, _freePoints[rand].position, Quaternion.identity);

        Coin coin = obj.GetComponent<Coin>();
        HealthKit healthKit = obj.GetComponent<HealthKit>();
        
        if (coin != null)
        {
            coin.Collected += DestroyObject;
        }

        if (healthKit != null)
        {
            healthKit.Collect += DestroyObject;
        }

        RemovePoint(_freePoints[rand]);

        return true;
    }

    private void RemovePoint(Transform point)
    {
        _freePoints.Remove(point);
    }

    private void DestroyObject(Coin coin)
    {
        coin.Collected -= DestroyObject;
        Destroy(coin.gameObject);
    }

    private void DestroyObject(HealthKit kit)
    {
        kit.Collect -= DestroyObject;
        Destroy(kit.gameObject);
    }
}