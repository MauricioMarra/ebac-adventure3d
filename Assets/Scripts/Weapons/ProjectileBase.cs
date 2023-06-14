using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class ProjectileBase : MonoBehaviour
{
    public float bulletSpeed = .5f;
    public float direction = 1;

    private float _lifetime = 2f;

    [SerializeField] protected List<string> _tagsToCollide = new List<string>();

    protected virtual void Start()
    {
        Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        MoveProjectile();
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        var c = collision.gameObject.GetComponent<IDamagable>();

        if (c != null && _tagsToCollide?.Find(x => x.Equals(collision.gameObject.tag))?.Count() > 0)
        {
            var hitDirection = collision.gameObject.transform.position - transform.position;
            hitDirection.y = 0;

            if (collision.gameObject.tag.Equals("Boss") || collision.gameObject.tag.Equals("Player"))
                c.TakeDamage(5);
            else
                c.TakeDamage(5, hitDirection.normalized * -1);


            Destroy(gameObject, 3f);
        }

        if (!collision.gameObject.tag.Equals("Enemy") && !collision.gameObject.tag.Equals("Boss") && !collision.gameObject.tag.Equals("Projectile"))
        {
            this.GetComponentInChildren<SphereCollider>().enabled = false;
            this.GetComponentInChildren<MeshRenderer>().enabled = false;
        }
    }

    protected virtual void MoveProjectile()
    {
        this.transform.Translate(0, 0, direction * bulletSpeed * Time.deltaTime);
    }
}
