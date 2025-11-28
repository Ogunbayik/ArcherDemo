public interface IPlayerState
{
    public void EnterState(PlayerStateController player);
    public void Tick(PlayerStateController player);
    public void ExitState(PlayerStateController player);
}
