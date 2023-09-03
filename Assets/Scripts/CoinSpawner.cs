using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private int _coinsCount;

    private List<Transform> _spawnPoints;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<Transform>().ToList();

        if (_coinsCount > _spawnPoints.Count)
        {
            _coinsCount = _spawnPoints.Count;
        }

        for (int i = 0; i < _coinsCount; i++)
        {
            var randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

            Instantiate(_coinPrefab, randomSpawnPoint.position, Quaternion.identity);
            _spawnPoints.Remove(randomSpawnPoint);
        }
    }
}
