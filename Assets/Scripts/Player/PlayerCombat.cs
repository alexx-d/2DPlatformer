using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(MeleeAttacker))]
public class PlayerCombat : MonoBehaviour
{
    private PlayerInput _playerInput;
    private MeleeAttacker _attacker;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _attacker = GetComponent<MeleeAttacker>();
    }

    private void OnEnable()
    {
        _playerInput.AttackPerformed += _attacker.Attack;
    }

    private void OnDisable()
    {
        _playerInput.AttackPerformed -= _attacker.Attack;
    }
}