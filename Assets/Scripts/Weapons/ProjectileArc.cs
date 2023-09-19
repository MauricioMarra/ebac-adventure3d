using System.Collections.Generic;
using UnityEngine;

public class ProjectileArc : ProjectileBase
{
    private GameObject _target;
    
    private Transform startPoint;
    private Transform endPoint;
    
    public float arcHeight = 5f;
    public float arcDuration = 1f;

    private float elapsedTime = 0f;

    [SerializeField] private List<ParticleSystem> _explosion;

    private void Awake()
    {
        startPoint = this.transform;
    }

    private void Update()
    {
        //elapsedTime = 0f;
        //ShootProjectile();
    }

    public void ShootProjectile()
    {
        elapsedTime = 0f;
        MoveProjectile();
    }

    protected override void MoveProjectile()
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

    public void SetTarget(GameObject target)
    {
        this._target = target;
        endPoint = _target.transform;
    }

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        //if (_tagsToCollide.Find(x => x.Equals(collision.gameObject.tag)).Count() > 0)
        if (!collision.gameObject.tag.Equals("Enemy") || !collision.gameObject.tag.Equals("Boss"))
            PlayExplosion();
    }

    private void PlayExplosion()
    {
        _explosion.ForEach(i => i.Play());
    }
}
