using UnityEngine;

public class PlayerMoveState : IPlayerState
{
    private float _horizontalInput;
    private float _verticalInput;

    private Vector3 _movementDirection;

    public void EnterState(PlayerStateController player)
    {

    }

    public void Tick(PlayerStateController player)
    {
        HandleMovement(player);
        HandleRotation(player);
    }

    public void ExitState(PlayerStateController player)
    {

    }
    private void HandleMovement(PlayerStateController player)
    {
        _horizontalInput = Input.GetAxis(GameConstant.PlayerInput.HORIZONTAL_INPUT);
        _verticalInput = Input.GetAxis(GameConstant.PlayerInput.VERTICAL_INPUT);

        _movementDirection.Set(_horizontalInput, 0f, _verticalInput);
        player.characterController.SimpleMove(_movementDirection * player.MovementSpeed);
    }
    private void HandleRotation(PlayerStateController player)
    {
        if (IsMoving())
        {
            Quaternion targetRotation = Quaternion.LookRotation(_movementDirection, Vector3.up);
            player.PlayerVisual.transform.rotation = Quaternion.Slerp(player.PlayerVisual.transform.rotation, targetRotation, player.RotationSpeed * Time.deltaTime);
        }
    }
    private bool IsMoving()
    {
        return _movementDirection != Vector3.zero;
    }
}
