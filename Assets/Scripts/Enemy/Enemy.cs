using UnityEditor.Experimental.GraphView;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private EnemyVision _vision;
    [SerializeField] private EnemyCombat _combat;
    [SerializeField] private EnemyMovement _movement;
    [SerializeField] private EnemyPatrol _patrol;
    [SerializeField] private EnemyAnimation _animation;
    [SerializeField] private EnemyAnimationEvents _animationEvents;

    private Transform _chaseTarget;
    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _vision.PlayerSpotted += OnPlayerSpotted;
        _vision.PlayerLost += OnPlayerLost;

        _animationEvents.Attacked += _combat.ExecuteHit;
    }

    private void OnDisable()
    {
        _vision.PlayerSpotted -= OnPlayerSpotted;
        _vision.PlayerLost -= OnPlayerLost;

        _animationEvents.Attacked -= _combat.ExecuteHit;
    }

    private void Update()
    {
        HandleAnimation();

        if (_chaseTarget != null)
        {
            HandleChase();
        }
        else
        {
            HandlePatrol();
        }
    }

    private void HandleChase()
    {
        _movement.SetTarget(_chaseTarget.position);
        _combat.TryStartAttack();
    }

    private void HandlePatrol()
    {
        _patrol.UpdatePatrolLogic();

        if (_patrol.IsWaiting)
        {
            _movement.Stop();
        }
        else
        {
            _movement.SetTarget(_patrol.GetCurrentWaypoint());
        }
    }

    private void HandleAnimation()
    {
        if (_animation != null && _rigidbody != null)
        {
            _animation.SetSpeed(Mathf.Abs(_rigidbody.velocity.x));
        }
    }

    private void OnPlayerSpotted(Transform target)
    {
        _chaseTarget = target;
        _combat.SetTarget(target);

        if (_animation != null)
        {
            _animation.SetAggressive(true);
        }
    }

    private void OnPlayerLost()
    {
        _chaseTarget = null;
        _combat.SetTarget(null);

        if (_animation != null)
        {
            _animation.SetAggressive(false);
        }
    }
}