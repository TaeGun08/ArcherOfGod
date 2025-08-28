using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotDeadState : BotStateBase
{
    private static readonly int Dead = Animator.StringToHash("Dead");

    public override void StateEnter()
    {
        Animator.SetTrigger(Dead);
        Rigidbody2D.velocity = Vector2.zero;
    }

    public override void StateExit()
    {
    }
}
