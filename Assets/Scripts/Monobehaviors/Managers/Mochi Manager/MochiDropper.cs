using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MochiDropper : MonoBehaviour
{
    [Header("Variables")]
    [SerializeField] InputAction _touch;
    [SerializeField] InputAction _drag;
    [Range(0, 1)]
    [SerializeField] float _dropperTransparency;
    [SerializeField] int _mochiMinIndex;
    [SerializeField] int _mochiMaxIndex;

    [Header("Events")]
    [SerializeField] VoidEvent _spawnMochi;

    [Header("References")]
    [SerializeField] GameObject _dropMarker;
    [SerializeField] SpriteRenderer _dropMarkerSprite;
    [SerializeField] ObjectPoolController _objectPoolController;
    [SerializeField] GameSettings _gameSettings;
    [SerializeField] BoolVariable _gameIsPaused;

    GameObject _nextMochi;
    Vector3 _cursorScreenPosition = Vector3.zero;
    bool _isDragging = false;
    bool _inputEnabled = true;

    private void Awake()
    {
        _touch.Enable();
        _drag.Enable();
    }

    private void OnEnable()
    {
        _drag.performed += OnDragPerformed;
        _touch.performed += OnTouchPerformed;
        _touch.canceled += OnTouchCanceled;
    }

    private void OnDisable()
    {
        _drag.performed -= OnDragPerformed;
        _touch.performed -= OnTouchPerformed;
        _touch.canceled -= OnTouchCanceled;
    }

    private void Start()
    {
        _objectPoolController.AddObjectsToPool(40);
        LoadNextMochi();
    }

    private void OnDragPerformed(InputAction.CallbackContext p_context)
    {
        if (!_inputEnabled) return;
        _cursorScreenPosition = p_context.ReadValue<Vector2>();
    }

    private void OnTouchPerformed(InputAction.CallbackContext p_context)
    {
        if (_gameIsPaused.value || !_inputEnabled) return; 
        
        _isDragging = true;
    }

    private void OnTouchCanceled(InputAction.CallbackContext p_context)
    {
        if (_gameIsPaused.value || !_inputEnabled) return;

        _isDragging = false; 
        SpawnMochi();
    }

    public void OnMochiLanded()
    {
        LoadNextMochi();
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
        float minX = mainCamera.transform.position.x - cameraWidth + _nextMochi.transform.localScale.x * 0.6f;
        float maxX = mainCamera.transform.position.x + cameraWidth - _nextMochi.transform.localScale.x * 0.6f;
        float clampedX = Mathf.Clamp(p_x, minX, maxX);
        return clampedX;
    }

    private void SpawnMochi()
    {
        _inputEnabled = false;
        _nextMochi.transform.position = _dropMarkerSprite.transform.position;
        _nextMochi.SetActive(true);
        _dropMarkerSprite.gameObject.SetActive(false);
    }

    private void LoadNextMochi()
    {
        _nextMochi = _objectPoolController.GetObject();
        MochiType mochiType = GetRandomMochiType();
        _nextMochi.GetComponent<Mochi>().ConfigureMochi(mochiType);
        UpdateMochiDropperSprite(mochiType);
        _inputEnabled = true;
    }

    private void UpdateMochiDropperSprite(MochiType p_mochiType)
    {
        Color newColor = p_mochiType.color;
        newColor.a = _dropperTransparency;
        _dropMarkerSprite.color = newColor;
        _dropMarker.transform.localScale = new Vector3(p_mochiType.scale, p_mochiType.scale, 1f);
        _dropMarker.transform.position = new Vector3(0f, _dropMarker.transform.position.y, 1f);
        _dropMarkerSprite.gameObject.SetActive(true);
    }

    private MochiType GetRandomMochiType()
    {
        int randomIndex = Random.Range(_mochiMinIndex, _mochiMaxIndex + 1);
        return (_gameSettings.mochiMatrix[randomIndex]);
    }
}
