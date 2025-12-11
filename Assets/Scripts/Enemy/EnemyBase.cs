using TMPro;
using UnityEngine;
using UnityEngine.AI;

public abstract class EnemyBase : MonoBehaviour
{
    protected NavMeshAgent _agent;
    protected PlayerStateController _player;
    protected IEnemyState _currentState;
    protected EnemyAnimationController _animationController;

    [Header("Enemy Settings")]
    [SerializeField] protected BaseEnemyDataSO _enemyDataSO;
    [Header("Test Parts")]
    public TextMeshProUGUI stateText;


    public EnemyIdleState IdleState { get; protected set; }
    public EnemyWanderState WanderState { get; protected set; }
    public EnemyChaseState ChaseState { get; protected set; }
    public EnemyAttackState AttackState { get; protected set; }
    public EnemySuicideState SuicideState { get; protected set; }
    public EnemyAttackWaitState AttackWaitState { get; protected set; }

    
    public IEnemyState CurrentState => _currentState;
    public BaseEnemyDataSO EnemyData => _enemyDataSO;
    public NavMeshAgent Agent => _agent;
    public Vector3 InitialPosition => _initialPosition;
    public PlayerStateController Player => _player;
    public EnemyAnimationController AnimationController => _animationController;


    private Vector3 _initialPosition;

    private float _currentAttackCooldown;
    protected virtual void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _player = GameObject.FindAnyObjectByType<PlayerStateController>();
        _animationController = GetComponent<EnemyAnimationController>();

        InitializeStates();
        SetupEnemy();
    }
    private void InitializeStates()
    {
        IdleState = new EnemyIdleState();
        WanderState = new EnemyWanderState();
        ChaseState = new EnemyChaseState();
        AttackState = new EnemyAttackState();
        SuicideState = new EnemySuicideState();
        AttackWaitState = new EnemyAttackWaitState();

        _currentState = IdleState;
    }
    private void SetupEnemy()
    {
        _agent.speed = _enemyDataSO.WanderSpeed;
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
    public void TestSetStateText(string stateName, Color color)
    {
        stateText.text = stateName;
        stateText.color = color;
    }
    protected float GetDistanceBetweenPlayer()
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
    private void DecreaseAttackCooldown()
    {
        if (CanAttack())
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
    public bool TargetInChaseDistance()
    {
        return GetDistanceBetweenPlayer() <= _enemyDataSO.ChaseDistance;
    }
    public void StoppedCharacter()
    {
        _agent.isStopped = true;
        _agent.ResetPath();
    }
    public abstract bool TargetInAttackRange();
    public abstract Transform GetAttackTransform();
    public abstract AttackStrategySO AttackStrategy();
    public abstract float GetSuicideTime();
    public abstract void OnChaseStart();

}
