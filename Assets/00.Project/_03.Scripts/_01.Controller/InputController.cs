using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : SingletonBehavior<InputController>
{
    public Vector2 Direction { get; set; }
    public int SkillCount { get; set; }
    public bool UseSkill { get; set; }

    private void Update()
    {
        Direction = new Vector2(InputHorizontal(), InputVertical());
        InputSelectSkill();
    }

    private float InputHorizontal()
    {
        return Input.GetAxis("Horizontal");
    }

    private float InputVertical()
    {
        return Input.GetAxis("Vertical");
    }

    private void InputSelectSkill()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) SkillCount = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2)) SkillCount = 2;
        else if (Input.GetKeyDown(KeyCode.Alpha3)) SkillCount = 3;
        else if (Input.GetKeyDown(KeyCode.Alpha4)) SkillCount = 4;
        else if (Input.GetKeyDown(KeyCode.Alpha5)) SkillCount = 5;

        if (SkillCount == 0 || UseSkill) return;
        GameManager.Instance.Player.PlayerController.ChangeState<PlayerSkillState>();
        UseSkill = true;
    }
}
