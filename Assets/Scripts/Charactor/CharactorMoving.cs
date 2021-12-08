using System.Collections.Generic;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(CharactorCoins), typeof(CharactorInteract))]
public class CharactorMoving : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private int _linesCount = 3;
    [SerializeField] private CameraMoving _cameraMoving;
    [SerializeField] private UnityEvent _onInteractPoint;
    [SerializeField] private UnityEvent _onFinishLine;

    private Dictionary<int, float> _positions;
    private int _currentLine = 2;
    private float _targetPosition = 0.5f;
    private bool _isMoving = true;
    private bool _isRichedPoint = false;
    private Vector3 _interactPoint;
    private CinemachineVirtualCamera _currentCamera;

    private void Start()
    {
        _positions = new Dictionary<int, float>()
        {
            {1, -0.5f},
            {2, 0.5f},
            {3, 1.5f}
        };
    }

    private void Update()
    {
        if (_isMoving == true)
        {
            Move();

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                ChangeLine(-1);
            }

            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                ChangeLine(1);
            }
        }
        else
        {
            MoveToInteractPoint(_interactPoint);
        }
    }

    private void ChangeLine(int direction)
    {
        if (_currentLine != 1 && direction < 0 || _currentLine != _linesCount && direction > 0)
        {
            _currentLine += direction;
            _targetPosition = _positions[_currentLine];
        }
    }

    private void Move()
    {
        transform.position += Vector3.forward * _moveSpeed * Time.deltaTime;

        if (Vector3.Distance(transform.position,
            new Vector3(_targetPosition, transform.position.y, transform.position.z)) >= 0)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(_targetPosition, transform.position.y, transform.position.z), _moveSpeed * Time.deltaTime);
        }
    }

    private void MoveToInteractPoint(Vector3 interacPoint)
    {
        if (_isRichedPoint == false)
        {
            if (Vector3.Distance(transform.position, interacPoint) > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                    interacPoint, _moveSpeed * Time.deltaTime);
            }
            else
            {
                _cameraMoving.OnInteractPoint(_currentCamera);
                _onInteractPoint?.Invoke();
                _isRichedPoint = true;
            }
        }
    }

    public void SetInteractPoint(Vector3 interactPoint, CinemachineVirtualCamera camera)
    {
        _isMoving = false;
        _currentCamera = camera;
        _isRichedPoint = false;
        _targetPosition = _positions[2];
        _interactPoint = interactPoint;
    }

    public void ContinueMoving()
    {
        _cameraMoving.ActivateMainCamera();
        _isMoving = true;
    }

    public void OnFinishLine()
    {
        _isMoving = false;
        transform.DORotate(new Vector3(0, 180f, 0), 1f);
        _onFinishLine?.Invoke();
    }
}