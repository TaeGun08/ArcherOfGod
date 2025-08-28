using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerStateBase
{
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Idle = Animator.StringToHash("Idle");

    private MovementBase movementBase;
    
    public override void Initialize(PlayerContext context)
    {
        base.Initialize(context);
        movementBase = GetComponent<MovementBase>();
        movementBase.Initialize(context);
    }
    
    public override void StateEnter()
    {
        Animator.SetTrigger(Walk);
    }

    private void FixedUpdate()
    {
        movementBase.Movement(Stat.MoveSpeed);
        
        if (InputController.Instance.Direction.magnitude == 0.0f)
        {
            PlayerController.ChangeState<PlayerAttackState>();
        }
    }
    
    public override void StateExit()
    {
        Animator.SetTrigger(Idle);
    }
}
