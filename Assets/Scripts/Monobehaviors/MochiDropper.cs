using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MochiDropper : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] InputAction _touch;
    [SerializeField] InputAction _drag;

    [Header("References")]
    [SerializeField] GameObject _dropMarker;

    Vector3 _cursorScreenPosition = Vector3.zero;
    bool _isDragging = false;

    private void Awake()
    {
        _touch.Enable();
        _drag.Enable();

        _drag.performed += context => { _cursorScreenPosition = context.ReadValue<Vector2>(); };
        _touch.performed += context => { _isDragging = true; };
        _touch.canceled += context => { _isDragging = false; };
    }

    private void Update()
    {
        if (_isDragging)
        {
            // Calculate the new position for the _dropMarker using Lerp
            Vector3 targetPosition = Camera.main.ScreenToWorldPoint(_cursorScreenPosition);
            targetPosition.y = _dropMarker.transform.position.y;
            targetPosition.z = 0; // Assuming z should be 0, adjust as needed

            // Adjust the lerp speed by changing the second parameter (e.g., 0.1f)
            _dropMarker.transform.position = Vector3.Lerp(_dropMarker.transform.position, targetPosition, 0.1f);
        }
    }

    private IEnumerator DragTask()
    {
        yield return null;
    }
}
