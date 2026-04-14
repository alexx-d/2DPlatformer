using System.Collections;
using UnityEngine;

[RequireComponent (typeof(EnemyMovement))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform _pathRoot;
    [SerializeField] private float _waitTime = 1f;

    private const float StopThreshold = 0.1f;

    private Transform[] _waypoints;
    private EnemyMovement _movement;
    private int _currentPointIndex;
    private Coroutine _patrolCoroutine;
    private WaitForSeconds _wait;

    private void Awake()
    {
        _movement = GetComponent<EnemyMovement>();
        _wait = new WaitForSeconds(_waitTime);
    }

    private void Start()
    {
        if (_pathRoot == null)
        {
            enabled = false;
            return;
        }

        _waypoints = new Transform[_pathRoot.childCount];

        for (int i = 0; i < _pathRoot.childCount; i++)
        {
            _waypoints[i] = _pathRoot.GetChild(i);
        }

        if (_waypoints.Length > 0)
        {
            _patrolCoroutine = StartCoroutine(PatrolRoutine());
        }
    }

    private IEnumerator PatrolRoutine()
    {
        while (enabled)
        {
            Transform target = _waypoints[_currentPointIndex];
            _movement.SetTarget(target.position);

            while (Mathf.Abs(transform.position.x - target.position.x) > StopThreshold)
            {
                yield return null;
            }

            yield return _wait;

            _currentPointIndex = ++_currentPointIndex % _waypoints.Length;
        }
    }
}