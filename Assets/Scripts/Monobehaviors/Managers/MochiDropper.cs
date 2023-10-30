using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MochiDropper : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] InputAction _touch;
    [SerializeField] InputAction _drag;

    [Header("Events")]
    [SerializeField] VoidEvent _spawnMochi;

    [Header("References")]
    [SerializeField] GameObject _dropMarker;
    [SerializeField] ObjectPoolController _objectPoolController;

    Vector3 _cursorScreenPosition = Vector3.zero;
    bool _isDragging = false;

    private void Awake()
    {
        _touch.Enable();
        _drag.Enable();

        _drag.performed += context => { _cursorScreenPosition = context.ReadValue<Vector2>(); };
        _touch.performed += context => { _isDragging = true; };
        _touch.canceled += context => { _isDragging = false; SpawnMochi(); };
    }

    private void Start()
    {
        _objectPoolController.InitializePool(20);
    }

    private void Update()
    {
        if (_isDragging)
        {
            // Calculate the new position for the _dropMarker using Lerp
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(_cursorScreenPosition);


            targetPosition.x = ClampInPlayArea(targetPosition.x);
            targetPosition.y = _dropMarker.transform.position.y;
            targetPosition.z = 0; // Assuming z should be 0, adjust as needed

            // Adjust the lerp speed by changing the second parameter (e.g., 0.1f)
            _dropMarker.transform.position = Vector3.Lerp(_dropMarker.transform.position, targetPosition, 0.1f);
        }
    }

    private float ClampInPlayArea(float p_x)
    {

        Camera mainCamera = Camera.main;
        // Calculate the camera's boundaries
        float cameraWidth = mainCamera.orthographicSize * mainCamera.aspect;
        float minX = mainCamera.transform.position.x - cameraWidth + 0.5f;
        float maxX = mainCamera.transform.position.x + cameraWidth - 0.5f;
        float clampedX = Mathf.Clamp(p_x, minX, maxX);
        return clampedX;
    }

    private void SpawnMochi()
    {
        GameObject mochi = _objectPoolController.GetObject();
        mochi.transform.position = _dropMarker.transform.position;
        mochi.SetActive(true);
    }
}
