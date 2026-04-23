using UnityEngine;

[RequireComponent(typeof(MeleeAttacker))]
public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private float _attackRange = 1.5f;

    private MeleeAttacker _attacker;
    private EnemyAnimation _enemyAnimation;

    private float _nextAttackTime;
    private float _sqrAttackRange;
    private Transform _target;

    private void Awake()
    {
        _attacker = GetComponent<MeleeAttacker>();
        _enemyAnimation = GetComponentInParent<EnemyAnimation>();

        _sqrAttackRange = _attackRange * _attackRange;
    }

    public void SetTarget(Transform target) => _target = target;

    public void TryStartAttack()
    {
        if (_target == null)
        {
            return;
        }

        if (IsTargetInAttackRange() && Time.time >= _nextAttackTime)
        {
            _nextAttackTime = Time.time + _attackCooldown;

            if (_enemyAnimation != null)
            {
                _enemyAnimation.PlayAttack();
            }
        }
    }

    public void ExecuteHit()
    {
        if (_target != null && IsTargetInAttackRange())
        {
            _attacker.Attack();
        }
    }

    private bool IsTargetInAttackRange()
    {
        Vector2 offset = (Vector2)_target.position - (Vector2)transform.position;
        return offset.sqrMagnitude <= _sqrAttackRange;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
    }
}