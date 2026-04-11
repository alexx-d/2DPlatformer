using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _reward = 1;

    public int Collect()
    {
        Destroy(gameObject);
        return _reward;
    }
}