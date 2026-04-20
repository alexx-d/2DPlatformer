using UnityEngine;

public class MeleeAttacker : MonoBehaviour
{
    [SerializeField] private Transform _attackPoint;
    [SerializeField] private float _attackRange = 0.5f;
    [SerializeField] private LayerMask _enemyLayers;
    [SerializeField] private int _damage = 20;
    [SerializeField] private int _maxTargets = 10;

    private Collider2D[] _hitResults;

    private void Awake()
    {
        _hitResults = new Collider2D[_maxTargets];
    }

    public void Attack()
    {
        int hitCount = Physics2D.OverlapCircleNonAlloc(_attackPoint.position, _attackRange, _hitResults, _enemyLayers);

        for (int i = 0; i < hitCount; i++)
        {
            if (_hitResults[i].TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(_damage);
            }

            _hitResults[i] = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (_attackPoint == null) return;
        Gizmos.DrawWireSphere(_attackPoint.position, _attackRange);
    }
}