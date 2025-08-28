using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementBase
{
    public override void Movement(float moveSpeed)
    {
        Vector2 direction = InputController.Instance.Direction;
        Vector2 move = direction * (moveSpeed * Time.deltaTime);

        rigidbody2D.MovePosition(rigidbody2D.position + move);

        if (direction.x == 0) return;
        Vector3 scale = rigidbody2D.transform.localScale;
        scale.x = direction.x >= 0 ? -1 : 1;
        rigidbody2D.transform.localScale = scale;
    }
}
