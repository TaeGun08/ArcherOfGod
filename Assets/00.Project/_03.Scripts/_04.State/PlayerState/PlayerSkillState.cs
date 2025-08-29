using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerStateBase
{
    private SkillBase[] skillBases;
    public SkillBase[] SkillBases => skillBases;
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
        skill = skillBases[InputController.Instance.SkillCount];
        skill.TargetRigidBody2D = GameManager.Instance.Bot.GetComponent<Rigidbody2D>();
        Vector3 scale = Rigidbody2D.transform.localScale;
        scale.x = -1f;
        Rigidbody2D.transform.localScale = scale;
        skill.SkillEntry();
    }

    public override void StateExit()
    {
        skill.SkillEnd();
    }
}
