using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunBase : MonoBehaviour
{
    public ProjectileBase projectile;
    public Transform projectileSpawnPoint;
    public float shootDelay;
    public GameObject player;
    public AudioClip audioClip;

    private Coroutine _shootCoroutine;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    protected virtual IEnumerator ShootCoroutine()
    {
        while (true)
        {
            Shoot();
            yield return new WaitForSeconds(shootDelay);
        }
    }

    protected virtual void Shoot()
    {
        var p = Instantiate(projectile, projectileSpawnPoint.position, transform.rotation);
        p.direction = player.transform.localScale.x;
        p.ChangeDamageMultiplier(player.GetComponent<Player>().GetDamageMultiplier());
    }

    public void StartShoting()
    {
        _shootCoroutine = StartCoroutine(ShootCoroutine());
    }

    public void StopShooting()
    {
        if (_shootCoroutine != null) StopCoroutine(_shootCoroutine);
    }
}
