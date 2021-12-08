using Cinemachine;
using UnityEngine;

public class CameraMoving : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _mainCamera;
    
    private CinemachineVirtualCamera _currentInteractCamera;

    public void ActivateMainCamera()
    {
        if (_currentInteractCamera != null)
            _currentInteractCamera.Priority = 1;
        _mainCamera.Priority = 2;
    }

    public void OnInteractPoint(CinemachineVirtualCamera camera)
    {
        _currentInteractCamera = camera;
        _mainCamera.Priority = 1;
        _currentInteractCamera.Priority = 2;
    }
}