using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : ImpactBase
{
    private float timer = 1f;

    [SerializeField] private Collider2D collider2D;

    private void OnEnable()
    {
        collider2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        int layer = other.gameObject.layer;
        if (layer == LayerMask.NameToLayer("Player") || layer == LayerMask.NameToLayer("Bot"))
        {
            var damageAble = other.GetComponent<IDamageAble>();
            if (damageAble == null) return;
            damageAble.TakeDamage(damage);
            collider2D.enabled = false;
        }
    }
    
    protected override void OnImpact()
    {
        timer -= Time.deltaTime;

        if (timer > 0) return;
        collider2D.enabled = false;
        timer = 1f;
    }
}
