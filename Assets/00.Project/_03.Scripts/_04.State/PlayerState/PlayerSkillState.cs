using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerStateBase
{
    private SkillBase[] skillBases;
    private SkillBase skill;
    
    public override void Initialize(PlayerContext context)
    {
        base.Initialize(context);
        skillBases = GetComponentsInChildren<SkillBase>();

        foreach (var skillBase in skillBases)
        {
            skillBase.Initialize(context);
        }
    }
    
    public override void StateEnter()
    {
        skill = skillBases[InputController.Instance.SkillCount - 1];
        skill.TargetRigidBody2D = GameManager.Instance.Bot.GetComponent<Rigidbody2D>();
        skill.SkillEntry();
    }

    public override void StateExit()
    {
        skill.SkillEnd();
    }
}
