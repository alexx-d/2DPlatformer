using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    private int _coinsCount;

    public int TotalCoins => _coinsCount;

    public void AddCoins(int amount)
    {
        if (amount < 0)
        {
            return;
        }

        _coinsCount += amount;
    }
}