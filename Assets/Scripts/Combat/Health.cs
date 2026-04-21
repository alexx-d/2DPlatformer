using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable
{
    [SerializeField] private int _maxHealth = 100;
    private int _currentHealth;

    public event Action<int> Changed;
    public event Action Died;

    public int MaxHealth => _maxHealth;
    public int CurrentHealth => _currentHealth;

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth <= 0 || damage < 0)
        {
            return;
        }

        _currentHealth = Mathf.Clamp(_currentHealth - damage, 0, _maxHealth);

        Changed?.Invoke(_currentHealth); 

        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (amount <= 0 || _currentHealth >= _maxHealth)
        {
            return;
        }

        _currentHealth = Mathf.Clamp(_currentHealth + amount, 0, _maxHealth);

        Changed?.Invoke(_currentHealth);
    }

    private void Die()
    {
        Died?.Invoke();
        Destroy(gameObject);
    }
}