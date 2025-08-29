using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowRain : SkillBase
{
    [Header("Skill Settings")]
    [SerializeField] private Arrow arrowPrefab;       
    [SerializeField] private int arrowCount;     
    [SerializeField] private float interval;   
    [SerializeField] private float spawnHeight; 
    [SerializeField] private float spreadX;      
    [SerializeField] private float duration;   
    
    protected override IEnumerator SkillCoroutine()
    {
        Vector2 targetCenter = TargetRigidBody2D != null
            ? TargetRigidBody2D.position
            : (Vector2)transform.position + Vector2.right * 5f;
        
        animator.SetTrigger(Attack);
        for (int i = 0; i < arrowCount; i++)
        {
            Arrow rainArrow = PoolObject(arrowPrefab.gameObject).GetComponent<Arrow>();
            rainArrow.gameObject.SetActive(true);
            
            Vector2 start = targetCenter + new Vector2(Random.Range(-spreadX, spreadX), spawnHeight);
            
            Vector2 end = targetCenter + new Vector2(Random.Range(-spreadX, spreadX), 0f);
            
            Vector2 control = (start + end) / 2 + Vector2.up * 2f;
            
            rainArrow.Duration = duration;
            rainArrow.TargetPlayerOrBot = rigidbody2D.gameObject.layer.Equals(LayerMask.NameToLayer("Bot"));
            rainArrow.ShotArrow(start, control, end);

            yield return new WaitForSeconds(interval);
        }

        yield return new WaitForSeconds(0.5f);
        if (rigidbody2D.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.Player.PlayerController.ChangeState<PlayerAttackState>();
        }
        else
        {
            GameManager.Instance.Bot.BotController.ChangeState<BotAttackState>();
        }
    }
}
