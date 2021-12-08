using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int _addCount = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharactorCoins>())
        {
            other.GetComponent<CharactorCoins>().AddCoin(_addCount);
            Destroy(gameObject);
        }
    }
}
