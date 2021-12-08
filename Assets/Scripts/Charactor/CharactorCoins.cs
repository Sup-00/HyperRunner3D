using UnityEngine;

public class CharactorCoins : MonoBehaviour
{
    [SerializeField] private CoinsUI _coinsUI;

    private int _cointCount = 0;

    private void Start()
    {
        _coinsUI.RenderCoinsCount(_cointCount);
    }

    public void AddCoin(int count)
    {
        _cointCount += count;
        _coinsUI.RenderCoinsCount(_cointCount);
    }

    public bool SubtractrCoins(int count)
    {
        if (_cointCount < count)
        {
            return false;
        }
        else
        {
            _cointCount -= count;
            _coinsUI.RenderCoinsCount(_cointCount);
            return true;
        }
    }
}