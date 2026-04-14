using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    private int _coinsCount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            _coinsCount += coin.Collect();
        }
    }
}