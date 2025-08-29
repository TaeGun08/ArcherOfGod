using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCoolTimeManager : SingletonBehavior<SkillCoolTimeManager>
{
    private Dictionary<SkillBase, Coroutine> runningCooldowns = new Dictionary<SkillBase, Coroutine>();
    
    public void StartCooldown(SkillBase skill)
    {
        if (runningCooldowns.ContainsKey(skill))
            return;

        Coroutine cr = StartCoroutine(CooldownCoroutine(skill));
        runningCooldowns.Add(skill, cr);
    }
    
    private IEnumerator CooldownCoroutine(SkillBase skill)
    {
        skill.CanUseSkill = false;
        skill.CoolTimer = skill.CoolTime;

        while (skill.CoolTimer > 0)
        {
            yield return new WaitForSeconds(1f);
            skill.CoolTimer--;
        }

        skill.CanUseSkill = true;
        
        runningCooldowns.Remove(skill);
    }
    
    public void StopCooldown(SkillBase skill)
    {
        if (runningCooldowns.TryGetValue(skill, out var cr))
        {
            StopCoroutine(cr);
            runningCooldowns.Remove(skill);
            skill.CanUseSkill = true;
        }
    }
    
    public void StopAllCooldowns()
    {
        foreach (var cr in runningCooldowns.Values)
        {
            StopCoroutine(cr);
        }
        runningCooldowns.Clear();
    }
}