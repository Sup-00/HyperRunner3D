using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _camera;
    [SerializeField] private StopPoint _stopPoint;
    [SerializeField] private UnityEvent _onPay;
    [SerializeField] private UnityEvent _onHit;

    private Animator _animator;
    private float _timer = 1f;
    private bool _isTimerActive = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isTimerActive == true)
        {
            if (_timer > 0)
            {
                _timer -= 1 * Time.deltaTime;
            }
            else
            {
                _animator.enabled = false;
                _isTimerActive = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<CharactorInteract>())
        {
            other.GetComponent<CharactorInteract>().Interact(_stopPoint, this, _camera);
        }
    }

    public void OnPay()
    {
        _onPay?.Invoke();
    }

    public void OnHit()
    {
        _onHit?.Invoke();
        _isTimerActive = true;
    }
}