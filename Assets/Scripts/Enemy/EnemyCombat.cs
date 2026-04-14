using UnityEngine;

[RequireComponent (typeof(MeleeAttacker))]
public class EnemyCombat : MonoBehaviour
{
    [SerializeField] private float _attackCooldown = 1f;

    private MeleeAttacker _attacker;
    private float _lastAttackTime;

    private void Awake()
    {
        _attacker = GetComponent<MeleeAttacker>();
    }

    private void Update()
    {
        if (Time.time >= _lastAttackTime + _attackCooldown)
        {
            _attacker.Attack();
            _lastAttackTime = Time.time;
        }
    }
}
