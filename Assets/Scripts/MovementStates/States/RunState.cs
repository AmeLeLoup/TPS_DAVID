using UnityEngine;

public class RunState : MovementBaseState
{
    public override void EnterState(MovementStateManager movement)
    {
        movement.anim.SetBool("Running", true);
    }

    public override void UpdateState(MovementStateManager movement)
    {
        if (Input.GetKeyUp(KeyCode.LeftShift)) ExitState(movement, movement.Walk);
        else if (movement.direction.magnitude < 0.1f) ExitState(movement,movement.Idle);
        
        if (movement.verticalInput < 0) movement.currentMoveSpeed = movement.runbackSpeed;
        else movement.currentMoveSpeed = movement.runSpeed;
    }
    
    void ExitState(MovementStateManager movement, MovementBaseState state)
    {
        movement.anim.SetBool("Running", false);
        movement.SwitchState(state);
    }
}
