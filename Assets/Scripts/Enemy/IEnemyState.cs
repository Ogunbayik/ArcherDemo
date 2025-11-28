public interface IEnemyState
{
    public void EnterState(EnemyBase enemy);
    public void Tick(EnemyBase enemy);
    public void ExitState(EnemyBase enemy);
}
