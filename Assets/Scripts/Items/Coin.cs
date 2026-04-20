using UnityEngine;

public class Coin : MonoBehaviour, ICollectible
{
    [SerializeField] private int _reward = 1;

    public void Collect(GameObject player)
    {
        if (player.TryGetComponent(out PlayerWallet wallet))
        {
            wallet.AddCoins(_reward);
            Destroy(gameObject);
        }
    }
}