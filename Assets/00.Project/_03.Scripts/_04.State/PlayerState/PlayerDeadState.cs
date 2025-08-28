using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerStateBase
{
    private static readonly int Dead = Animator.StringToHash("Dead");

    public override void StateEnter()
    {
        Animator.SetTrigger(Dead);
    }

    public override void StateExit()
    {
        
    }
}
