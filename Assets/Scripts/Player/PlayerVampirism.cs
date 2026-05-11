using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class PlayerVampirism : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldown = 4f;
    [SerializeField] private float _tickRate = 0.5f;
    [SerializeField] private int _damagePerTick = 5;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Health _playerHealth;

    private const int MaxTargets = 10;
    private readonly Collider2D[] _overlapResults = new Collider2D[MaxTargets];

    private Coroutine _activeRoutine;
    private WaitForSeconds _wait;
    private bool _isReady = true;

    public event Action<float> StateChanged;

    public float Radius => _radius;

    private void Awake()
    {
        _wait = new WaitForSeconds(_tickRate);
    }

    public void TryActivate()
    {
        if (_isReady)
        {
            _activeRoutine = StartCoroutine(AbilityRoutine());
        }
    }

    private IEnumerator AbilityRoutine()
    {
        _isReady = false;
        StateChanged?.Invoke(_duration);

        int totalTicks = Mathf.RoundToInt(_duration / _tickRate);
        float tickProgressStep = _tickRate / _duration;

        for (int i = 0; i < totalTicks; i++)
        {
            ProcessVampirismTick();
            yield return _wait;
        }

        StateChanged?.Invoke(_cooldown);

        float cooldownEndTime = Time.time + _cooldown;

        while (Time.time < cooldownEndTime)
        {
            yield return null;
        }

        _isReady = true;
        _activeRoutine = null;
    }

    private void ProcessVampirismTick()
    {
        Health target = GetClosestEnemy();

        if (target != null)
        {
            target.TakeDamage(_damagePerTick);
            _playerHealth.Heal(_damagePerTick);
        }
    }

    private Health GetClosestEnemy()
    {
        int count = Physics2D.OverlapCircleNonAlloc(
            transform.position,
            _radius,
            _overlapResults,
            _enemyLayer
        );

        Health closestHealth = null;
        float minDistanceSqr = Mathf.Infinity;
        Vector2 currentPosition = transform.position;

        for (int i = 0; i < count; i++)
        {
            if (_overlapResults[i].TryGetComponent(out Health enemyHealth))
            {
                if (enemyHealth.CurrentHealth <= 0)
                {
                    continue;
                }

                float distanceSqr = ((Vector2)_overlapResults[i].transform.position - currentPosition).sqrMagnitude;

                if (distanceSqr < minDistanceSqr)
                {
                    minDistanceSqr = distanceSqr;
                    closestHealth = enemyHealth;
                }
            }
        }

        return closestHealth;
    }
}