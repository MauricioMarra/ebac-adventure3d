using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAbilityArchShoot : EnemyAiblityBase
{
    private Transform startPoint;
    private Transform endPoint;
    public float arcHeight = 5f;
    public float arcDuration = 1f;

    private float elapsedTime = 0f;

    private void Start()
    {
        startPoint = this.transform;
        endPoint = _player.transform;
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Y))
        //{
        //    elapsedTime = 0f;
        //    LaunchArc();
        //}
    }

    private void LaunchArc()
    {
        Vector3 startPos = startPoint.position;
        Vector3 endPos = endPoint.position;
        Vector3 controlPoint = CalculateControlPoint(startPos, endPos, arcHeight);

        StartCoroutine(PerformArc(startPos, endPos, controlPoint));
    }

    private Vector3 CalculateControlPoint(Vector3 start, Vector3 end, float height)
    {
        Vector3 midPoint = (start + end) / 2f;
        midPoint.y += height;
        return midPoint;
    }

    private System.Collections.IEnumerator PerformArc(Vector3 start, Vector3 end, Vector3 control)
    {
        while (elapsedTime < arcDuration)
        {
            float t = elapsedTime / arcDuration;
            transform.position = CalculateArcPoint(start, end, control, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private Vector3 CalculateArcPoint(Vector3 start, Vector3 end, Vector3 control, float t)
    {
        float u = 1f - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 point = (uu * start) + (2f * u * t * control) + (tt * end);
        return point;
    }
}
