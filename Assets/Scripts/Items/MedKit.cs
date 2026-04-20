using UnityEngine;

public class MedKit : MonoBehaviour, ICollectible
{
    [SerializeField] private int _healAmount = 25;
    [SerializeField] private GameObject _pickUpEffect;

    public void Collect(GameObject player)
    {
        if (player.TryGetComponent(out Health health))
        {
            if (health.CurrentHealth < health.MaxHealth)
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
}