using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerStateBase
{
    [Header("AttackSettings")] [SerializeField]
    private float attackDelay;
    [SerializeField] private Arrow arrowPrefab;
    
    private Queue<GameObject> arrows = new Queue<GameObject>();

    private Coroutine attackCroutine;
    
    public override void StateEnter()
    {
        attackCroutine = StartCoroutine(ShotArrow());
    }

    private IEnumerator ShotArrow()
    {
        WaitForSeconds wfs = new WaitForSeconds(attackDelay);
        
        while (true)
        {
            if (arrows.Count > 0)
            {
                arrows.Dequeue();
            }
            else
            {
                Arrow arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);
                Vector2 p0 = PlayerController.transform.position + Vector3.up * 0.5f;
                Vector2 p1 = Vector2.up * 8f;
                Vector2 p2 = GameManager.Instance.Bot.transform.position;
                StartCoroutine(arrow.ShotArrow(p0, p1, p2));
            }
            yield return wfs;
        }
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
        attackCroutine = null;
    }
}
