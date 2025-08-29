using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkState : PlayerStateBase
{
    private static readonly int Walk = Animator.StringToHash("Walk");
    private static readonly int Idle = Animator.StringToHash("Idle");

    private MovementBase movementBase;
    
    private float idleTimer = 0f;
    private float idleDelay = 0.1f;
    
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
        
        if (InputController.Instance.Direction.magnitude < 0.01f)
        {
            idleTimer += Time.fixedDeltaTime;
            if (idleTimer >= idleDelay)
            {
                PlayerController.ChangeState<PlayerAttackState>();
            }
        }
        else
        {
            idleTimer = 0f;
        }
    }
    
    public override void StateExit()
    {
        idleTimer = 0f;
        Animator.SetTrigger(Idle);
    }
}
