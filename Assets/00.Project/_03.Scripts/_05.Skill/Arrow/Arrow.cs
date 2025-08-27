using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, ITarget
{
    [field: Header("Arrow")]
    [field: SerializeField] public bool TargetPlayerOrBot { get; set; }

    [SerializeField] private float damage;
    [SerializeField] private float duration = 2f;
    private float elapsedTime = 0f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageAble damageable;
        switch (TargetPlayerOrBot)
        {
            case true:
                if (collision.gameObject.layer == LayerMask.NameToLayer("Bot")) return;

                damageable = collision.GetComponent<IDamageAble>();
                if (damageable != null) damageable.TakeDamage(damage);
                break;
            case false:
                if (collision.gameObject.layer == LayerMask.NameToLayer("Player")) return;

                damageable = collision.GetComponent<IDamageAble>();
                if (damageable != null) damageable.TakeDamage(damage);
                break;
        }
    }

    public IEnumerator ShotArrow(Vector2 p0, Vector2 p1, Vector2 p2)
    {
        Vector2 previousPos = p0;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            Vector2 pos = BezierCurve.Quadratic(p0, p1, p2, t);
            transform.position = pos;
            
            Vector2 direction = pos - previousPos;
            if (direction != Vector2.zero)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle);
            }

            previousPos = pos; 
            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForSeconds(4f);
        
        gameObject.SetActive(false);
    }
}