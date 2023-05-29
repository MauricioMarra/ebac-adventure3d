using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float bulletSpeed = .5f;
    public float direction = 1;

    private float _lifetime = 2f;

    private void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        this.transform.Translate(0, 0, direction * bulletSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        var c = collision.gameObject.GetComponent<IDamagable>();
        if (c != null)
        {
            var hitDirection = collision.gameObject.transform.position - transform.position;
            hitDirection.y = 0;

            c.TakeDamage(5, hitDirection.normalized * -1);
            Destroy(gameObject);
        }
    }
}
