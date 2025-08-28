using System.Collections.Generic;
using UnityEngine;

public class Whirlwind : ImpactBase
{
    [Header("Whirlwind Settings")]
    [SerializeField] private float pullRadius = 3f;       // ������� ����
    [SerializeField] private float swirlSpeed = 180f;     // ȸ�� �ӵ�
    [SerializeField] private float pullStrength = 0.5f;   // �߽����� ������� ��
    [SerializeField] private float randomness = 0.1f;     // ȸ�� �ұ�Ģ�� (���� ����)

    private List<Transform> swirlingArrows = new List<Transform>();
    private Dictionary<Transform, float> arrowDistances = new Dictionary<Transform, float>();

    protected override void OnImpact()
    {
        // ���� �� ȭ�� ����
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, pullRadius);
        foreach (Collider2D hit in hits)
        {
            Arrow arrow = hit.GetComponent<Arrow>();
            if (arrow != null && !swirlingArrows.Contains(hit.transform))
            {
                swirlingArrows.Add(hit.transform);
                arrow.StopArrowCoroutine(); // �����ϰ� �ڷ�ƾ ����
                arrowDistances[arrow.transform] = Vector3.Distance(transform.position, arrow.transform.position);
            }
        }

        // ȸ���� �̵� ó��
        for (int i = 0; i < swirlingArrows.Count; i++)
        {
            Transform arrow = swirlingArrows[i];

            // �̹� ���ŵ� ȭ���̸� ����Ʈ���� ����
            if (arrow == null)
            {
                swirlingArrows.RemoveAt(i);
                arrowDistances.Remove(arrow);
                i--;
                continue;
            }

            float distance = arrowDistances[arrow];
            Vector3 centerDir = transform.position - arrow.position;

            // �߽� ���� ���� ����
            Vector3 tangent = new Vector3(-centerDir.y, centerDir.x, 0).normalized;

            // ���� �ӵ� ȸ�� + ��¦ ����
            float swirlThisFrame = swirlSpeed * Time.deltaTime * (1f + Random.Range(-randomness, randomness));
            Vector3 randomOffset = new Vector3(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f), 0) * randomness;

            // ��ġ ������Ʈ (ȸ�� + �߽� ���� + �ұ�Ģ ������)
            arrow.position += tangent * swirlThisFrame;
            arrow.position += centerDir.normalized * pullStrength * Time.deltaTime;
            arrow.position += randomOffset * Time.deltaTime;

            // �Ÿ� ������Ʈ
            arrowDistances[arrow] = Mathf.Max(0.1f, distance - pullStrength * Time.deltaTime);
        }
    }
}
