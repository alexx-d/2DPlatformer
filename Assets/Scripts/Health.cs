using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;

    public event Action<int> Changed;
    public event Action Died;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0)
        {
            return;
        }

        _currentHealth -= damage;
        Changed?.Invoke(_currentHealth); 

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        _currentHealth += amount;

        if (_currentHealth > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }

        Changed?.Invoke(_currentHealth);
    }

    private void Die()
    {
        Died?.Invoke();
        Destroy(gameObject);
    }
}