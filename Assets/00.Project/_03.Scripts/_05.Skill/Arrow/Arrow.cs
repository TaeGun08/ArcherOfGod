using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour, ITarget
{
    [field: Header("Arrow")]
    [field: SerializeField] public bool TargetPlayerOrBot { get; set; }
    [SerializeField] private float damage;
    [field: SerializeField] public float Duration { get; set; }
    private float elapsedTime = 0f;

    private Coroutine coroutine;

    private Collider2D collider2D;

    private void Awake()
    {
        collider2D = GetComponent<Collider2D>();
    }

    private void OnDisable()
    {
        elapsedTime = 0f;
        collider2D.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        IDamageAble damageable;
        switch (TargetPlayerOrBot)
        {
            case true:
                if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
                {
                    damageable = other.GetComponent<IDamageAble>();
                    DamageAble(damageable);
                }

                break;
            case false:
                if (other.gameObject.layer == LayerMask.NameToLayer("Bot"))
                {
                    damageable = other.GetComponent<IDamageAble>();
                    DamageAble(damageable);
                }

                break;
        }
    }

    private void DamageAble(IDamageAble damageable)
    {
        if (damageable == null) return;
        damageable.TakeDamage(damage);
        gameObject.SetActive(false);
        StopCoroutine(coroutine);
        coroutine = null;
    }

    public void ShotArrow(Vector2 p0, Vector2 p1, Vector2 p2)
    {
        coroutine = StartCoroutine(ShotArrowCoroutine(p0, p1, p2));
    }

    private IEnumerator ShotArrowCoroutine(Vector2 p0, Vector2 p1, Vector2 p2)
    {
        Vector2 previousPos = p0;
        p2.y = -1.55f;
        elapsedTime = 0f;

        while (elapsedTime < Duration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / Duration;

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

        collider2D.enabled = false;
    
        yield return new WaitForSeconds(4f);
    
        gameObject.SetActive(false);
    }

}