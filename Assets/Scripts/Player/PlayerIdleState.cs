
using UnityEngine;

public class PlayerIdleState : IPlayerState
{
    private float horizontalInput;
    private float verticalInput;
    public void EnterState(PlayerStateController player)
    {

    }
    public void Tick(PlayerStateController player)
    {
        horizontalInput = Input.GetAxis(GameConstant.PlayerInput.HORIZONTAL_INPUT);
        verticalInput = Input.GetAxis(GameConstant.PlayerInput.VERTICAL_INPUT);

        if (Mathf.Abs(horizontalInput) > 0.1f || Mathf.Abs(verticalInput) > 0.1f)
            player.SwitchState(player.MoveState);
    }
    public void ExitState(PlayerStateController player)
    {

    }

}
