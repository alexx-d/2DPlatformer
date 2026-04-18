using UnityEngine;

public class MedKit : MonoBehaviour
{
    [SerializeField] private int _healAmount = 25;
    [SerializeField] private GameObject _pickUpEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Health health))
        {
            health.Heal(_healAmount);

            if (_pickUpEffect != null)
            {
                Instantiate(_pickUpEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }
}