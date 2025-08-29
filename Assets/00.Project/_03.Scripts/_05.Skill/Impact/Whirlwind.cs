using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : ImpactBase
{
    [Header("Whirlwind Settings")]
    [SerializeField] private float pullRadius = 3f;
    [SerializeField] private float swirlSpeed = 180f;
    [SerializeField] private float pullStrength = 0.5f;
    [SerializeField] private float randomness = 0.1f;

    [Header("Duration & Target")]
    [SerializeField] private float whirlDuration = 2f;

    private readonly List<Arrow> arrows = new();
    private float timer;
    private bool released;

    public bool WhirlwindTarget { get; set; }

    private void OnEnable()
    {
        timer = 0f;
        released = false;
        arrows.Clear();
    }

    protected override void OnImpact()
    {
        timer += Time.deltaTime;

        switch (released)
        {
            case false when timer < whirlDuration:
            {
                foreach (var hit in Physics2D.OverlapCircleAll(transform.position, pullRadius))
                {
                    Arrow arrow = hit.GetComponent<Arrow>();
                    if (arrow == null || arrows.Contains(arrow)) continue;
                    arrows.Add(arrow);
                    arrow.StopArrowCoroutine();
                }

                foreach (var arrow in arrows)
                {
                    if (arrow == null) continue;

                    Vector3 dir = transform.position - arrow.transform.position;
                    Vector3 tangent = new Vector3(-dir.y, dir.x, 0).normalized;

                    arrow.transform.position += tangent * (swirlSpeed * Time.deltaTime);
                    arrow.transform.position += dir.normalized * (pullStrength * Time.deltaTime);
                    arrow.transform.position += (Vector3)Random.insideUnitCircle * (randomness * Time.deltaTime);
                }

                break;
            }
            case false:
            {
                released = true;
                foreach (var arrow in arrows)
                {
                    if (arrow == null) continue;

                    arrow.TargetPlayerOrBot = !WhirlwindTarget;
                    Vector2 p0 = arrow.transform.position;
                    Vector2 p1 = Vector2.up * 8f;
                    Vector2 target = WhirlwindTarget
                        ? GameManager.Instance.Bot.transform.position
                        : GameManager.Instance.Player.transform.position;
                    Vector2 p2 = target + new Vector2(Random.Range(-3f, 3f), 0f);

                    arrow.ShotArrow(p0, p1, p2);
                }
                gameObject.SetActive(false);
                break;
            }
        }
    }
}
