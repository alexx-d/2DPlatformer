using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out MonoBehaviour item))
        {
            HandlePickup(item);
        }
    }

    private void HandlePickup(MonoBehaviour item)
    {
        switch (item)
        {
            case Coin coin:
                _player.Wallet.AddCoins(coin.Amount);
                coin.Collect();
                break;

            case MedKit medKit:
                if (_player.Health.CurrentHealth < _player.Health.MaxHealth)
                {
                    _player.Health.Heal(medKit.HealPower);
                    medKit.Collect();
                }
                break;
        }
    }
}
