using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : ImpactBase
{
    [Header("Whirlwind Settings")]
    [SerializeField] private float pullRadius = 3f;       // 끌어당기는 범위
    [SerializeField] private float swirlSpeed = 180f;     // 회전 속도
    [SerializeField] private float pullStrength = 0.5f;   // 중심으로 끌어당기는 힘
    [SerializeField] private float randomness = 0.1f;     // 회전 불규칙성 (조금 줄임)

    private List<Transform> swirlingArrows = new List<Transform>();
    private Dictionary<Transform, float> arrowDistances = new Dictionary<Transform, float>();

    protected override void OnImpact()
    {
        // 범위 내 화살 감지
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, pullRadius);
        foreach (Collider2D hit in hits)
        {
            Arrow arrow = hit.GetComponent<Arrow>();
            if (arrow != null && !swirlingArrows.Contains(hit.transform))
            {
                swirlingArrows.Add(hit.transform);
                arrow.StopArrowCoroutine(); // 안전하게 코루틴 종료
                arrowDistances[arrow.transform] = Vector3.Distance(transform.position, arrow.transform.position);
            }
        }

        // 회오리 이동 처리
        for (int i = 0; i < swirlingArrows.Count; i++)
        {
            Transform arrow = swirlingArrows[i];

            // 이미 제거된 화살이면 리스트에서 삭제
            if (arrow == null)
            {
                swirlingArrows.RemoveAt(i);
                arrowDistances.Remove(arrow);
                i--;
                continue;
            }

            float distance = arrowDistances[arrow];
            Vector3 centerDir = transform.position - arrow.position;

            // 중심 기준 접선 방향
            Vector3 tangent = new Vector3(-centerDir.y, centerDir.x, 0).normalized;

            // 일정 속도 회전 + 살짝 랜덤
            float swirlThisFrame = swirlSpeed * Time.deltaTime * (1f + Random.Range(-randomness, randomness));
            Vector3 randomOffset = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0) * randomness;

            // 위치 업데이트 (회전 + 중심 끌기 + 불규칙 움직임)
            arrow.position += tangent * swirlThisFrame;
            arrow.position += centerDir.normalized * pullStrength * Time.deltaTime;
            arrow.position += randomOffset * Time.deltaTime;

            // 거리 업데이트
            arrowDistances[arrow] = Mathf.Max(0.1f, distance - pullStrength * Time.deltaTime);
        }
    }
}
