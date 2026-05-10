using UnityEngine;

[RequireComponent (typeof(Health))]
public class DeathHandler : MonoBehaviour
{
    [SerializeField] private Health _health;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    private void OnEnable()
    {
        _health.Died += HandleDeath;
    }

    private void OnDisable()
    {
        _health.Died -= HandleDeath;
    }     

    private void HandleDeath()
    {
        Destroy(gameObject);
    }
}