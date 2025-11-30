using TMPro;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    protected NavMeshAgent _agent;
    protected PlayerStateController _player;

    [Header("EnemySO")]
    [SerializeField] protected EnemySO _currentEnemySO;
    [SerializeField] protected AttackStrategySO _currentAttackSO;
    [SerializeField] protected Transform _attackTransform;
    public EnemySO EnemySO => _currentEnemySO;
    public AttackStrategySO CurrentAttackSO => _currentAttackSO;
    public NavMeshAgent Agent => _agent;

    public TextMeshProUGUI stateText;

    private Vector3 _initialPosition;

    protected IEnemyState _currentState;

    public EnemyIdleState IdleState { get; protected set; }
    public EnemyWanderState WanderState { get; protected set; }
    public EnemyChaseState ChaseState { get; protected set; }
    public EnemyAttackState AttackState { get; protected set; }
    public IEnemyState CurrentState => _currentState;

    
    public Vector3 InitialPosition => _initialPosition;
    public PlayerStateController TargetPlayer => _player;
    public Transform AttackTransform => _attackTransform;

    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindAnyObjectByType<PlayerStateController>();

        InitializeStates();
        SetupEnemy();
    }
    private void InitializeStates()
    {
        IdleState = new EnemyIdleState();
        WanderState = new EnemyWanderState();
        ChaseState = new EnemyChaseState();
        AttackState = new EnemyAttackState();

        _currentState = IdleState;
    }
    private void SetupEnemy()
    {
        _agent.speed = _currentEnemySO.WanderSpeed;
        _initialPosition = transform.position;
    }
    private void Start()
    {
        _currentState.EnterState(this);
    }
    private void Update()
    {
        _currentState.Tick(this);
    }
    public void SwitchState(IEnemyState newState)
    {
        if (_currentState == newState)
            return;

        _currentState.ExitState(this);
        _currentState = newState;
        _currentState.EnterState(this);
    }
    public void SetStateName(string stateName, Color color)
    {
        stateText.text = stateName;
        stateText.color = color;
    }

    public float GetDistanceBetweenPlayer()
    {
        var distance = Vector3.Distance(transform.position, TargetPlayer.transform.position);
        return distance;
    }
    public void SetMovement(Vector3 targetPos, float speed)
    {
        _agent.SetDestination(targetPos);
        _agent.speed = speed;
    }
}
