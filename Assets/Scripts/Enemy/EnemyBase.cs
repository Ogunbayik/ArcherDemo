using TMPro;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    protected NavMeshAgent _agent;
    protected PlayerStateController _player;

    [Header("Enemy Settings")]
    [SerializeField] protected EnemySO _enemySO;
    [Header("Attack Settings")]
    [SerializeField] protected AttackStrategySO _attackStrategy;
    [SerializeField] protected Transform _attackTransform;
    public EnemySO EnemySO => _enemySO;
    public AttackStrategySO CurrentAttackSO => _attackStrategy;
    public NavMeshAgent Agent => _agent;

    [Header("Test Parts")]
    public TextMeshProUGUI stateText;

    private Vector3 _initialPosition;

    protected IEnemyState _currentState;
    public IEnemyState CurrentState => _currentState;


    public EnemyIdleState IdleState { get; protected set; }
    public EnemyWanderState WanderState { get; protected set; }
    public EnemyChaseState ChaseState { get; protected set; }
    public EnemyAttackState AttackState { get; protected set; }

    
    public Vector3 InitialPosition => _initialPosition;
    public PlayerStateController Player => _player;
    public Transform AttackTransform => _attackTransform;

    private float _currentAttackCooldown;
    private bool canAttack;

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
        _agent.speed = _enemySO.WanderSpeed;
        _initialPosition = transform.position;
    }
    private void Start()
    {
        _currentState.EnterState(this);
    }
    private void Update()
    {
        DecreaseAttackCooldown();

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
    public bool TargetInChaseDistance()
    {
        return GetDistanceBetweenPlayer() <= _enemySO.ChaseDistance;
    }
    public bool TargetInAttackRange()
    {
        return GetDistanceBetweenPlayer() <= _enemySO.AttackDistance;
    }
    public float GetDistanceBetweenPlayer()
    {
        var distance = Vector3.Distance(transform.position, Player.transform.position);
        return distance;
    }
    public void SetMovement(Vector3 targetPos, float speed)
    {
        _agent.SetDestination(targetPos);
        _agent.speed = speed;
    }
    public void SetCurrentCooldown(float value)
    {
        _currentAttackCooldown = value;
    }
    public void DecreaseAttackCooldown()
    {
        if (canAttack)
            return;

        if (_currentAttackCooldown > 0)
            _currentAttackCooldown -= Time.deltaTime;
        else
            _currentAttackCooldown = 0;
    }
    public bool CanAttack()
    {
        return _currentAttackCooldown == 0;
    }
}
