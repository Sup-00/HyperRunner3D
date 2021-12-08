using UnityEngine;
using UnityEngine.UI;

public class CoinsUI : MonoBehaviour
{
    [SerializeField] private Text _coinCountText;

    public void RenderCoinsCount(int count)
    {
        _coinCountText.text = count.ToString();
    }
}
