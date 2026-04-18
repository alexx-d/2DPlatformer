using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyPatrol), typeof(EnemyMovement))]
public class EnemyBehavior : MonoBehaviour
{
    private EnemyVision _vision;
    private EnemyPatrol _patrol;
    private EnemyMovement _movement;

    private Transform _chaseTarget;
    private EnemyAnimation _enemyAnimation;

    private void Awake()
    {
        _vision = GetComponentInChildren<EnemyVision>();

        _patrol = GetComponent<EnemyPatrol>();
        _movement = GetComponent<EnemyMovement>();

        _enemyAnimation = GetComponent<EnemyAnimation>();

        if (_vision == null)
        {
            Debug.LogError("EnemyVision не найден в дочерних объектах!");
        }
    }

    private void OnEnable()
    {
        _vision.PlayerSpotted += OnPlayerSpotted;
        _vision.PlayerLost += OnPlayerLost;
    }

    private void OnDisable()
    {
        _vision.PlayerSpotted -= OnPlayerSpotted;
        _vision.PlayerLost -= OnPlayerLost;
    }

    private void Start()
    {
        OnPlayerLost();
    }

    private void Update()
    {
        if (_chaseTarget != null)
        {
            _movement.SetTarget(_chaseTarget.position);
        }
    }

    private void OnPlayerSpotted(Transform target)
    {
        _patrol.StopPatrol();

        if (_enemyAnimation != null)
        {
            _enemyAnimation.SetAggressive(true);
        }

        _chaseTarget = target;
    }

    private void OnPlayerLost()
    {
        _chaseTarget = null;

        if (_enemyAnimation != null)
        {
            _enemyAnimation.SetAggressive(false);
        }

        _patrol.StartPatrol();
    }
}