using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBase : MonoBehaviour
{
    protected NavMeshAgent _agent;

    protected PlayerStateController _player;

    [Header("EnemySO")]
    [SerializeField] protected EnemySO _enemySO;
    public EnemySO EnemySO => _enemySO;

    protected bool isAttacking = false;
    protected float animationTime = 2f;

    protected IEnemyState _currentState;

    public EnemyIdleState IdleState { get; protected set; }
    public EnemyWanderState WanderState { get; protected set; }

    public void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindAnyObjectByType<PlayerStateController>();
    }
    private void Start()
    {
        InitializeStates();

        _currentState.EnterState(this);
    }

    private void InitializeStates()
    {
        IdleState = new EnemyIdleState();
        WanderState = new EnemyWanderState();

        _currentState = IdleState;
        _agent.speed = _enemySO.MovementSpeed;
    }
    protected void SwitchState(IEnemyState newState)
    {
        if (_currentState == newState)
            return;

        _currentState.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }
    
}
