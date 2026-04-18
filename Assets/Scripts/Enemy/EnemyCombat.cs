using UnityEngine;

[RequireComponent(typeof(MeleeAttacker))]
public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float _attackCooldown = 1f;
    [SerializeField] private LayerMask _playerLayer;

    private MeleeAttacker _attacker;
    private EnemyAnimation _enemyAnimation;
    private float _lastAttackTime;
    private Transform _targetInRange;

    private void Awake()
    {
        _attacker = GetComponent<MeleeAttacker>();

        _enemyAnimation = GetComponentInParent<EnemyAnimation>();
    }

    private void Update()
    {
        if (_targetInRange != null && Time.time >= _lastAttackTime + _attackCooldown)
        {
            TryAttack();
        }
    }

    private void TryAttack()
    {
        _lastAttackTime = Time.time;

        if (_enemyAnimation != null)
        {
            _enemyAnimation.PlayAttack();
        }

        _attacker.Attack();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_playerLayer.Contains(other.gameObject.layer))
        {
            _targetInRange = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_playerLayer.Contains(other.gameObject.layer))
        {
            _targetInRange = null;
        }
    }
}