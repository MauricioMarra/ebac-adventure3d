using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public ProjectileBase projectile;
    public Transform projectileSpawnPoint;
    public float shootDelay;
    public GameObject player;
    public AudioClip audioClip;

    private Coroutine _shootCoroutine;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            _shootCoroutine = StartCoroutine(ShootCoroutine());
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            if (_shootCoroutine != null) StopCoroutine(_shootCoroutine);
        }
    }

    IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootDelay);
        }
    }

    private void Shoot()
    {
        var p = Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
        p.direction = player.transform.localScale.x;
    }
}
