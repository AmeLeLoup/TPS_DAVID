using UnityEngine;

public class RifleIdleState : AimingBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.anim.SetBool("Aiming", false);
    }

    public override void UpdateState(AimStateManager aim)
    {
        
    }
}
