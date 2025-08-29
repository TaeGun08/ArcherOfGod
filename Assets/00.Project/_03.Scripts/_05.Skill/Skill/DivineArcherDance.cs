using System.Collections;
using UnityEngine;

public class DivineArcherDance : SkillBase
{
    [Header("SkillSettings")] 
    [SerializeField] private float jumpForce;
    [SerializeField] private float spinSpeed;
    [SerializeField] private float duration;
    [SerializeField] private Arrow arrow;
    [SerializeField] private float shotInterval;

    private int arrowCount = 5;
    
    protected override IEnumerator SkillCoroutine()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, 0f);
        rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        float elapsed = 0f;
        float nextShotTime = 0f;
        int fired = 0;

        animator.SetTrigger(Attack);
        
        while (elapsed < duration)
        {
            float rotation = spinSpeed * Time.deltaTime;
            rigidbody2D.transform.Rotate(0f, 0f, rotation);
            
            if (elapsed >= nextShotTime && fired < arrowCount)
            {
                Arrow(PoolObject(arrow.gameObject).GetComponent<Arrow>());
                fired++;
                nextShotTime = elapsed + shotInterval;
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        
        yield return new WaitForSeconds(0.6f);
        if (rigidbody2D.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.Player.PlayerController.ChangeState<PlayerAttackState>();
        }
        else
        {
            GameManager.Instance.Bot.BotController.ChangeState<BotAttackState>();
        }
    }

    private void Arrow(Arrow arrow)
    {
        arrow.gameObject.SetActive(true);
        Vector2 p0 = transform.position + (Vector3.up * 0.5f);
        Vector2 p1 = Vector2.up * Random.Range(-1f, 1f);
        Vector2 p2 = TargetRigidBody2D.transform.position;
        arrow.Duration = 1.5f;
        arrow.TargetPlayerOrBot = rigidbody2D.gameObject.layer.Equals(LayerMask.NameToLayer("Bot"));
        arrow.ShotArrow(p0, p1, p2);
    }
}
