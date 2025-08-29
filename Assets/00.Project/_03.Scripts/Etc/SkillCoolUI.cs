using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillCoolUI : MonoBehaviour
{
    [Header("SkillUISettings")] 
    [SerializeField] private PlayerSkillState skillState;
    [SerializeField] private int skillIndex;
    [SerializeField] private Image skillCool;
    [SerializeField] private TMP_Text skillCoolText;

    private void LateUpdate()
    {
        switch (skillState.SkillBases[skillIndex].CoolTimer <= 0)
        {
            case true:
                if (skillCoolText.gameObject.activeSelf)
                {
                    skillCoolText.gameObject.SetActive(false);
                }
                
                if (skillCool.gameObject.activeSelf)
                {
                    skillCool.gameObject.SetActive(false);
                }
                
                break;
            case false:
                
                if (skillCoolText.gameObject.activeSelf == false)
                {
                    skillCoolText.gameObject.SetActive(true);
                }
                
                if (skillCool.gameObject.activeSelf == false)
                {
                    skillCool.gameObject.SetActive(true);
                }
                break;
        }

        skillCool.fillAmount = skillState.SkillBases[skillIndex].CoolTimer / skillState.SkillBases[skillIndex].CoolTime;
        
        skillCoolText.text = skillState.SkillBases[skillIndex].CoolTimer.ToString("F0");
    }
}