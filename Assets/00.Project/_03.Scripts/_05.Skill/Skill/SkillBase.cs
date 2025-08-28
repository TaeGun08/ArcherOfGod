using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillBase : MonoBehaviour
{
    protected Animator animator;
    protected Rigidbody2D rigidbody2D;
    public Rigidbody2D TargetRigidBody2D { get; set; }

    private Queue<GameObject> pools = new Queue<GameObject>();
    protected Coroutine skillCroutine;
    
    public virtual void Initialize(IContextBase context)
    {
        animator = context.Animator;
        rigidbody2D = context.RigidBody2D;
    }
    
    protected GameObject PoolObject(GameObject prefab)
    {
        GameObject pool = null;

        if (pools.Count > 0)
        {
            for (int i = 0; i < pools.Count; i++)
            {
                pool = pools.Dequeue();
                if (pool.activeSelf == false)
                {
                    pools.Enqueue(pool);
                    return pool;
                }

                pools.Enqueue(pool);
            }
        }

        pool = Instantiate(prefab, transform.position, Quaternion.Euler(0, 0, -180f), GameManager.Instance.transform);
        pools.Enqueue(pool);
        return pool;
    }
    
    public abstract void SkillEntry();
    public abstract void SkillEnd();
}
