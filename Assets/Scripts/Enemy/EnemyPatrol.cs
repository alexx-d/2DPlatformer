using System.Collections;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform _pathRoot;
    [SerializeField] private float _waitTime = 1f;
    [SerializeField] private float _stopThreshold = 0.1f;

    private Transform[] _waypoints;
    private int _currentPointIndex;
    private float _nextMoveTime;
    private bool _isWaiting;

    public bool IsWaiting => _isWaiting;

    private void Awake()
    {
        InitializeWaypoints();
    }

    public Vector2 GetCurrentWaypoint()
    {
        return _waypoints[_currentPointIndex].position;
    }

    public void UpdatePatrolLogic()
    {
        if (_waypoints == null || _waypoints.Length <= 1)
        {
            return;
        }

        if (_isWaiting)
        {
            if (Time.time >= _nextMoveTime)
            {
                _isWaiting = false;
                _currentPointIndex = ++_currentPointIndex % _waypoints.Length;
            }
            return;
        }

        if (Mathf.Abs(transform.position.x - GetCurrentWaypoint().x) <= _stopThreshold)
        {
            _isWaiting = true;

            _nextMoveTime = Time.time + _waitTime;
        }
    }

    private void InitializeWaypoints()
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
    }
}