using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerStateBase
{
    private AttackBase attackBase;
    
    public override void Initialize(PlayerContext context)
    {
        base.Initialize(context);
        attackBase = GetComponent<AttackBase>();
        attackBase.Initialize(context);
    }
    
    public override void StateEnter()
    {
        attackBase.TargetRigidBody2D = GameManager.Instance.Bot.GetComponent<Rigidbody2D>();
        attackBase.AttackEntry(Stat.ShotSpeed);
        Vector3 scale = Rigidbody2D.transform.localScale;
        scale.x = -1f;
        Rigidbody2D.transform.localScale = scale;
    }

    private void Update()
    {
        if (InputController.Instance.Direction.magnitude > 0.01f)
        {
            PlayerController.ChangeState<PlayerWalkState>();
        }
    }

    public override void StateExit()
    {
        attackBase.AttackEnd();
    }
}