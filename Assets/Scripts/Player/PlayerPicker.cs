using UnityEngine;

public class PlayerPicker : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ICollectible collectible))
        {
            collectible.Collect(gameObject);
        }
    }
}
