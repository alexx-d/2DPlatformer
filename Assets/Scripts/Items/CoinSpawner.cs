using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform[] _spawnPoints;

    private void Start()
    {
        SpawnAll();
    }

    private void SpawnAll()
    {
        foreach (Transform point in _spawnPoints)
        {
            Instantiate(_coinPrefab, point.position, Quaternion.identity, transform);
        }
    }
}