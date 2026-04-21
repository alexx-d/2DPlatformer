using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : Pickup
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 10;
    [SerializeField] private int _poolMaxSize = 20;

    [SerializeField] private Transform[] _spawnPoints;

    private ObjectPool<T> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: CreateItem,
            actionOnGet: OnGetFromPool,
            actionOnRelease: OnReleaseFromPool,
            actionOnDestroy: OnDestroyPoolObject,
            collectionCheck: false,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
        );
    }

    private void Start()
    {
        SpawnInitialItems();
    }

    private void SpawnInitialItems()
    {
        foreach (var point in _spawnPoints)
        {
            T item = _pool.Get();
            item.transform.position = point.position;
        }
    }

    private T CreateItem()
    {
        return Instantiate(_prefab);
    }

    private void OnGetFromPool(T item)
    {
        item.gameObject.SetActive(true);
        item.Collected += OnItemCollected;
    }

    private void OnReleaseFromPool(T item)
    {
        item.Collected -= OnItemCollected;
        item.gameObject.SetActive(false);
    }

    private void OnDestroyPoolObject(T item)
    {
        Destroy(item.gameObject);
    }

    private void OnItemCollected(Pickup item)
    {
        if (item is T specificItem)
        {
            _pool.Release(specificItem);
        }
    }
}