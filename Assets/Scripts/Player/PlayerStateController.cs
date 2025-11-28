using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
    public CharacterController characterController {  get; private set; }

    [Header("Speed Settings")]
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _rotationSpeed;
    [Header("Visual Settings")]
    [SerializeField] private GameObject _playerVisual;

    public PlayerIdleState IdleState { get; private set; }
    public PlayerMoveState MoveState { get; private set; }

    private IPlayerState currentState;

    public float MovementSpeed => _movementSpeed;
    public float RotationSpeed => _rotationSpeed;
    public GameObject PlayerVisual => _playerVisual;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();

        IdleState = new PlayerIdleState();
        MoveState = new PlayerMoveState();

        currentState = IdleState;
    }
    private void Start()
    {
        currentState.EnterState(this);
    }
    private void Update()
    {
        currentState.Tick(this);
    }
    public void SwitchState(IPlayerState newState)
    {
        if (currentState == newState)
            return;

        currentState.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }
}
