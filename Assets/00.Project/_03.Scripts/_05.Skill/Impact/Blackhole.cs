using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blackhole : ImpactBase
{
    [SerializeField] private float pullRadius = 4f;
    [SerializeField] private float pullForce = 5f;
    [SerializeField] private float pullDuration = 2f;

    private float pullTimer;

    private void OnEnable()
    {
        pullTimer = 0f;
        Vector3 pos = transform.position;
        pos.y = -1.55f;
        transform.position = pos;
    }

    protected override void OnImpact()
    {
        pullTimer += Time.deltaTime;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, pullRadius);

        foreach (var col in colliders)
        {
            Rigidbody2D rb = col.attachedRigidbody;
            if (rb == null) continue;

            Vector2 dir = ((Vector2)transform.position - rb.position).normalized;
            float pullStep = pullForce * Time.deltaTime;
            Vector2 newPos = rb.position + dir * pullStep;
            rb.MovePosition(newPos);
        }

        if (pullTimer >= pullDuration)
        {
            pullTimer = 0f;
            gameObject.SetActive(false);
        }
    }
}