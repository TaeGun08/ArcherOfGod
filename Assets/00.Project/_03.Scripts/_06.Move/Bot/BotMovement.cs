using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotMovement : MovementBase
{
    private ArrowScanner arrowScanner;

    public override void Initialize(IContextBase context)
    {
        base.Initialize(context);
        arrowScanner = FindAnyObjectByType<ArrowScanner>();
    }

    public override void Movement(float moveSpeed)
    {
        arrowScanner.Danger();
        Vector2 direction = arrowScanner.Direction;
        Vector2 move = direction * (moveSpeed * Time.deltaTime);

        rigidbody2D.MovePosition(rigidbody2D.position + move);

        if (direction.x == 0) return;
        Vector3 scale = rigidbody2D.transform.localScale;
        scale.x = direction.x >= 0 ? -1 : 1;
        rigidbody2D.transform.localScale = scale;
    }
}
