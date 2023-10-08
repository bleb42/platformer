using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private int _objectCount;

    private List<Transform> _spawnPoints;

    private void Start()
    {
        _spawnPoints = GetComponentsInChildren<Transform>().ToList();

        if (_objectCount > _spawnPoints.Count)
        {
            _objectCount = _spawnPoints.Count;
        }

        for (int i = 0; i < _objectCount; i++)
        {
            var randomSpawnPoint = _spawnPoints[Random.Range(0, _spawnPoints.Count)];

            Instantiate(_prefab, randomSpawnPoint.position, Quaternion.identity);
            _spawnPoints.Remove(randomSpawnPoint);
        }
    }
}
