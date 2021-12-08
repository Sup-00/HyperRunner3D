using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class CharactorInteract : MonoBehaviour
{
    [SerializeField] private InteractUI _interactUI;
    [SerializeField] private UnityEvent _onHit;
    [SerializeField] private UnityEvent _onPay;

    private CharactorCoins _charactorCoins;
    private CharactorMoving _charactorMoving;
    private Enemy _currentEnemy;
    private CinemachineVirtualCamera _currentCamera;
    private bool _isTimerActive = false;
    private float _payTimer = 1.5f;
    private float _getHitTimer = 1f;
    private float _currentTimer;

    private void Update()
    {
        if (_isTimerActive == true)
        {
            if (_currentTimer > 0)
            {
                _currentTimer -= 1 * Time.deltaTime;
            }
            else
            {
                _charactorMoving.ContinueMoving();
                _isTimerActive = false;
            }
        }
    }

    private void Start()
    {
        _charactorCoins = GetComponent<CharactorCoins>();
        _charactorMoving = GetComponent<CharactorMoving>();
    }

    private void HideInteractUI()
    {
        _interactUI.gameObject.SetActive(false);
    }

    private void ShowInteractUI(StopPoint point)
    {
        _charactorMoving.SetInteractPoint(point.transform.position, _currentCamera);
        _interactUI.gameObject.SetActive(true);
    }

    private void ActivateTimer(float timer)
    {
        _currentTimer = timer;
        _isTimerActive = true;
    }

    public void Interact(StopPoint stopPoint, Enemy enemy,CinemachineVirtualCamera camera)
    {
        _currentEnemy = enemy;
        _currentCamera = camera;
        ShowInteractUI(stopPoint);
    }

    public void Pay(int coinsCount)
    {
        if (_charactorCoins.SubtractrCoins(coinsCount))
        {
            HideInteractUI();
            _onPay?.Invoke();
            _currentEnemy.OnPay();

            ActivateTimer(_payTimer);
        }
    }

    public void GetHit()
    {
        HideInteractUI();
        _onHit?.Invoke();
        _currentEnemy.OnHit();

        ActivateTimer(_getHitTimer);
    }
}