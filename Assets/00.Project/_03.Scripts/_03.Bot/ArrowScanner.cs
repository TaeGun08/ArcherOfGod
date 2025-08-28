using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScanner : MonoBehaviour
{
    [Header("ScannerSettings")] [SerializeField]
    private float dangerDistance = 0.5f;

    private CircleCollider2D collider2D;
    public Vector2 Direction { get; set; }

    private void Awake()
    {
        collider2D = GetComponent<CircleCollider2D>();
    }

    public void Danger()
    {
        Collider2D hit =
            Physics2D.OverlapCircle(collider2D.bounds.center, collider2D.radius, LayerMask.GetMask("Arrow"));
        if (hit != null)
        {
            Vector2 dir = hit.transform.position - transform.position;
            dir.y = 0f;
            float distance = dir.magnitude;
            if (dir.x < 0 && distance <= dangerDistance)
            {
                Direction = Vector2.right;
            }
            else
            {
                Direction = Vector2.left;
            }
            
        }
    }
}