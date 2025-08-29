using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class LightningSkill : SkillBase
{
    [Header("LightningSettings")]
    [SerializeField] private int segmentCount = 10;  
    [SerializeField] private float jaggedness = 0.5f;   
    [SerializeField] private float interval = 3f;       
    [SerializeField] private float flashDuration = 0.2f;
    [SerializeField] private GameObject lightningReadyPrefab;
    [SerializeField] private GameObject lightningPrefab;

    private LineRenderer line;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
    }

    private void DrawLightning(Vector2 start, Vector2 end)
    {
        line.positionCount = segmentCount + 1;
        Vector2 direction = (end - start).normalized;

        for (int i = 0; i <= segmentCount; i++)
        {
            float t = i / (float)segmentCount;
            Vector2 point = Vector2.Lerp(start, end, t);

            if (i != 0 && i != segmentCount)
            {
                float offsetFactor = Mathf.Sin(t * Mathf.PI);
                Vector2 perpendicular = new Vector2(-direction.y, direction.x);
                point += perpendicular * (Random.Range(-jaggedness, jaggedness) * offsetFactor);
            }

            line.SetPosition(i, point);
        }
    }

    protected override IEnumerator SkillCoroutine()
    {
        while (true)
        {
            GameObject ready = null;
            if (lightningReadyPrefab != null)
            {
                ready = Instantiate(lightningReadyPrefab, transform);
                ready.SetActive(true);
            }
            
            Vector2 start = new Vector2(Random.Range(-8f, 8f), transform.position.y);
            Vector2 end = new Vector3(start.x, -1.55f, 0);
            ready.transform.position = start;

            yield return new WaitForSeconds(interval);
            
            Destroy(ready);
            
            DrawLightning(start, end);
            line.enabled = true;
            
            if (lightningPrefab != null)
            {
                GameObject hitEffect = PoolObject(lightningPrefab);
                hitEffect.transform.position = end;
            }
            
            yield return new WaitForSeconds(flashDuration);
            line.enabled = false;
        }
    }
}