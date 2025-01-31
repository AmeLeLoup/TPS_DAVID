using UnityEngine;

public class AimState : AimingBaseState
{
    public override void EnterState(AimStateManager aim)
    {
        aim.anim.SetBool("Aiming", true);
    }

    public override void UpdateState(AimStateManager aim)
    {
        
    }
}
