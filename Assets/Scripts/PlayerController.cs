using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    private CharacterController _characterController;

    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    [Header("Visual Settings")]
    [SerializeField] private GameObject _playerVisual;

    private float _horizontalInput;
    private float _verticalInput;

    private Vector3 _movementDirection;
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
    }
    void Update()
    {
        HandleMovement();
        HandleRotation();
    }
    private void HandleMovement()
    {
        _horizontalInput = Input.GetAxis(GameConstant.PlayerInput.HORIZONTAL_INPUT);
        _verticalInput = Input.GetAxis(GameConstant.PlayerInput.VERTICAL_INPUT);

        _movementDirection.Set(_horizontalInput, 0f, _verticalInput);
        _characterController.SimpleMove(_movementDirection * _movementSpeed);
    }
    private void HandleRotation()
    {
        if (IsMoving())
        {
            Quaternion targetRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);
            _playerVisual.transform.rotation = Quaternion.Slerp(_playerVisual.transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }
    }
    private bool IsMoving()
    {
        return _movementDirection != Vector3.zero;
    }
}
