using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectablePhysics : CollectableBase
{
    private void Update()
    {
        this.transform.RotateAroundLocal(Vector3.up, 5f * Time.deltaTime);
    }

    protected override void OnTriggerEnter(Collider collision)
    {
        base.OnTriggerEnter(collision);

        if (collision.gameObject.CompareTag("Ground"))
        {
            this.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        }
    }
}
