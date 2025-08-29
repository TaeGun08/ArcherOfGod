using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : SingletonBehavior<InputController>
{
    public Vector2 Direction { get; set; }
    public int SkillCount { get; set; }
    public bool UseSkill { get; set; }

    [SerializeField] private PlayerSkillState playerSkillState;
    
    private void Update()
    {
        Direction = new Vector2(InputHorizontal(), 0f);
        InputSelectSkill();
    }

    private float InputHorizontal()
    {
        bool left = Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
        bool right = Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);


        if (left && right) return 0f;
        
        if (left) return -1f;
        
        if (right) return 1f;
        
        return 0f;
    }

    private void InputSelectSkill()
    {
        if (GameManager.Instance.GameStarted == false) return;

        if (Input.GetKeyDown(KeyCode.Alpha1)) TryUseSkill(0);
        else if (Input.GetKeyDown(KeyCode.Alpha2)) TryUseSkill(1);
        else if (Input.GetKeyDown(KeyCode.Alpha3)) TryUseSkill(2);
        else if (Input.GetKeyDown(KeyCode.Alpha4)) TryUseSkill(3);
        else if (Input.GetKeyDown(KeyCode.Alpha5)) TryUseSkill(4);
    }

    private void TryUseSkill(int skillNumber)
    {
        var skill = playerSkillState.SkillBases[skillNumber];
        if (skill == null) return;
        if (skill.CanUseSkill == false) return;

        SkillCount = skillNumber;
        if (UseSkill) return;
        GameManager.Instance.Player.PlayerController.ChangeState<PlayerSkillState>();
        UseSkill = true;
    }
}
