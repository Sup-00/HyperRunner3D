using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private FinishLine finishLine;
    [SerializeField] private CharactorMoving _charactor;
    [SerializeField] private Image _bar;

    private float _totalDistance;
    private float _normalizedDistance;

    private void Start()
    {
        _totalDistance = GetDistance();
    }

    private void Update()
    {
        RenderProgressBar();
    }

    private void RenderProgressBar()
    {
        _normalizedDistance = 1 - (GetDistance() / _totalDistance);
        _bar.fillAmount = _normalizedDistance;
    }

    private float GetDistance()
    {
        return Vector3.Distance(_charactor.transform.position, finishLine.transform.position);
    }
}